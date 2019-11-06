using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject mapIcon;
    public GameObject map;
    public GameObject health;
    public GameObject fires;
    private FireInventory inventory;

    //public GameObject fire;

    //private FireFly normalFire;
    //private LightningSpread lightningFire;
    //private FireTrail scorchFire;
    private Animator mapAnim;
    private PlayerController pc;
    //private EnemySpawner es;
    private bool activeMap;
    //private Animator anim;
    //private bool activeFire;

    // Start is called before the first frame update
    void Start()
    {
        activeMap = false;
        //activeFire = true;
        //anim = fire.GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
        inventory = GetComponent<FireInventory>();
        mapAnim = mapIcon.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            getOtherInput();
        }
    }

    public Vector2 GetMoveInput()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        return moveInput;
    }

    public void getOtherInput()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!activeMap)
            {
                mapAnim.SetBool("CloseMap", true);
                Invoke("MapIconToggle", 1.25f);
                map.SetActive(true);
                health.SetActive(false);
                fires.SetActive(false);
            }
            else
            {
                mapAnim.SetBool("CloseMap", false);
                Invoke("MapIconToggle", 0.5f);
                map.SetActive(false);
                health.SetActive(true);
                fires.SetActive(true);
            }
            activeMap = !activeMap;
            FindObjectOfType<AudioManager>().MapToggleSound();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            inventory.ChangeActive(inventory.fires[0], 0);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (inventory.fires[1] != null && inventory.CheckResource(1))
            {
                inventory.ChangeActive(inventory.fires[1], 1);
            }
            else
            {
                inventory.EmptyAnimation(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (inventory.fires[2] != null && inventory.CheckResource(2))
            {
                inventory.ChangeActive(inventory.fires[2], 2);
            }
            else
            {
                inventory.EmptyAnimation(2);
            }
        }
    }
    private void MapIconToggle()
    {
        mapIcon.SetActive(!mapIcon.activeSelf);
    }
      
}
