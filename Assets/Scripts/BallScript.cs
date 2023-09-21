using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float ballForce;
    bool gameStarted = false;
    public UIManager ui;
    public Transform paddle;
    public Transform powerup;
    public Transform explosion;
    AudioSource audio;

    // Start is called before the first frame update

    void Start()
    {
        ui = GameObject.FindWithTag("ui").GetComponent<UIManager>();
        audio = GetComponent <AudioSource> ();
        
    }
    // Update is called once per frame
    void Update()
    {
        if(ui.gameOver) {
            return; 
        }
        if(gameStarted==false)
        {
            transform.position = paddle.position;
        }
        if(Input.GetKeyUp(KeyCode.Space) && gameStarted==false)
        {
            rb.isKinematic = false;
            rb.AddForce(new Vector2(ballForce, ballForce));
            gameStarted = true;
        }
    }

    void OnTriggerEnter2D(Collider2D C)
    {
        if(C.CompareTag("bottom"))
        {
            ui.UpdateLives(-1);
            rb.velocity = Vector2.zero;
            gameStarted = false;
        }
    }

    //Brick behaviour

    void OnCollisionEnter2D(Collision2D C)
    {
        if(C.transform.CompareTag("bricks"))
        {
            BrickScript brickScript = C.gameObject.GetComponent<BrickScript>();
            if(brickScript.hitsToBreak > 1)
            {
                brickScript.BreakBrick ();
            }
            else {

            Transform newExplosion = Instantiate(explosion,C.transform.position,C.transform.rotation);
            Destroy(newExplosion.gameObject, 2.5f);


            int randChance = Random.Range(1,101);
            if(randChance < 30){
                Instantiate(powerup,C.transform.position,C.transform.rotation);
            }
            ui.UpdateNumberOfBricks ();
            
    

           Destroy (C.gameObject);
            }

            audio.Play ();
        
        }
    }
}



