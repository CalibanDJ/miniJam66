using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    

    // Start is called before the first frame update
    void Start()
    {
        hp = 1;
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
        rigidBody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        Follow();
        SetDirection();
        CheckStatus();
    }

}
