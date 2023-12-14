using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableOnStartUp : MonoBehaviour
{
    public GameObject disableThing;
    // Start is called before the first frame update
    void Start()
    {
        disableThing.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
