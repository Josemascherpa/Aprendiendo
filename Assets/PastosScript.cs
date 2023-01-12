using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastosScript : MonoBehaviour
{
    public List<GameObject> pastos;
    // Start is called before the first frame update
    private void Awake()
    {
        
        for(int i = 0; i < this.transform.childCount;i++)
        {
            var pasto = transform.GetChild(i).gameObject;
            pastos.Add(pasto);
        }
    }    

   
}
