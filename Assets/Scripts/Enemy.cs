using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerController player;
    public float speed = 0.03f;
    public Rigidbody2D rigidBody;
    public int hp;

    public void SetDirection()
    {
        if (rigidBody.velocity.x > 0 && transform.rotation.eulerAngles.y != 0)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.y = 0;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        else if (rigidBody.velocity.x < 0 && transform.rotation.eulerAngles.y != 180)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.y = 180;
            transform.rotation = Quaternion.Euler(rotationVector);
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == ("Weapon"))
        {
            hp--;
        }
    }
}
