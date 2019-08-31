using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    private bool m_isActivated = false;
    [SerializeField]
    private CameraController m_cameraController;
    [SerializeField]
    private GameObject m_loseScreen;

    public void KillPlayer()
    {
        if (m_isActivated) return;

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child != transform)
            {
                Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
                Vector3 offset = new Vector3(Random.Range(2f, 10f), Random.Range(2f, 10f), Random.Range(2f, 10f));
                rb.AddForce(offset, ForceMode.Impulse);
            }
            m_isActivated = true;
        }
        m_cameraController.enabled = false;
        Invoke("Lose", 1.5f);
    }

    private void Lose()
    {
        m_loseScreen.SetActive(true);
    }
}
