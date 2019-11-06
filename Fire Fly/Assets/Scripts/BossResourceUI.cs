using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossResourceUI : MonoBehaviour
{
    private Slider bar;
    private BossResources res;
    private Text boss;

    private void Start()
    {
        bar = GameObject.FindGameObjectWithTag("BossHealth").GetComponent<Slider>();
        res = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossResources>();
        boss = GameObject.FindGameObjectWithTag("BossName").GetComponent<Text>();
        boss.text = res.bossname;
        bar.maxValue = res.health;
        bar.value = bar.maxValue;

    }

    public void updateValue()
    {
        bar = GameObject.FindGameObjectWithTag("BossHealth").GetComponent<Slider>();
        res = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossResources>();
        boss = GameObject.FindGameObjectWithTag("BossName").GetComponent<Text>();
        boss.text = res.bossname;
        if (bar != null)
        {
            if (bar.maxValue <= res.health)
            {
                bar.maxValue = res.health;
            }
            bar.value = res.health;
            if (res.death)
            {
                Invoke("OnDeath", 1f);
            }
        }
    }

    private void OnDeath()
    {
        Destroy(this.gameObject);
        //this.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        updateValue();
        res.OnValueChanged.AddListener(updateValue);
        //Debug.Log("enabled");
    }

    private void OnDisable()
    {
        res.OnValueChanged.RemoveListener(updateValue);
        //Debug.Log("disabled");
    }
}
