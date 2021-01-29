using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrackPrefabsSO", menuName = "Custom/TrackPrefabsSO", order = int.MaxValue)]
public class TrackPrefabsSO : ScriptableObject
{
    [System.Serializable]
    public class TrackPrefabDatas
    {
        [Header("�Ӽ�")]
        public string m_prefabName;
        public GameObject m_prefabsGO;

        [Header("������")]
        public Vector3 m_scaleOffset;
    }

    [SerializeField] private TrackPrefabDatas[] m_arrTrackPrefabDatas;
    public TrackPrefabDatas[] PrefabDatas { get => m_arrTrackPrefabDatas; }
}
