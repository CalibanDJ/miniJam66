using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10.0f;
    public float JumpForce = 30.0f;
    [Range(0, .3f)] public float MovementSmoothing = .1f;
    public Animator Animator;
    public GameObject Rotor;

    //Type of actions
    public enum Action { Forward = 1, Backward = -1, Jump = 2, Attack = 3};
    public Action[] key_primary = new Action[2];
    public Action[] key_secondary = new Action[2];
    public bool key_update = true;
    public int lastAct1 = 1;
    public int lastAct2 = 1;

    //Attack variables
    public float attack_speed = 1.0f;
    float next_attack = 0.0f;

    private float motion = 0;
    private bool jump = false;
    private bool facingRight = true;
    private float distToGround = 1f;
    private Rigidbody2D rigidBody;
    private Vector2 velocity = Vector2.zero;
    private bool Grounded;

    //Attack variables
    public float damage_speed = 1.0f;
    float damage_attack = 0.0f;

    public uint actual_hp = 5;

    private AudioSource source;

    private void Assign_Keys()
    {
        key_primary[0] = Action.Forward;
        key_primary[1] = Action.Backward;
        key_secondary[0] = Action.Attack;
        key_secondary[1] = Action.Jump;
    }
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        Assign_Keys();
        distToGround = 0.1f;
    }

    private int OnBDown(string button, Action[] actions, int lastAction)
    {
        if (Input.GetButton(button))
        {
            if (actions[lastAction] == Action.Forward || actions[lastAction] == Action.Backward)
            {
                motion = (int)actions[lastAction] * Speed;
            }
            if (actions[lastAction] == Action.Attack && (Time.time >= next_attack))
            {
                next_attack = Time.time + attack_speed;
                Attack();
            }
            if (actions[lastAction] == Action.Jump && !jump && IsGrounded())
            {
                jump = true;
            }
        }
        if (Input.GetButtonUp(button))
        {
            if (actions[lastAction] == Action.Forward || actions[lastAction] == Action.Backward)
            {
                motion = 0;
            }
            lastAction = (lastAction + 1) % 2;
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
        this.GetComponent<WeaponPlayerController>().PerformWeaponAttack(Speed < -0.01);
        source.Play();
    }

    private void Update()
    {
        Inputs();
        Animator.SetFloat("HorizontalSpeed", Mathf.Abs(motion));
    }

    private void FixedUpdate()
    {
        Move(motion * Time.fixedDeltaTime);
        IsDead();
    }
    

    private void Move(float speed)
    {
        //rigidBody.AddForce(Vector3.right * speed * 200);
        Vector3 targetVelocity = new Vector3(speed, rigidBody.velocity.y);
        rigidBody.velocity = Vector2.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, MovementSmoothing);
        
        if (speed > 0.01 && !facingRight)
        {
            Animator.SetTrigger("RotateRight");
            Animator.ResetTrigger("RotateLeft");
            Rotor.transform.localRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            facingRight = true;
        }
        else if (speed < -0.01 && facingRight)
        {
            Animator.SetTrigger("RotateLeft");
            Animator.ResetTrigger("RotateRight");
            Rotor.transform.localRotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
            facingRight = false;
        }
        
        if (jump && IsGrounded())
        {
            jump = false;
            //Animator.SetBool("OnGround", false);
            rigidBody.AddForce(new Vector3(0f, JumpForce, 0f), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        ExecuteDamage(col);
        //ExecuteGrounded(col);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        ExecuteDamage(col);
    }

    void ExecuteDamage(Collision2D col)
    {
        if (col.gameObject.tag == ("Ennemy") && (Time.time >= damage_attack))
        {
            damage_attack = Time.time + damage_speed;
            Hitted(1);
        }
        else if (col.gameObject.tag == ("EnnemyConfusion") && (Time.time >= damage_attack))
        {
            Destroy(col.gameObject);
            damage_attack = Time.time + damage_speed;
            RandomizeActions();
        }
    }

    void ExecuteGrounded(Collision2D col)
    {
        if ( !(Grounded) && col.gameObject.tag == ("Ground"))
        {
            Grounded = true;
        }
    }

    void Hitted(uint damage)
    {
        actual_hp = actual_hp - damage;
    }

    void IsDead()
    {
        if(actual_hp <= 0)
        {
            EndScoreController.changeLevel();
            MainMenu.lastLevel(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("DeathScene");
        }
    }

    public string getString()
    {
        return Speed.ToString();
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position - new Vector3(0, transform.localScale.y / 2, 0), Vector3.down, distToGround, ~(1 << 9));
    }

    public void RandomizeActions()
    {
        for (int i = 0; i < 2; i++)
        {
            int index = Random.Range(i, 4);
            var arr = index > 2 ? key_secondary : key_primary;
            var tmp = arr[index & 1];
            arr[index & 1] = key_primary[i];
            key_primary[i] = tmp;
        }
        int index2 = Random.Range(0, 2);
        var tmp2 = key_secondary[index2];
        key_secondary[index2] = key_secondary[0];
        key_secondary[0] = tmp2;
        motion = 0;
        key_update = true;
    }
}
