using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class ARFillteredPlanes : MonoBehaviour
{
    [SerializeField] private Vector2 dimensionForBigPlane;

    public event Action OnVerticalPlaneFound;
    public event Action OnHorizontalPlaneFound;
    public event Action OnBigPlaneFound;

    private ARPlaneManager m_arPlaneManager;
    private List<ARPlane> m_arPlanes;

    private void OnEnable()
    {
        m_arPlanes = new List<ARPlane>();
        m_arPlaneManager = FindObjectOfType<ARPlaneManager>();
        m_arPlaneManager.planesChanged += OnPlanesChanged;
    }

    private void OnDisable()
    {
        m_arPlaneManager.planesChanged -= OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null && args.added.Count > 0)
        {
            m_arPlanes.AddRange(args.added);
        }

        foreach (ARPlane plane in m_arPlanes.Where(plane => plane.extents.x * plane.extents.y >= 0.1f))
        {
            if (plane.alignment.IsVertical())
            {
                OnVerticalPlaneFound.Invoke();
            }
            else
            {
                OnHorizontalPlaneFound.Invoke();
            }

            if (plane.extents.x * plane.extents.y >= dimensionForBigPlane.x * dimensionForBigPlane.y)
            {
                OnBigPlaneFound.Invoke();
            }
        }
    }
}
