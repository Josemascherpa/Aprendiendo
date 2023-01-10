using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PollitoScript : MonoBehaviour
{
    private NavMeshAgent agente;
    private GameObject pastitoABuscar;
    private GameObject[] pastitos;
    private bool buscar = true;
    private GameObject pastitoYaComido=null;
    private Animator anim;
    private bool eat= false;
    private bool run=false;
    void Start()
    {
        anim = GetComponent<Animator>();
        agente = GetComponent<NavMeshAgent>();
        //HACER UN ARREGLOS CON TODOS LOS PASTITOS
        pastitos = GameObject.FindGameObjectsWithTag("pastito");
        pastitoABuscar = pastitos[Random.Range(0, pastitos.Length)];
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Run", run);
        anim.SetBool("Eat", eat);

        if (buscar && pastitoABuscar!=null && agente!=null)
        {
            eat = false;
            run = true;            
            agente.destination = pastitoABuscar.transform.position;
        }
        else if(pastitos.Length<=0 && agente != null)
        {
            print("BUSCNADO PLAYER");
            agente.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("pastito") && agente!=null)
        {
            run = false;
            eat = true;            
            buscar = false;
            agente.enabled = false;
            Vector3 direction = collision.gameObject.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = rotation;            
            StartCoroutine(NuevoObjetivo());
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(agente);
             
        }
    }

    IEnumerator NuevoObjetivo()
    {
        yield return new WaitForSeconds(8f);
        if (agente != null)
        {            
            pastitoABuscar = null;
            pastitos = null;
            pastitos = GameObject.FindGameObjectsWithTag("pastito");
            pastitoABuscar = pastitos[Random.Range(0, pastitos.Length)];
            buscar = true;
            agente.enabled = true;
        }
        
    }
    
}
