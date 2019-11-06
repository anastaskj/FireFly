using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public Sprite bossSprite;
    public Sprite game;
    public BorderController bc;
    public LevelGeneration levelGen;

    private SpriteRenderer sr;
    private Vector2 bossPosition;
    private Vector2 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        //place the game sprite on top of the room
        this.sr.sprite = game;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {

        if (bc.genеrаtionCompletion && this.sr.sprite !=bossSprite)
        {
            bossPosition = levelGen.lastRoom.transform.position;
            spawnPosition = this.transform.position;

            if (bossPosition.ToString() == spawnPosition.ToString())
            {
                this.sr.sprite = bossSprite;
            }
        }
    }

}
