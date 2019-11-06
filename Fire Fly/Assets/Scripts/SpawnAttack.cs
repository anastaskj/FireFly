using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAttack : MonoBehaviour
{
    public float attackCd = 1.5f;
    public GameObject entity;
    public float spawnRange;

    private EnemyBehavior enemy;
    private bool attacking = false;
    private float attackTimer = 0f;
    private Rigidbody2D body;
    private Animator anim;
    private Vector2 randomMovement;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyBehavior>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking && enemy.attackReady)
        {
            attacking = true;
            attackTimer = attackCd;
            //Debug.Log(attackTimer);
        }
        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
               
            }
            else
            {
                SpawnEntity();
                attacking = false;
            }
        }
    }

    void SpawnEntity()
    {
      GameObject instance = Instantiate(entity, transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-0.5f, 0.5f)), Quaternion.identity);
        instance.transform.parent = this.transform;
        FindObjectOfType<AudioManager>().PixieAttackSound();
    }




}
