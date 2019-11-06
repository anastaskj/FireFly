using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float startTimeBtwAttack;
    public float attackRange;
    public LayerMask whatIsPlayer;

    private float timeBtwAttack;
    private GameObject player;

    private EnemyBehavior enemy;
    private Rigidbody2D body;
    private PlayerController pc;
    private Animator anim;
    //private Collider2D attackTrigger;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyBehavior>();
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        body = GetComponentInParent<Rigidbody2D>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (enemy.attackReady)
        {
            if (timeBtwAttack <= 0)
            {
                Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(player.transform.position, attackRange, whatIsPlayer);
                for (int i = 0; i < playerToDamage.Length; i++)
                {
                    playerToDamage[i].GetComponent<PlayerResources>().TakeDamage(this.gameObject);
                }
                enemy.attackReady = false;
                FindObjectOfType<AudioManager>().LasherAttackSound();
                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 dir = collision.GetContact(0).point;
            dir = -dir.normalized;
            pc.TakeDamage(this.gameObject);
            player.GetComponent<Rigidbody2D>().AddForce(dir * 600);
        }
    }

   

}
