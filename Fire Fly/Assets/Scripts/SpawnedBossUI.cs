using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnedBossUI : MonoBehaviour
{
    public GameObject bossHealth;
    public Canvas canvas;
   
    public void SpawnUI()
    {
        GameObject slider = Instantiate(bossHealth) as GameObject;
        slider.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }
}
