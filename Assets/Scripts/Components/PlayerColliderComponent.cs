using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderComponent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PostProccessingComponent p_component;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //StartCoroutine(p_component.Hurt());
        if (collision.collider.CompareTag("Enemy"))
        {
            StartCoroutine(p_component.Hurt());
        }
    }

}
