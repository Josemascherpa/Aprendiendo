using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPastos : MonoBehaviour
{
    [SerializeField] private GameObject pasto;
    [SerializeField] private int cantidadPastos=60;
    [SerializeField] private GameObject parentPastos;
    [SerializeField]private GameObject minX;
    [SerializeField] private GameObject maxX;
    [SerializeField] private GameObject minZ;
    [SerializeField] private GameObject maxZ;
    void Start()
    {
        for (int i=0;i<cantidadPastos;i++)
        {
            Instantiate(pasto, new Vector3(Random.Range(minZ.transform.position.x, maxZ.transform.position.x),0.985f,Random.Range(minX.transform.position.z, maxX.transform.position.z)),Quaternion.identity,parentPastos.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
