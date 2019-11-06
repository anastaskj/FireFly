using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyResources : MonoBehaviour
{
    public float speed = 1f;
    public float damagePerTick = 0.5f; //starts at half a heart per tick
    public float rateOfFire = 0.5f; //starts at 2 ticks per second
    public Vector2 scale;
    public float xpPerSecond;
    public bool enoughXp;
    private float timeBtwUpdate;

    private float savedDamage;
    private PlayerResources pr;
    private FireInventory inv;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
    }
    private void Awake()
    {
        pr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<FireInventory>();
    }
    private void Update()
    {
        if (timeBtwUpdate<= 0)
        {
            if (pr.xp >= xpPerSecond)
            {
                pr.LoseXp(xpPerSecond);
            }
            else
            {
                inv.ChangeActive(inv.fires[0], 0);
            }
            timeBtwUpdate = 1;
        }
        else
        {
            timeBtwUpdate -= Time.deltaTime;
        }
        
    }

    public void Holster()
    {
        this.gameObject.layer = 15;
        anim = GetComponent<Animator>();
        anim.SetBool("RoomCleared", true);
    }

    public void Activate()
    {
        this.gameObject.layer = 0;
        anim = GetComponent<Animator>();
        anim.SetBool("RoomCleared", false);
    }

}
