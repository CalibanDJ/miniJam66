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
    public float DistanceDetection = 10.0f;
    public AudioSource source;

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
        if (Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2.0f)) <= DistanceDetection)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        }
        
    }

    public void CheckStatus()
    {
        if(hp <= 0)
        {
            EndScoreController.majScore(1);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == ("Weapon"))
        {
            if (source != null)
            {
                source.Play();
            }

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
