using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    /*
     Enemies will spawn in the room whenever you reach its middle. <--spawn is in SpawnObject script
     They will move towards you until they are in range. <-- public range variable for the prefab
     This script will be accompanied by a script that will handle the attack of the enemy.
    */
    public float soundTimer = 1f;
    public float detectRange;
    public float movementRange;
    public bool attackReady;
    public LayerMask whatIsWall;

    private float soundTimerStart = 0f;
    private EnemyResources er;
    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 moveposition;
    private PlayerResources pr;
    private Animator anim;
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        er = GetComponent<EnemyResources>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        attackReady = false;
        pr = player.GetComponent<PlayerResources>();
        anim = GetComponent<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        moveposition = player.transform.position - transform.position;
        RaycastHit2D hit = CheckWall(moveposition);
        //Debug.Log(hit.normal);
        if (!hit.collider)
        {

            if (Vector2.Distance(player.transform.position, transform.position) > er.range)
            {
                transform.position = Vector2.MoveTowards(rb.position, player.transform.position, er.speed * Time.deltaTime);
                attackReady = false;
                if (this.gameObject.tag == "Pixie" || this.gameObject.tag == "Lasher")
                {
                    anim.SetBool("Attacking", false);
                    anim.SetFloat("Rotation", moveposition.x); //fix
                }
                else if (this.gameObject.tag == "PixieTree")
                {
                    anim.SetBool("Attacking", false);
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
                if (this.gameObject.tag == "Pixie" || this.gameObject.tag == "Lasher" || this.gameObject.tag == "PixieTree")
                {
                    anim.SetBool("Attacking", true);
                }
                attackReady = true;
            }
        }
        else
        {
            MoveRandomly(hit);
        }

        if (soundTimerStart > 0)
        {
            soundTimerStart -= Time.deltaTime;

        }
        else
        {
            if (this.tag == "Pixie" || this.tag == "PixieTree")
            {

                FindObjectOfType<AudioManager>().PixieWalkSound();
            }
            else if (this.tag == "Lasher")
            {
                FindObjectOfType<AudioManager>().LasherWalkSound();
            }
            else if (this.gameObject.layer == 10)
            {
                FindObjectOfType<AudioManager>().WispSound();
            }
            soundTimerStart = soundTimer;
        }
    }

    public void TakeDamage()
    {
        ps.Play();
        er.TakeDamage();
    }

    private void MoveRandomly(RaycastHit2D hit)
    {
        //rb.velocity = new Vector3(Random.Range(-movementRange, movementRange), Random.Range(-movementRange, movementRange));
        if (hit.normal == new Vector2(0f,1f)) //there is a wall on the right
        {
            rb.velocity = new Vector3(-1 * movementRange, 1); //go left
        }
        else if (hit.normal == new Vector2(0f, -1f))
        {
            rb.velocity = new Vector3(1, -1 * movementRange);
        }
        else if (hit.normal == new Vector2(1f, 0f)) //there is a wall on the left
        {
            rb.velocity = new Vector3(1 * movementRange, -1); //go right
        }
        else if (hit.normal == new Vector2(-1f, 0f))
        {
            rb.velocity = new Vector3(-1, 1 * movementRange);
        }
        else if (hit.normal == new Vector2(0f, 0f))
        {
            rb.velocity = new Vector3(Random.Range(-movementRange, movementRange), Random.Range(-movementRange, movementRange));
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pr.TakeDamage(this.gameObject); 
            if (this.gameObject.layer == 10)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private RaycastHit2D CheckWall(Vector2 direction)
    {
       return Physics2D.Raycast(this.transform.position, direction, detectRange, whatIsWall);
    }
   
}
