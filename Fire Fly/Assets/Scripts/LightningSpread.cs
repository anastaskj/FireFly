using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpread : MonoBehaviour
{

    public float rangeOfSpread;

    private FireFlyResources resources;
    private PlayerController pc;
    private float timeBtwAttack;

    private void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        resources = GetComponent<FireFlyResources>();
    }

    private void Awake()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        resources = GetComponent<FireFlyResources>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (timeBtwAttack <= 0)
        {
            SpreadDamage(collision.gameObject);
            timeBtwAttack = resources.rateOfFire;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void SpreadDamage(GameObject primaryTarget)
    {
        Collider2D[] entitiesToDamage = Physics2D.OverlapCircleAll(primaryTarget.transform.position, rangeOfSpread);
        for (int i = 0; i < entitiesToDamage.Length; i++)
        {
            if (entitiesToDamage[i].tag == "Player")
            {
                entitiesToDamage[i].GetComponent<PlayerController>().TakeDamage(this.gameObject);
            }
            else if (entitiesToDamage[i].gameObject.layer == 12 || entitiesToDamage[i].gameObject.layer == 10)
            {
                entitiesToDamage[i].GetComponent<EnemyBehavior>().TakeDamage();
            }
        }
    }

}
