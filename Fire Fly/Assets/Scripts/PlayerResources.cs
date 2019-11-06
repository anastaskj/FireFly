using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class PlayerResources : MonoBehaviour
{
    public UnityEvent OnValueChanged = new UnityEvent();
    private FireFlyResources fire;
    private FireInventory inventory;
    public float health = 3;
    public float armor = 0;
    public float xp = 0;
    public float maxXp = 100;
    public float maxArmor = 3;
    public float invinsibleAmount;

    private Animator anim;
    private bool invincible = false;
    private bool death = false;
    private void Start()
    {
        inventory = GetComponent<FireInventory>();
        if (LevelManager.level != 0)
        {
            xp = LevelManager.exp;
            armor = LevelManager.armor;
            health = LevelManager.health;
            OnValueChanged.Invoke();
        }
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(GameObject damager)
    {
        if (!invincible)
        {
           
            fire = GameObject.FindGameObjectWithTag("FireFly").GetComponent<FireFlyResources>();
            if (health > 0.5f)
            {
                GetComponent<SpriteRenderer>().color = Color.HSVToRGB(0, 100, 100);
                if (armor > 0.5f && damager.CompareTag("FireFly"))
                {
                    armor -= fire.damagePerTick;
                    
                }
                else if (armor > 0 && !damager.CompareTag("FireFly"))
                {
                    armor -= 0.5f;
                }
                else if (armor <= 0)
                {
                    health -= 0.5f;
                }
                if (!death)
                {
                    FindObjectOfType<AudioManager>().TakeDamagePlayerSound();
                }
                OnValueChanged.Invoke();
            }
            else
            {
                if (damager.CompareTag("FireFly"))
                {
                    anim.SetBool("DeathFire", true);
                    FindObjectOfType<AudioManager>().DeathPlayerFireSound();
                }
                else
                {
                    anim.SetBool("DeathNormal", true);
                    FindObjectOfType<AudioManager>().DeathPlayerSound();
                }
                health -= fire.damagePerTick;
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                OnValueChanged.Invoke();
                death = true;
                Invoke("DeathScreen", 2.5f);
            }
            invincible = true;
            Invoke("resetInvulnerability", invinsibleAmount);
        }
    }

    public void GainHealth(float hp)
    {
        this.health += hp;
        if (this.health > 3)
        {
            health = 3;
        }
        OnValueChanged.Invoke();
        FindObjectOfType<AudioManager>().HealthPickupSound();
    }

    public void GainArmor(float armor)
    {
        this.armor += armor;
        if (this.armor > maxArmor)
        {
            armor = maxArmor;
        }
        OnValueChanged.Invoke();
        FindObjectOfType<AudioManager>().ArmorPickupSound();
    }

    public void GainXp(float xp)
    {
        this.xp += xp;
        if (this.xp > maxXp)
        {
            xp = maxXp;
        }
        OnValueChanged.Invoke();
    }
    
    public void LoseXp(float xp)
    {
        this.xp -= xp;
        if (xp <= 0)
        {
            xp = 0;
        }
        OnValueChanged.Invoke();
    }

    public void NextLevel()
    {
        LevelManager.exp = this.xp;
        LevelManager.armor = this.armor;
        LevelManager.health = this.health;
        LevelManager.LoadNewLevel();
    }


    private void DeathScreen()
    {
        Cursor.visible = true;
        LevelManager.level = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void resetInvulnerability()
    {
        invincible = false;
        GetComponent<SpriteRenderer>().color = Color.HSVToRGB(0, 0, 100);
    }
}
