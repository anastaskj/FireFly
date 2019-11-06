using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private PlayerController pc;

    // Use this for initialization
    void Start()
    {
        this.transform.position = player.transform.position + new Vector3(0, 0, -30); //Needs fixing
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;

        pc = player.GetComponent<PlayerController>();

    }
    

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (pc.InRoom)
        {
            if(pc.currentRoom != null)
            {
                transform.position = pc.currentRoom.transform.position + new Vector3(0, 0, -30);
            }
        }
        else
        {
            transform.position = player.transform.position + offset;
        }
    }
}
