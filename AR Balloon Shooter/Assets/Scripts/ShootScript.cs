using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject m_arCamera;
    public GameObject m_smoke;

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_arCamera.transform.position, m_arCamera.transform.forward, out hit))
        {
            if (hit.transform.name == "balloon1(Clone)" || hit.transform.name == "balloon2(Clone)" || hit.transform.name == "balloon3(Clone)")
            {
                Destroy(hit.transform.gameObject);

                Instantiate(m_smoke, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
