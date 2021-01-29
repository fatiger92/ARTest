using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ImageRecognitionExample : MonoBehaviour
{
    [SerializeField] private Text m_txtAdded;
    [SerializeField] private Text m_txtUpdate;
    [SerializeField] private Text m_txtRemoved;

    ARTrackedImageManager m_aRTrackedImageManager;

    int m_addedCount = 0;
    int m_updateCount = 0;
    int m_removedCount = 0;

    void Awake()
    {
        GetReference();
    }
    void GetReference()
    {
        m_aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        m_aRTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        m_aRTrackedImageManager.trackedImagesChanged -= OnImageChanged;
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
                        break;
                    case TrackingState.Tracking:
                        ++m_updateCount;
                        break;
                    case TrackingState.Limited:
                        break;
                    default:
                        break;
                }
                m_txtUpdate.text = string.Format($"updated 실행 되어진 수 :: {m_updateCount}");
            }
        }

        if (args.removed != null)
        {
            foreach (var trackedImage in args.removed)
            {
                switch (trackedImage.trackingState)
                {
                    case TrackingState.None:
                        break;
                    case TrackingState.Tracking:
                        ++m_removedCount;
                        break;
                    case TrackingState.Limited:
                        break;
                    default:
                        break;
                }
                m_txtRemoved.text = string.Format($"removed 실행 되어진 수 :: {m_removedCount}");
            }
        }
        else
        {
            m_txtRemoved.text = string.Format($"removed는 작동이안되요...");
        }
    }
}
