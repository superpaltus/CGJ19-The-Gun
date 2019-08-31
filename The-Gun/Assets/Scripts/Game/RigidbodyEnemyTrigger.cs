using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyEnemyTrigger : MonoBehaviour
{
    private bool m_isActivated;

    private void OnTriggerEnter(Collider other)
    {
        if (m_isActivated) return;
        if (other.gameObject.GetComponent<BulletProp>())
        {
            foreach (Transform child in GetComponentsInChildren<Transform>())
            {
                if (child != transform)
                {
                    child.gameObject.AddComponent<Rigidbody>();
                }
                m_isActivated = true;
            }
        }
    }
}
