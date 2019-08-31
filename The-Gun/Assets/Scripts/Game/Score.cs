using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private Text m_txtScore;

    private int m_currentScore = -1;

    public static Score instance { get; private set; }


    private void Start()
    {
        instance = this;
        UpdateScore();
    }

    public void UpdateScore()
    {
        m_currentScore++;
        m_txtScore.text = m_currentScore.ToString() + "/1000";
    }
}
