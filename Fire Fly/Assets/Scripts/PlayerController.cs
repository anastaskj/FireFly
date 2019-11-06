using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public bool InRoom;
    public Collider2D currentRoom;
    public float soundTimer = 0.3f;
   
    private PlayerResources pr;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private PlayerInput input;
    private ParticleSystem ps;
    private EnemySpawner es;
    private Animator anim;
    private float startSoundTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        pr = GetComponent<PlayerResources>();
        ps = GetComponentInChildren<ParticleSystem>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        moveVelocity = input.GetMoveInput().normalized * speed;
        anim.SetFloat("Direction", moveVelocity.x);
        if (input.GetMoveInput() != Vector2.zero)
        {
            if (startSoundTimer > 0)
            {
                startSoundTimer -= Time.deltaTime;

            }
            else
            {
                FindObjectOfType<AudioManager>().StepsPlayerSound();
                FindObjectOfType<AudioManager>().ClothMovementSound();
                startSoundTimer = soundTimer;
            }
        }
       

    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
       
    }

    public void TakeDamage(GameObject damager)
    {
        //pr = GetComponent<PlayerResources>();
        pr.TakeDamage(damager);
        if (damager.tag == "FireFly")
        {
            ps.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            InRoom = true;
            currentRoom = collision;
        }
        else if(collision.gameObject.tag == "EndLevel")
        {
            pr.NextLevel();
            FindObjectOfType<AudioManager>().PortalDeactivateSound();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            
            es = currentRoom.gameObject.GetComponent<EnemySpawner>();
            if (es != null)
            {
                if (es.clearedRoom)
                {
                    
                    HolsterAll();
                }
                else
                {
                    
                    ActivateAll();
                }
            }
            else
            {
                
                HolsterAll();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            InRoom = false;
            currentRoom = null;
        }
    }

    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    private void HolsterAll()
    {
        GameObject[] activeFires = GameObject.FindGameObjectsWithTag("FireFly");
        foreach (GameObject a in activeFires)
        {
            a.GetComponent<FireFlyResources>().Holster();
        }
    }

    private void ActivateAll()
    {
        GameObject[] activeFires = GameObject.FindGameObjectsWithTag("FireFly");
        foreach (GameObject a in activeFires)
        {
            a.GetComponent<FireFlyResources>().Activate();
        }
    }
}

