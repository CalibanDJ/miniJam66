using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScoreController : MonoBehaviour
{

    public static int score = 0;

    public static void majScore(int n)
    {
        score += n;
    }

    void Start()
    {
        GameObject.Find("num").GetComponent<UnityEngine.UI.Text>().text = score.ToString();
        score = 0;
    }
}
