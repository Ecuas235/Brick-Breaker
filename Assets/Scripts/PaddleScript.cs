using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaddleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float speed;
    public float maxX;
    public UIManager ui;
    void Start()
    {
        
    }

    void MoveLeft()
    {
        rb.velocity = new Vector2(-speed, 0);

    }
    void MoveRight()
    {
        rb.velocity = new Vector2(speed, 0);
    }
    void Stop()
    {
        rb.velocity = Vector2.zero;
    }
    // Update is called once per frame
    void Update()
    {
        if (ui.gameOver) {
            return;
        }
        float x = Input.GetAxis("Horizontal");
        if(x > 0)
        {
            MoveRight();
        }
        if (x < 0)
        {
            MoveLeft();
        }
        if(x == 0)
        {
            Stop();
        }
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -maxX, maxX);
        transform.position = pos;
    }

   void OnTriggerEnter2D(Collider2D other)
   {
       if (other.CompareTag ("ExtraLife")){
           ui.UpdateLives(1);
           Destroy(other.gameObject);
       }
       
   }
}
