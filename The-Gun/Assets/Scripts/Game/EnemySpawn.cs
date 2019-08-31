using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private Transform m_enemyContainer;
    [SerializeField]
    private GameObject m_enemyPrefab;
    [SerializeField]
    private float m_maxSpawnTime = 5f;

    private float m_currentSpawnTime;

    private bool m_isSpawning;
    private bool m_isReadyToSpawnOne = false;

    private void Start()
    {
        m_currentSpawnTime = Random.Range(0f, m_maxSpawnTime);
    }

    private void Update()
    {
        if (m_isSpawning)
        {
            SpawnTimer();
            if (m_isReadyToSpawnOne)
            {
                Instantiate(m_enemyPrefab, transform.position, Quaternion.identity, m_enemyContainer);
                m_isReadyToSpawnOne = false;
            }
        }
    }

    private void SpawnTimer()
    {
        m_currentSpawnTime -= Time.deltaTime;
        if (m_currentSpawnTime <= 0)
        {
            m_isReadyToSpawnOne = true;
            m_currentSpawnTime = Random.Range(0f, m_maxSpawnTime);
        }
    }

    public void ChangeThisSpawnActivity()
    {
        m_isSpawning = !m_isSpawning;
    }
}
