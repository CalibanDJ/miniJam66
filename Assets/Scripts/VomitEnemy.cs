using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomitEnemy : Enemy
{
    public float attack_speed = 7.0f;
    float next_attack = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        hp = 2;
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
        Jump();
        SetDirection();
        CheckStatus();
    }
    
    void Jump()
    {
        if (Grounded)
        {
            Grounded = false;
            rigidBody.AddForce(new Vector3(0f, JumpForce, 0f), ForceMode2D.Impulse);
        }
    }
}
