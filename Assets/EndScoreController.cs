using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScoreController : MonoBehaviour
{

    public static int score = 0;
    static int totalScore = 0;

    public static void majScore(int n)
    {
        score += n;
    }

    public static void changeLevel()
    {
        totalScore += score;
        score = 0;
    }

    public static void reset()
    {
        score = 0;
        totalScore = 0;
    }

    void Start()
    {
        GameObject.Find("num").GetComponent<UnityEngine.UI.Text>().text = totalScore.ToString();
    }
}
