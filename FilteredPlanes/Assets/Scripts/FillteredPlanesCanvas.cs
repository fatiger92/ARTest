using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillteredPlanesCanvas : MonoBehaviour
{
    [SerializeField] private Toggle m_verticalPlaneToggle;
    [SerializeField] private Toggle m_horizontalPlaneToggle;
    [SerializeField] private Toggle m_bigPlanePlaneToggle;
    [SerializeField] private Button m_startButton;

    private ARFillteredPlanes m_arFillteredPlanes;

    public bool VerticalPlaneToggle 
    { 
        get => m_verticalPlaneToggle.isOn;
        set
        {
            m_verticalPlaneToggle.isOn = value;
            CheckIfAllAreTrue();
        } 
    }

    public bool HorizontalPlaneToggle
    {
        get => m_horizontalPlaneToggle.isOn;
        set
        {
            m_horizontalPlaneToggle.isOn = value;
            CheckIfAllAreTrue();
        }
    }

    public bool BigPlaneToggle
    {
        get => m_bigPlanePlaneToggle.isOn;
        set
        {
            m_bigPlanePlaneToggle.isOn = value;
            CheckIfAllAreTrue();
        }
    }

    /*
     * if you use which is Update(), you have to check every single frame, 
     * but if u use prop, can check only three times.
     */

    private void OnEnable()
    {
        m_arFillteredPlanes = FindObjectOfType<ARFillteredPlanes>();

        m_arFillteredPlanes.OnVerticalPlaneFound += () => VerticalPlaneToggle = true;
        m_arFillteredPlanes.OnHorizontalPlaneFound += () => HorizontalPlaneToggle = true;
        m_arFillteredPlanes.OnBigPlaneFound += () => BigPlaneToggle = true;
    }

    private void OnDisable()
    {
        m_arFillteredPlanes.OnVerticalPlaneFound -= () => VerticalPlaneToggle = true;
        m_arFillteredPlanes.OnHorizontalPlaneFound -= () => HorizontalPlaneToggle = true;
        m_arFillteredPlanes.OnBigPlaneFound -= () => BigPlaneToggle = true;
    }

    private void CheckIfAllAreTrue()
    {
        if (VerticalPlaneToggle && HorizontalPlaneToggle && BigPlaneToggle)
        {
            m_startButton.interactable = true;
        }
            
    }

}
