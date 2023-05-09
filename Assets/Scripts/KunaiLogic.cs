using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiLogic : MonoBehaviour
{

    public float SPEED = 19f;
    public Rigidbody2D rb;

    private void Start()
    {
        if(rb == null)
        rb = GetComponent<Rigidbody2D>();

        
        rb.velocity = transform.right * SPEED;
    }

    //make sure kunai is gone when its off screen
    private void Update()
    {
        if(transform.position.x > 200 || transform.position.x<-200)
        {
            Destroy(gameObject);
        }
    }

 
}
