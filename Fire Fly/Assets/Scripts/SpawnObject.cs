using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject[] objects2;
    public GameObject[] enemies;
    private EnemySpawner es;
    // Start is called before the first frame update
    void Start()
    {
        if (!this.CompareTag("EnemySpawner") && !this.CompareTag("Barrier"))
        {
            GameObject instance;
            int rand;
            if (LevelManager.level == 0)
            {
                rand = Random.Range(0, objects.Length);
                instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
                instance.transform.parent = transform;
            }
            else if (LevelManager.level >= 1)
            {
                rand = Random.Range(0, objects2.Length);
                instance = (GameObject)Instantiate(objects2[rand], transform.position, Quaternion.identity);
                instance.transform.parent = transform;
            }
        }
    }

   public void SpawnEnemy()
   {
        es = GetComponentInParent<EnemySpawner>();
        int rand = Random.Range(0, enemies.Length);
        GameObject instance = (GameObject)Instantiate(enemies[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
        es.enemies.Add(instance);
    }
    public void SpawnBarrier()
    {
        es = GetComponentInParent<EnemySpawner>();
        GameObject instance = null;
        int rand;
        if (LevelManager.level == 0)
        {
            rand = Random.Range(0, objects.Length);
            instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;

        }
        else if (LevelManager.level >= 1)
        {
            rand = Random.Range(0, objects2.Length);
            instance = (GameObject)Instantiate(objects2[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
        }

        FindObjectOfType<AudioManager>().BarrierSpawnSound();
        es.barriers.Add(instance);
    }
}
