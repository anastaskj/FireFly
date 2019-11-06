using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireInventory : MonoBehaviour
{
    public GameObject[] essenceTexts;
    public GameObject activeFire;
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject[] fires = new GameObject[3];
    public GameObject normalFire;
    private GameObject instance;
    private Color inactiveColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        inactiveColor.a = 0.6f;
        LevelManager.fires[0] = activeFire;
        LevelManager.fireIcons[0] = normalFire;
        LevelManager.essenceText[0] = "0";
        if (LevelManager.level != 0)
        {
            int count = 0;
            foreach (GameObject g in LevelManager.fires)
            {
                if (g != null)
                {
                    count += 1;
                }
            }
            for (int i = 0; i < count; i++)
            {
                fires[i] = LevelManager.fires[i];
                isFull[i] = true;
                essenceTexts[i].GetComponent<Text>().text = LevelManager.essenceText[i];
                Instantiate(LevelManager.fireIcons[i], slots[i].transform, false);
            }
            instance = Instantiate(activeFire);
        }
        else
        {
            instance = Instantiate(activeFire);
            isFull[0] = true;
            Instantiate(normalFire, slots[0].transform, false);
        }
        slots[0].GetComponent<Image>().color = Color.white;
        fires[0] = activeFire;
    }
    

    public void ChangeActive(GameObject newFire, int index)
    {
        Destroy(instance);
        activeFire = newFire;
        instance = Instantiate(activeFire);
        slots[index].GetComponent<Image>().color = Color.white;
        foreach (GameObject s in slots)
        {
            if (s != slots[index])
            {
                s.GetComponent<Image>().color = inactiveColor;
            }
        }
    }

    public void EmptyAnimation(int index)
    {
        slots[index].GetComponent<Animator>().SetBool("Alert", true);
        FindObjectOfType<AudioManager>().CantChangeFireSound();
        Invoke("CancelAnimation", 1f);
    }
    public void CancelAnimation()
    {
        foreach (GameObject s in slots)
        {
            s.GetComponent<Animator>().SetBool("Alert", false);
        }
    }
    public bool CheckResource(int index)
    {
        if (GetComponent<PlayerResources>().xp > fires[index].GetComponent<FireFlyResources>().xpPerSecond)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}

