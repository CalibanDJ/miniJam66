using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10.0f;
    public float JumpForce = 30.0f;
    [Range(0, .3f)] public float MovementSmoothing = .05f;
    //public Animator Animator;

    //Type of actions
    public enum Action { Forward = 1, Backward = -1, Jump = 2, Attack = 3};
    public Action[] key_primary = new Action[2];
    public Action[] key_secondary = new Action[2];
    int lastAct1 = 1;
    int lastAct2 = 1;

    //Attack variables
    public float attack_speed = 1.0f;
    float next_attack = 0.0f;

    private Rigidbody rigidBody;
    private float motion = 0;
    private bool jump = false;
    private float distToGround = 1f;

    private void Assign_Keys()
    {
        key_primary[0] = Action.Forward;
        key_primary[1] = Action.Backward;
        key_secondary[0] = Action.Attack;
        key_secondary[1] = Action.Jump;
    }
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        Assign_Keys();
    }

    private int OnBDown(string button, Action[] actions, int lastAction)
    {
        if (Input.GetButtonDown(button))
        {
            lastAction = (lastAction + 1) % 2;
            if (actions[lastAction] == Action.Jump && !jump && IsGrounded())
            {
                jump = true;
                lastAction = (lastAction + 1) % 2;
            }
        }

        if (Input.GetButton(button))
        {
            if (actions[(lastAction + 1) % 2] == Action.Forward || actions[(lastAction + 1) % 2] == Action.Backward)
            {
                motion = (int)actions[lastAction] * Speed;
            }
            if (actions[lastAction] == Action.Attack && (Time.time >= next_attack))
            {
                next_attack = Time.time + attack_speed;
                Attack();
            }
        }

        return lastAction;
    }

    private void Inputs()
    {
        lastAct1 = OnBDown("Key1", key_primary, lastAct1);
        lastAct2 = OnBDown("Key2", key_secondary, lastAct2);
    }

    private void Attack()
    {

    }

    private void Update()
    {
        Inputs();
        //motion = Input.GetAxisRaw("Horizontal") * Speed;
        //jump = Input.GetButton("Jump");
        //Animator.SetFloat("HorizontalSpeed", motion);
        //Animator.SetFloat("VerticalSpeed", rigidBody.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }

    private void FixedUpdate()
    {
        Debug.Log(IsGrounded());
       Move(motion * Time.fixedDeltaTime);
    }

    private void Move(float speed)
    {
        //Vector2 targetVelocity = new Vector2(speed, rigidBody.velocity.y);
        //rigidBody.velocity = Vector2.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, MovementSmoothing);

        if (jump && IsGrounded())
        {
            jump = false;
            //Animator.SetBool("OnGround", false);
            rigidBody.AddForce(new Vector3(0f, JumpForce, 0f), ForceMode.Impulse);
        }
    }

    public string getString()
    {
        return Speed.ToString();
    }
}
