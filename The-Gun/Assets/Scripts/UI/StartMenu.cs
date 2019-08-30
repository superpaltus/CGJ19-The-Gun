using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private Button btnStart;
    [SerializeField]
    private Button btnQuit;

    private void OnEnable()
    {
        btnStart.onClick.AddListener(OnBtnStartCkick);
        btnQuit.onClick.AddListener(OnBtnQuitClick);
    }

    private void OnBtnStartCkick()
    {
        SceneManager.LoadScene("Main");
    }
    private void OnBtnQuitClick()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        btnStart.onClick.RemoveListener(OnBtnStartCkick);
        btnQuit.onClick.RemoveListener(OnBtnQuitClick);
    }
}
