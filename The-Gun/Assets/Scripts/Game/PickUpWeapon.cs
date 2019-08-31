using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject m_weaponOnPedestal;
    [SerializeField]
    private GameObject m_canvasWithHint;
    [SerializeField]
    private Text m_txtHint;
    [SerializeField]
    private EnemySpawnParent m_enemySpawnParent;
    [SerializeField]
    private Animator m_doorsAnimator;

    private Collider m_myBoxCollider;
    private bool m_isPlayerGetWeapon = false;

    private void Start()
    {
        m_myBoxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_canvasWithHint.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        print("im on collider");

        if (!m_isPlayerGetWeapon)
        {
            print("ready to get");
            if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
            {
                print("get");
                PlayerGetWeapon(other.gameObject, true);
                m_doorsAnimator.SetTrigger("DoorsClose");
                ChangeHintText("Fight them all!");
            }
        }
        else 
        {
            print("ready to unget");

            if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
            {
                print("unget");
                PlayerGetWeapon(other.gameObject, false);
                ChangeHintText("Украина незалежна держава!");
                m_doorsAnimator.SetTrigger("DoorsOpen");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_canvasWithHint.SetActive(false);
        }
    }

    private void PlayerGetWeapon(GameObject player, bool isGet)
    {
        player.GetComponent<PlayerController>().SetWeapon(isGet);
        m_weaponOnPedestal.SetActive(!isGet);
        m_isPlayerGetWeapon = isGet;
        m_enemySpawnParent.ChangeEnemySpawnsActivity();
        StartCoroutine(ResetTrigger());
    }

    private void ChangeHintText(string newHintText)
    {
        m_txtHint.text = newHintText;
    }

    IEnumerator ResetTrigger()
    {
        m_myBoxCollider.enabled = false;
        print("trigger inactive");
        yield return new WaitForSeconds(5f);
        m_myBoxCollider.enabled = true;
        print("trigger ready again");
    }
}
