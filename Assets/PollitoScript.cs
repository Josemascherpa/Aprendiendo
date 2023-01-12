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
    private void Awake()
    {
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
        if (buscar && agente.enabled)
        {            
            agente.destination = pastosList.pastos[numRand].gameObject.transform.position;
            eat = false;
            run = true;
            buscar = false;
        }
        anim.SetBool("Run", run);
        anim.SetBool("Eat", eat);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("pastito") && agente!=null)
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
        
    }

    IEnumerator NuevoObjetivo()
    {
        yield return new WaitForSeconds(8f);
        if (agente != null)
        {            
            numRand = Random.Range(0, pastosList.pastos.Count);            
            buscar = true;
            agente.enabled = true;
            
        }
        
    }
    
}
