using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScoreController : MonoBehaviour
{
    public static void majScore(int n)
    {
        GameObject.Find("NumEnemy").GetComponent<UnityEngine.UI.Text>().text = n.ToString();
    }
}
