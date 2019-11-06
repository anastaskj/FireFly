using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirePickup : MonoBehaviour
{
    public GameObject itemImage;
    public GameObject fire;
    private FireInventory inventory;

    // Start is called before the first frame update
    //void Start()
    //{
    //    inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<FireInventory>();
    //}
    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<FireInventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (!inventory.isFull[i])
                {
                    inventory.isFull[i] = true;
                    inventory.fires[i] = fire; //the fire object is now connected to the icon

                    inventory.essenceTexts[i].GetComponent<Text>().text = fire.GetComponent<FireFlyResources>().xpPerSecond.ToString();
                    LevelManager.essenceText[i] = inventory.essenceTexts[i].GetComponent<Text>().text;
                    LevelManager.fires[i] = fire;
                    LevelManager.fireIcons[i] = itemImage;
                    Instantiate(itemImage, inventory.slots[i].transform, false);
                    FindObjectOfType<AudioManager>().PickupFireSound();
                    Destroy(this.gameObject);
                    break;
                }
            }
        }
    }

}
