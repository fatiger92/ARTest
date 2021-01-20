using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public Transform[] m_arrSpawnPointsTransform;
    public GameObject[] m_arrBalloonsPrefabs;

    private void Start()
    {
        StartCoroutine(coStartSpawning());
    }
    IEnumerator coStartSpawning()
    {
        yield return new WaitForSeconds(4);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(m_arrBalloonsPrefabs[i], m_arrSpawnPointsTransform[i].position, Quaternion.identity);
        }

        StartCoroutine(coStartSpawning());
    }
}
