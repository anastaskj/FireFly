using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevelEnd : MonoBehaviour
{
    public GameObject end;
    private GameObject spawnPoint;

    public void GenerateEnd()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("EndSpawn");
        Instantiate(end, spawnPoint.transform.position, Quaternion.identity);

        FindObjectOfType<AudioManager>().PortalActivateSound();
    }
}
