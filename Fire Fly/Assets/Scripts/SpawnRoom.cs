using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LayerMask room;
    public LevelGeneration levelGen;
    public GameObject border;

    private BorderController controls;

    private void Start()
    {
        controls = border.GetComponent<BorderController>();
    }
    // Update is called once per frame
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
        if (roomDetection == null && controls.genеrаtionCompletion)
        {
            int rand = Random.Range(0, levelGen.rooms.Length);
            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
        }
    }
}
