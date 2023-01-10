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
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        //HACER UN ARREGLOS CON TODOS LOS PASTITOS
        pastitos = GameObject.FindGameObjectsWithTag("pastito");
        pastitoABuscar = pastitos[Random.Range(0, pastitos.Length)];
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (buscar && pastitoABuscar!=null)
        {
            agente.destination = pastitoABuscar.transform.position;
        }
        else if(pastitos.Length<=0)
        {
            agente.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("pastito"))
        {            
            buscar = false;
            agente.enabled = false;
            Vector3 direction = collision.gameObject.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = rotation;            
            StartCoroutine(NuevoObjetivo());
            Destroy(collision.gameObject);
        }
    }

    IEnumerator NuevoObjetivo()
    {
        yield return new WaitForSeconds(8f);        
        pastitoABuscar = null;
        pastitos = null;
        pastitos = GameObject.FindGameObjectsWithTag("pastito");
        pastitoABuscar = pastitos[Random.Range(0, pastitos.Length)];        
        buscar = true;
        agente.enabled = true;       
    }
    
}
