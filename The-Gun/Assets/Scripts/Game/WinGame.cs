using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    [SerializeField]
    ParticleSystem m_winPS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var emission = m_winPS.emission;
            emission.enabled = true;
        }
    }
}
