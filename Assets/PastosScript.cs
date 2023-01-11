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

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < pastos.Count; i++)
            {
                print(pastos[i].name);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            var random = Random.Range(0, pastos.Count);
            Destroy(pastos[random].gameObject);
            pastos.Remove(pastos[random]);
        }*/
        
    }
}
