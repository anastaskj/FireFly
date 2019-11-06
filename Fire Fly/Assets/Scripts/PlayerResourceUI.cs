using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourceUI : MonoBehaviour
{
    public Slider healthBar;
    public Slider armorBar;
    public Slider xpBar;
    public PlayerResources res;

    private void Start()
    {
        //res = player.GetComponent<PlayerResources>();
    }

    public void updateValue()
    {
        if (healthBar != null || armorBar != null || xpBar != null)
        {
            if (healthBar.CompareTag("Health"))
            {
                healthBar.value = res.health;
            }
            if (armorBar.CompareTag("Armor"))
            {
                armorBar.value = res.armor;
            }
            if (xpBar.CompareTag("XP"))
            {
                xpBar.value = res.xp;
            }
        }
    }

    public void OnEnable()
    {
        updateValue();
        res.OnValueChanged.AddListener(updateValue);
    }

    private void OnDisable()
    {
        res.OnValueChanged.RemoveListener(updateValue);
    }
}
