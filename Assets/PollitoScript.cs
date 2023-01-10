using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PollitoScript : MonoBehaviour
{
    private NavMeshAgent agente;    
    private GameObject[] pastitos;
    private bool buscar = true;   
    private Animator anim;
    private bool eat= false;
    private bool run=false;
    void Start()
    {
        anim = GetComponent<Animator>();
        agente = GetComponent<NavMeshAgent>();
        //HACER UN ARREGLOS CON TODOS LOS PASTITOS
        pastitos = GameObject.FindGameObjectsWithTag("pastito");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        print("Buscar:" + buscar);
        if (buscar)
        {
            print("Pastitos que hay cuando busco" + pastitos.Length);
            var pastito = pastitos[Random.Range(0, pastitos.Length)].transform.position;
            if(pastito!= null)
            {
                agente.destination = pastito;
                print("Encontre pastito");
            }
            else
            {
                agente.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
                print("Debo buscar al player");
            }          
            eat = false;
            run = true;          
            
        }
        anim.SetBool("Run", run);
        anim.SetBool("Eat", eat);




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
            print("Comi un pastito y tengo que buscar uno nuevo");
        }
        
    }

    IEnumerator NuevoObjetivo()
    {
        yield return new WaitForSeconds(8f);
        if (agente != null)
        {            
            pastitos = GameObject.FindGameObjectsWithTag("pastito");
            buscar = true;
            agente.enabled = true;
            print("Busque uno nuevo y actualize los que hay" + pastitos.Length);
        }
        
    }
    
}
