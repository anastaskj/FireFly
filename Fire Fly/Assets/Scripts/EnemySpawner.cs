using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] barrierPoints;
    public bool clearedRoom;
    public bool enteredRoom;
    public List<GameObject> enemies;
    public List<GameObject> barriers;

    //to be replaced with bar + name

    private Transform room;
    private PlayerController pc;
    private SpawnedBossUI ui;
    private bool spawned;
    // Start is called before the first frame update
    void Start()
    { 
        clearedRoom = false;
        enteredRoom = false;
        spawned = false;
        room = GetComponentInParent<Transform>();
        if (this.gameObject.tag == "BossRoom")
        {
            ui = GetComponent<SpawnedBossUI>();
        }
    }
    private void Update()
    {
        if (enteredRoom)
        {
            if (enemies.Count > 0)
            {
                for (var i = enemies.Count - 1; i > -1; i--)
                {
                    if (enemies[i] == null)
                        enemies.RemoveAt(i);
                }
            }
            else
            {
                clearedRoom = true;
                DestroyBarrier();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (!clearedRoom && !spawned)
            {
                Invoke("SpawnBarrier", 0.5f);
                Invoke("Spawn", 0.5f);
                spawned = true;
                
            }
        }
    }

    private void Spawn()
    {
        enemies = new List<GameObject>();
        foreach (GameObject sp in spawnPoints)
        {
            sp.GetComponent<SpawnObject>().SpawnEnemy();
        }
        if (this.gameObject.tag == "BossRoom")
        {
            ui.SpawnUI();
        }
    }
    private void SpawnBarrier()
    {
        enteredRoom = true;
        barriers = new List<GameObject>();
        foreach (GameObject sp in barrierPoints)
        {
            sp.GetComponent<SpawnObject>().SpawnBarrier();
        }
    }
    private void DestroyBarrier()
    {
        for (int i = 0; i < barriers.Count; i++)
        {
            Destroy(barriers[i]);
        }
    }
}
