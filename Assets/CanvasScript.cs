using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] private GameObject b_Play;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Play()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        b_Play.SetActive(false);
    }
}

