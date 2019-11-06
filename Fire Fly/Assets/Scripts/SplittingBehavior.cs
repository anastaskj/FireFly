using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingBehavior : MonoBehaviour
{
    public GameObject spawnOfTerra;
    public GameObject nextVersion;
    public float xpDrop;
    public int version = 1;
    public float health = 3;

    private FireFlyResources fly;
    private PlayerResources pr;
    private Animator anim;
    private Rigidbody2D rb;
    private ParticleSystem[] ps;
    private BossResources br;
    private SpawnOfTerra terra;

    private float timeBtwDamage;
    private float startTimeBtwDamage;
    private Vector2 movement;

    //start off in a random direction
    //if you collide with the Room layer you bounce off
    //if you collide with the player you deal damage and bounce off

    void Start()
    {
        fly = GameObject.FindGameObjectWithTag("FireFly").GetComponent<FireFlyResources>();
        pr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        br = GetComponentInParent<BossResources>();
        startTimeBtwDamage = fly.rateOfFire;
        terra = GetComponentInParent<SpawnOfTerra>();
        ps = GetComponentsInChildren<ParticleSystem>();
        movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.velocity = movement*5;
    }

    private void Move()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = movement*5;
        movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        FindObjectOfType<AudioManager>().TerraCollideSound();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FireFly"))
        {
            if (timeBtwDamage <= 0)
            {
                if (health > 0f)
                {
                    br.TakeDamage(fly.damagePerTick);
                    health -= fly.damagePerTick;
                    ps[0].Play();
                }
                else
                {
                    pr.GainXp(xpDrop);
                    health -= fly.damagePerTick;
                    br.TakeDamage(fly.damagePerTick);
                    //when you die you instantiate 2 lesser versions of yourself unless you've reached the final version

                    
                    ps[0].Play();
                    ps[1].Play();
                    Invoke("DestroySegment", 0.25f);
                }
                timeBtwDamage = startTimeBtwDamage;
            }
            timeBtwDamage -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //redo this
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 dir = collision.GetContact(0).point;
            dir = -dir.normalized;
            pr.TakeDamage(this.gameObject);
            //pr.GetComponent<Rigidbody2D>().AddForce(dir * 600);
            Move();
        }
        else if (collision.gameObject.layer == 11 || collision.gameObject.tag == "Spawn of Terra")
        {
            Move();
        }
    }

    private void DestroySegment()
    {
        if (version < 4)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject instance = (GameObject)Instantiate(nextVersion, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)), Quaternion.identity) as GameObject;

                instance.transform.SetParent(GameObject.FindGameObjectWithTag("Boss").transform);
                terra.forms.Add(instance);
            }
        }
        FindObjectOfType<AudioManager>().TerraDeathSound();
        Destroy(this.gameObject);
    }
}
