using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldScript : MonoBehaviour
{
    public int numEnemy;
    GameObject numEnemyHUD;

    // Start is called before the first frame update
    void Start()
    {
        
        numEnemyHUD = GameObject.Find("NumEnemy");
        MajEnemy();
    }

    void MajEnemy()
    {
        numEnemy = GameObject.FindGameObjectsWithTag("Ennemy").Length;
        numEnemyHUD.GetComponent<UnityEngine.UI.Text>().text = numEnemy.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        MajEnemy();
        if(numEnemy <= 0)
        {
            EndScoreController.changeLevel();
            MainMenu.lastLevel(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
