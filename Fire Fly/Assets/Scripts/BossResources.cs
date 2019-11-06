using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossResources : MonoBehaviour
{
    public UnityEvent OnValueChanged = new UnityEvent();
    public float health = 0;
    public bool death = false;
    public string bossname;

   

    private void Start()
    {
       
    }

    public void TakeDamage(float damage)
    {
        if (health >= 0f)
        {
            health -= damage;
            OnValueChanged.Invoke();
        }
        else
        {
            health -= damage;
            OnValueChanged.Invoke();
        }
    }

    public void SetName(string name)
    {
        this.bossname = name;
        OnValueChanged.Invoke();
    }

    public void GainHealth(float health)
    {
        this.health += health;
        OnValueChanged.Invoke();
    }


}
