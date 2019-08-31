using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 25f;

    private Transform m_playerTransform;

    void Start()
    {
        m_playerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        transform.LookAt(m_playerTransform, Vector3.up);
        Vector3 translation = (m_playerTransform.position - transform.position).normalized * Time.deltaTime * m_speed;
        transform.Translate(translation, Space.World);
    }
}
