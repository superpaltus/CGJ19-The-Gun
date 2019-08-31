using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] AudioClip shot;
    [SerializeField] AudioSource audioSource;


    [SerializeField]
    private Transform m_bulletPivot;
    [SerializeField]
    private GameObject m_bulletPrefab;
    [SerializeField] [Range(0f, 20f)]
    private float m_startBulletMagnitude = 5f;
    [SerializeField]
    private Transform m_bulletContainer;

    private Transform m_myTransform;

    private void Start()
    {
        m_myTransform = transform;
    }

    private void Update()
    {
        ProcessShooting();
    }

    private void ProcessShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(shot);
            GameObject newBullet = Instantiate(m_bulletPrefab, m_bulletPivot.position, Quaternion.identity);
            newBullet.transform.SetParent(m_bulletContainer, true);
            Vector3 distance = (newBullet.transform.position - m_myTransform.position).normalized;
            newBullet.GetComponent<Rigidbody>().AddForce(distance * m_startBulletMagnitude, ForceMode.Impulse);
        }
    }
}
