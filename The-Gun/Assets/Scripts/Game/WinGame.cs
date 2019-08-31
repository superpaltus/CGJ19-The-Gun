using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    [SerializeField]
    ParticleSystem m_winPS;
    [SerializeField]
    GameObject m_winMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var emission = m_winPS.emission;
            emission.enabled = true;
        }
        other.GetComponent<PlayerController>().enabled = false;

        Invoke("Win", 1.5f);
    }

    private void Win()
    {
        m_winMenu.SetActive(true);
    }
}
