using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField]
    GameObject m_canvasWithScore;
    [SerializeField]
    Text m_scoreText;
    [SerializeField]
    Material[] m_floorGood;
    [SerializeField]
    Material[] m_floorBad;
    [SerializeField]
    GameObject m_arena;

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
    [SerializeField]
    private GameObject m_enemiesContainer;

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
        if (!m_isPlayerGetWeapon)
        {
            if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
            {
                m_canvasWithScore.SetActive(true);
                PlayerGetWeapon(other.gameObject, true);
                m_doorsAnimator.SetTrigger("DoorsClose");
                ChangeHintText("Kill them all! Don't give up!");
                m_arena.GetComponent<MeshRenderer>().materials = m_floorBad;
            }
        }
        else 
        {
            if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
            {
                m_canvasWithScore.SetActive(false);
                PlayerGetWeapon(other.gameObject, false);
                ChangeHintText("Wow! You are smart!");
                m_arena.GetComponent<MeshRenderer>().materials = m_floorGood;

                m_doorsAnimator.SetTrigger("DoorsOpen");
                foreach (RigidbodyEnemyTrigger enemy in m_enemiesContainer.GetComponentsInChildren<RigidbodyEnemyTrigger>())
                {
                    enemy.DestroyMe();
                }
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
        yield return new WaitForSeconds(3f);
        m_myBoxCollider.enabled = true;
        print("trigger ready again");
    }
}
