﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyEnemyTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject m_mainParent;
    [SerializeField]
    private EnemyAI m_myAI;

    private bool m_isActivated;

    private void OnTriggerEnter(Collider other)
    {
        if (m_isActivated) return;

        if (other.gameObject.GetComponent<BulletProp>())
        {
            DestroyMe();
        }
        if (other.gameObject.GetComponent<PlayerController>())
        {
            HitAndDie();
        }
    }

    private void DestroyMe()
    {
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child != transform)
            {
                Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
                Vector3 offset = new Vector3(Random.Range(2f, 10f), Random.Range(2f, 10f), Random.Range(2f, 10f));
                rb.AddForce(offset, ForceMode.Impulse);
            }
            m_isActivated = true;
            StartCoroutine(DelayDestroy());
        }
    }

    private void HitAndDie()
    {
        // todo take a hit
        DestroyMe();
    }

    private IEnumerator DelayDestroy()
    {
        m_myAI.enabled = false;
        yield return new WaitForSeconds(15f);
        Destroy(m_mainParent);
    }
}
