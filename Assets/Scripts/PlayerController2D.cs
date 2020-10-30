using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float Speed = 10.0f;
    public float JumpForce = 10.0f;
    public LayerMask GroundLayer;
    [Range(0, .3f)] public float MovementSmoothing = .05f;
    public Animator Animator;

    private Rigidbody2D rigidBody;
    private Vector2 velocity = Vector2.zero;
    private float motion = 0;
    private bool jump = false;
    private bool onGround = false;
    
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collisionInfo)
    {
        if (!onGround)
        {
            foreach (ContactPoint2D contact in collisionInfo.contacts)
            {
                if (contact.normal.normalized.y > 0.5)
                {
                    onGround = true;
                    Animator.SetBool("OnGround", true);
                    break;
                }
            }
        }
    }

    private void Update()
    {
        motion = Input.GetAxisRaw("Horizontal") * Speed;
        jump = Input.GetButton("Jump");
        Animator.SetFloat("HorizontalSpeed", motion);
        Animator.SetFloat("VerticalSpeed", rigidBody.velocity.y);
    }

    private void FixedUpdate()
    {
        if (onGround)
        {
            //Fall off the ground
            if (!rigidBody.IsTouchingLayers(GroundLayer))
            {
                onGround = false;
                Animator.SetBool("OnGround", false);
            }
        }


        Move(motion * Time.fixedDeltaTime);
    }

    private void Move(float speed)
    {
        Vector2 targetVelocity = new Vector2(speed, rigidBody.velocity.y);
        rigidBody.velocity = Vector2.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, MovementSmoothing);
        
        if (jump && onGround)
        {
            onGround = false;
            Animator.SetBool("OnGround", false);
            rigidBody.AddForce(new Vector2(0f, JumpForce));
        }
    }
}
