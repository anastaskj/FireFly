using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrail : MonoBehaviour
{
    public GameObject scorchFire;
    public float timeBtwSpawn = 0.5f;
    private float startTimeBtwSpawn;

    // Update is called once per frame
    void Update()
    {
        if (startTimeBtwSpawn <= 0)
        {
            GameObject instance =  Instantiate(scorchFire, this.transform.position, Quaternion.identity);
            startTimeBtwSpawn = timeBtwSpawn;
        }
        else
        {
            startTimeBtwSpawn -= Time.deltaTime;
        }
    }
}
