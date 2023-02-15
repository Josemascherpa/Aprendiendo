using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PollitoScript : MonoBehaviour
{
    private NavMeshAgent agente;        
    private bool buscar = true;   
    private Animator anim;
    private bool eat= false;
    private bool run=false;
    [SerializeField] private PastosScript pastosList;
    private int numRand;
    private bool attackPlayer = false;
    private Renderer renderer;
    private void Awake()
    {
        renderer = transform.GetChild(4).GetComponent<Renderer>();
        buscar = true;
        pastosList = GameObject.FindGameObjectWithTag("pastos").GetComponent<PastosScript>();
        anim = GetComponent<Animator>();
        agente = GetComponent<NavMeshAgent>();        
    }
    private void Start()
    {
        numRand = Random.Range(0, pastosList.pastos.Count);       
    }

    // Update is called once per frame
    void Update()
    {
        if (buscar && agente.enabled && !attackPlayer)
        {            
            agente.destination = pastosList.pastos[numRand].gameObject.transform.position;
            eat = false;
            run = true;
            buscar = false;
        }else if(buscar && attackPlayer && agente.enabled)
        {
            renderer.material.color = Color.red;
            agente.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
            eat = false;
            run = true;
            
        }
        anim.SetBool("Run", run);
        anim.SetBool("Eat", eat);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("pastito") && agente!=null && !attackPlayer)
        {            
            agente.enabled = false;
            Vector3 direction = collision.gameObject.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = rotation;
            Physics.IgnoreCollision(this.transform.GetComponent<CapsuleCollider>(), collision.gameObject.transform.GetComponent<BoxCollider>());
            StartCoroutine(NuevoObjetivo());                        
            run = false;
            eat = true;            
        }
        if(collision.gameObject.CompareTag("Player") && attackPlayer)
        {
            Vector3 dir = collision.contacts[0].point - this.transform.position;           
            dir = dir.normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * 5, ForceMode.Impulse);
        }
        
    }

    IEnumerator NuevoObjetivo()
    {
        yield return new WaitForSeconds(8f);
        var numeroAttackPlayer = Random.Range(0, 100);
        if (agente != null && numeroAttackPlayer>5)
        {            
            numRand = Random.Range(0, pastosList.pastos.Count);            
            buscar = true;
            agente.enabled = true;

        }
        else
        {
            agente.enabled = true;
            attackPlayer = true;
            buscar = true;
        }
        
    }
    
}
