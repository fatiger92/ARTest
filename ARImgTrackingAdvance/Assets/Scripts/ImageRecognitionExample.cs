using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public enum ePrefabName
{
    Chicken =0,
    Cow,
    Duck,
}

public class ImageRecognitionExample : MonoBehaviour
{
    [Header("UI 텍스트")]
    [SerializeField] private Text m_txtAdded;
    [SerializeField] private Text m_txtUpdate;
    [SerializeField] private Text m_txtRemoved;
    [SerializeField] private Text m_txtStatus;

    [Header("트래킹 프리팹 데이터")]
    [SerializeField] private TrackPrefabsSO m_trackPrefabsSO;

    ARTrackedImageManager m_aRTrackedImageManager;
    Dictionary<string, GameObject> m_dicTrackDatas;

    int m_addedCount = 0;
    int m_updateCount = 0;
    int m_statusCount = 0;

    void Awake()
    {
        GetReference();
    }
    void GetReference()
    {
        m_aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
        m_dicTrackDatas = new Dictionary<string, GameObject>();
    }
    void OnEnable()
    {
        m_aRTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    void Start()
    {
        InitializeDictionary();
    }
    
    void InitializeDictionary()
    {
        GameObject shellGo = new GameObject();
        shellGo.name = "Prefabs";

        for (int i = 0; i < m_trackPrefabsSO.PrefabDatas.Length; i++)
        {
            GameObject tempGo = Instantiate(m_trackPrefabsSO.PrefabDatas[i].m_prefabsGO, shellGo.transform);
            tempGo.name = m_trackPrefabsSO.PrefabDatas[i].m_prefabName;
            tempGo.transform.localScale = m_trackPrefabsSO.PrefabDatas[i].m_scaleOffset;
            tempGo.SetActive(false);
            m_dicTrackDatas.Add(m_trackPrefabsSO.PrefabDatas[i].m_prefabName, tempGo);
        }
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        if (args.added != null)
        {
            foreach (var trackedImage in args.added)
            {
                ++m_addedCount;
                m_txtAdded.text = string.Format($"Added 실행 되어진 수 :: {m_addedCount}");
            }
        }

        if (args.updated != null)
        {
            foreach (var trackedImage in args.updated)
            {
                switch (trackedImage.trackingState)
                {
                    case TrackingState.None:
                        InactiveTrackPrefabs();
                        break;
                    case TrackingState.Tracking:
                        CheckTrackImage(trackedImage);
                        ++m_updateCount;
                        break;
                    case TrackingState.Limited:
                        m_txtStatus.text = string.Format($"Limited 실행 되어진 수 :: {m_statusCount}");
                        ++m_statusCount;
                        //InactiveTrackPrefabs();
                        break;
                }
                m_txtUpdate.text = string.Format($"updated 실행 되어진 수 :: {m_updateCount}");
            }
        }
    }

    public void CheckTrackImage(ARTrackedImage _image)
    {
        switch (_image.referenceImage.name)
        {
            case "Chicken":
                SetPrefabsPosition(m_dicTrackDatas[_image.referenceImage.name], _image.transform.localPosition, _image.transform.localRotation);
                m_dicTrackDatas[_image.referenceImage.name].SetActive(true);
                m_txtRemoved.text = _image.referenceImage.name;
                break;
            case "Cow":
                SetPrefabsPosition(m_dicTrackDatas[_image.referenceImage.name], _image.transform.localPosition, _image.transform.localRotation);
                m_dicTrackDatas[_image.referenceImage.name].SetActive(true);

                m_txtRemoved.text = _image.referenceImage.name;
                break;
            case "Duck":
                SetPrefabsPosition(m_dicTrackDatas[_image.referenceImage.name], _image.transform.localPosition, _image.transform.localRotation);
                m_dicTrackDatas[_image.referenceImage.name].SetActive(true);

                m_txtRemoved.text = _image.referenceImage.name;
                break;
            default:
                m_txtRemoved.text = "그런 동물이 없다고";
                break;
        }
    }
    public void SetPrefabsPosition(GameObject _prefab, Vector3 _pos, Quaternion _rot)
    {
        _prefab.transform.position = _pos;
        _prefab.transform.rotation = _rot;
    }
    public void InactiveTrackPrefabs()
    {
        foreach (var data in m_dicTrackDatas)
        {
            data.Value.SetActive(false);
        }
    }

    public void OnDisable()
    {
        m_aRTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }
}
