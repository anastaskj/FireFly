using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFly : MonoBehaviour
{
    private FireFlyResources resources;
    private PlayerController pc;
    private float timeBtwAttack;

    private void Start()
    {
        //pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        resources = GetComponent<FireFlyResources>();
    }

    private void Awake()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        resources = GetComponent<FireFlyResources>();
        FindObjectOfType<AudioManager>().StaticFireSound();
    }

    private void OnEnable()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        resources = GetComponent<FireFlyResources>();
        FindObjectOfType<AudioManager>().AppearFireSound();
    }

    



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (timeBtwAttack <= 0)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                pc.TakeDamage(this.gameObject);
            }
            else if (collision.gameObject.layer == 12 || collision.gameObject.layer == 10)
            {
                collision.gameObject.GetComponent<EnemyBehavior>().TakeDamage();
            }
            FindObjectOfType<AudioManager>().AttackFireSound();
            timeBtwAttack = resources.rateOfFire;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
}
