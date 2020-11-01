using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerController player;
    public float speed = 0.08f;
    public Rigidbody2D rigidBody;
    public int hp;
    public bool Grounded = false;
    public float JumpForce = 30.0f;

    public void SetDirection()
    {
        if (rigidBody.velocity.x > 0 && transform.rotation.eulerAngles.y != 0)
        {
            transform.localRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        }
        else if (rigidBody.velocity.x < 0 && transform.rotation.eulerAngles.y != 180)
        {
            transform.localRotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
        }

    }

    public void Follow()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
    }

    public void CheckStatus()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == ("Weapon"))
        {
            hp--;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        ExecuteGrounded(col);
    }

    void ExecuteGrounded(Collision2D col)
    {
        if ( !(Grounded) && col.gameObject.tag == ("Ground"))
        {
            Grounded = true;
        }
    }


  
}
