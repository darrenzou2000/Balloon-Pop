using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class doodLogic : MonoBehaviour
{

    bool hit = false;
    [SerializeField ] PersistantData persistantData;


    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision detected");
        if (collision.gameObject.tag == "Kunai")
        {
            if (hit) return;

            //stops background music
            GameObject.FindGameObjectWithTag("background").GetComponent<AudioSource>().Stop();

            transform.Rotate(0, 0, 90);
            
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10, ForceMode2D.Impulse);
            hit = true;
            persistantData.AddScore(-9999);
            Destroy(collision.gameObject);
            this.GetComponent<AudioSource>().Play();    
            this.GetComponent<Animator>().SetBool("dead", true);
        }
    }
}
