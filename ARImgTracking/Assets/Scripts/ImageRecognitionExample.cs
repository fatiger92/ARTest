using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageRecognitionExample : MonoBehaviour
{
    private ARTrackedImageManager m_aRTrackedImageManager;

    void Awake()
    {
        m_aRTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    public void OnEnable()
    {
        m_aRTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        m_aRTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            Debug.LogFormat($" 트래킹된 이미지 이름 :: {trackedImage.name}");
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
