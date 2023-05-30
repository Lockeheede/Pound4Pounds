using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int m_Score = 0;
  
    public delegate void ScoreChangedDelegate(int score, int change);
    public ScoreChangedDelegate ScoreChangedCallback;

    public void ChangeScore(int points)
    {
        m_Score += points;

        Debug.Log("Score: " +  m_Score);

        ScoreChangedCallback(m_Score, points);
    }

}

