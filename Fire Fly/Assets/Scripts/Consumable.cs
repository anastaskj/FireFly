using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    private PlayerResources pr;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pr = collision.gameObject.GetComponent<PlayerResources>();
            
            if (this.tag == "HealthConsumable")
            {
                if (pr.health < 3)
                {
                    pr.GainHealth(0.5f);
                    Destroy(this.gameObject);
                }
            }
            if (this.tag == "ArmorConsumable")
            {
                if (pr.armor < pr.maxArmor)
                {
                    pr.GainArmor(0.5f);
                    Destroy(this.gameObject);
                }
            }
        }
    }
    
}
