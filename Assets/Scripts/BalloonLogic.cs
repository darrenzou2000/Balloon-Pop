using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BalloonLogic : MonoBehaviour
{


    [SerializeField] float growSize = 0.1f;

    [SerializeField] Text scoreText;
    //this is to get the persistant data
    [SerializeField] PersistantData persistantData;

    
    Animator animator;

    //to fix double collides
    bool hit = false;

    SpriteRenderer sr;
    int count = 75;
    int BASECOUNT = 100;
    int score = 100;

    
    // Start is called before the first frame update

    public GameObject balloon;
    AudioSource poppingSound;
    
    void Start()
    {
        if(balloon == null)
        {
            balloon = GetComponent<GameObject>();
            sr = gameObject.GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            poppingSound = GetComponent<AudioSource>();

            persistantData = PersistantData.instance;
            persistantData.DisplayScore();

            Debug.Log("difficulty:" + persistantData.difficulity + "   name:" + persistantData.username);
            //sets the difficulty which is just how long player have to complete level
            switch (persistantData.difficulity) {
                case "easy": 
                    BASECOUNT = 30;
                    score = 20;
                    break;
                case "hard":
                    BASECOUNT = 15;
                    score = 40;
                    break;
                case "impossible":
                    BASECOUNT = 7;
                    break;
                default:
                    BASECOUNT = 20;
                    score = 80;
                    break;
            }


        }
    }

    private void FixedUpdate()
    {
        count -= 1;
        if (count == 0)
        { 
            transform.localScale += new Vector3(growSize, growSize, 0);
            count = BASECOUNT;
            score -= 1;
        }

        if (transform.lossyScale.y > 4)
        {
             sr.color= Color.red;
        }
        if (transform.lossyScale.y > 5 && hit==false) {
            Destroy(gameObject);
            persistantData.score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision detected");
        if (collision.gameObject.tag == "Kunai")
        {
            if (hit) return;
            
            hit = true;
            persistantData.AddScore(score);
            Destroy(collision.gameObject);
            poppingSound.Play();
            this.GetComponent<BoxCollider2D>().enabled = false;
            animator.SetBool("Popped", true);
            Invoke("nextLevel", 3);
        }
    }

    void nextLevel()
    {
        Debug.Log("Delay done");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
