using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int points;

    public UIManager ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindWithTag("ui").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D C)
    {
        if(C.gameObject.tag=="Ball")
        {

            ui.IncrementScore();
           
            
        }
    }
    `
    public int hitsToBreak;
    public Sprite hitSprite;


    public void BreakBrick()
        {
            hitsToBreak--;
            GetComponent<SpriteRenderer> ().sprite = hitSprite;


        }
    


}
