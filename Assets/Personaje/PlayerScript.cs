using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private float speed = 5;

    private Animator anim;
    private bool run = false;
    private bool jump = false;
    private bool attack1 = false;
    private bool attack2 = false;
    [SerializeField]private float jumpSpeed = 5f;

    private Vector3 velocity;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

    }
    private void Update()
    {
        if (attack1)
        {
            speed = 10;
        }
        else
        {
            speed = 5;
        }
        Inputs();
        Animation();
    }
    private void FixedUpdate()
    {
        Movement();
    }
    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            jump = true;
            rb.AddForce(new Vector3(0f, jumpSpeed, 0f), ForceMode.Impulse);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            attack1 = true;
            anim.Play("Attack1");
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            attack2 = true;
            anim.Play("Attack2");//TERMINAR ATTACK2 Y ATTACK 1 poner mas vel
        }
    }

    void Animation()
    {
        anim.SetBool("Run Forward", run);
        anim.SetBool("Jump", jump);
        anim.SetBool("Attack1", attack1);
        anim.SetBool("Attack2", attack2);
    }

    void Movement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        if (!jump)
        {
            velocity = Vector3.zero;
        }

        if (hor != 0.0f || ver != 0.0f)
        {
            if (!jump)
            {
                run = true;
            }
            Vector3 dir = transform.forward * ver + transform.right * hor;
            velocity = dir.normalized * speed;
            Vector3 rotateForward = transform.position - Camera.main.transform.position;
            Quaternion rotation = Quaternion.LookRotation(rotateForward.normalized);
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);

        }
        else
        {
            run = false;
        }
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;        
        if (rb.velocity.y >= jumpSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("terreno"))
        {
            if (jump)
            {
                jump = false;
                anim.Play("Idle");
            }
        }
    }

    void Attack01False()
    {
        attack1 = false;
    }void Attack02False()
    {
        attack2 = false;
    }
}