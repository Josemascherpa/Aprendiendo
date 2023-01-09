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
    [SerializeField]private float jumpSpeed = 5f;


    private Vector3 velocity;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

    }
    private void Update()
    {       
        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {           
            jump = true;            
            rb.AddForce(new Vector3(0f, jumpSpeed, 0f), ForceMode.Impulse);
        }
        anim.SetBool("Run Forward", run);
        anim.SetBool("Jump", jump);
    }
    private void FixedUpdate()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        if (!jump)
        {
            velocity = Vector3.zero;
        }

        if (hor != 0.0f || ver != 0.0f )
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
            velocity.y = rb.velocity.y;
            rb.velocity = velocity ;
        }
        else
        {
            run = false;
        }

        if (rb.velocity.y >= 7)
        {
            rb.velocity = new Vector3(rb.velocity.x, 7f, rb.velocity.z);
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
}