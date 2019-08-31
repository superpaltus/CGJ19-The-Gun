using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProp : MonoBehaviour
{
    [SerializeField]
    private float m_lifeTime = 3f;

    private float m_currentLifeTime;

    void Start()
    {
        m_currentLifeTime = m_lifeTime;
    }

    void Update()
    {
        m_currentLifeTime -= Time.deltaTime;
        if (m_currentLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
