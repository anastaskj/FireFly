using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResources : MonoBehaviour
{
    public float speed;
    public double health;
    public float range;
    public float xpDrop;

    public GameObject healthPickup;
    public GameObject armorPickup;

    private FireFlyResources firefly;
    private PlayerResources player;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        firefly = GameObject.FindGameObjectWithTag("FireFly").GetComponent<FireFlyResources>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        firefly = GameObject.FindGameObjectWithTag("FireFly").GetComponent<FireFlyResources>();
    }

    public void TakeDamage()
    {
        if (health > firefly.damagePerTick)
        {
            health -= firefly.damagePerTick;
        }
        else
        {
            health -= firefly.damagePerTick;
            player.GainXp(xpDrop);
            if (player.xp > player.maxXp)
            {
                player.xp = player.maxXp;
            }

            if (this.gameObject.layer != 10)
            {
                DropConsumable();
            }
            //death animation
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            this.GetComponent<EnemyBehavior>().enabled = false;
            FindObjectOfType<AudioManager>().EnemyDeathFireSound();
            anim.SetBool("Death", true);
            Invoke("EnemyDeath", 0.5f);
        }
    }

    private void DropConsumable()
    {
        int rand = Random.Range(0, 101);
        if (rand <= 20)
        {
            Instantiate(healthPickup, this.transform.position, Quaternion.identity);
        }
        else if (rand > 20 && rand <= 40)
        {
            Instantiate(armorPickup, this.transform.position, Quaternion.identity);
        }
    }

    private void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
