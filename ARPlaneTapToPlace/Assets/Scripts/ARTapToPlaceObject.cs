using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))] // ?? 뭘까 찾아보자.
public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject m_gameObjectToinstantiate;

    GameObject m_spawnedObject;
    ARRaycastManager m_aRRaycastManager;
    Vector2 m_touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        m_aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 m_touchPosition)
    {
        if (Input.touchCount > 0)
        {
            m_touchPosition = Input.GetTouch(index: 0).position;
            return true;
        }
        m_touchPosition = default;
        return true;
    }

    private void Update()
    {
        if (!TryGetTouchPosition(out Vector2 m_touchPosition))
            return;
        if (m_aRRaycastManager.Raycast(m_touchPosition, hits, trackableTypes: TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if (m_spawnedObject == null)
            {
                m_spawnedObject = Instantiate(m_gameObjectToinstantiate, hitPose.position, hitPose.rotation);
            }
            else
            {
                m_spawnedObject.transform.position = hitPose.position;
            }
        }
    }
}
