using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomitEnemy : Enemy
{
    PlayerController player;
    public float attack_speed = 7.0f;
    float next_attack = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    void Attack()
    {
        if (Time.time >= next_attack)
        {
            next_attack = Time.time + attack_speed;

        }
    }
}
