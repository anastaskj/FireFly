using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOfTerra : MonoBehaviour
{
    public GameObject startingForm;
    public List<GameObject> forms;


    private DropFire item;
    private GenerateLevelEnd end;

    private BossResources br;
    private ParticleSystem ps;
    private float hp = 0;

    // Start is called before the first frame update
    void Start()
    {
        forms = new List<GameObject>();
        GameObject instance = (GameObject)Instantiate(startingForm, transform.position, Quaternion.identity);
        instance.transform.parent = transform;
        forms.Add(instance);
        br = GetComponent<BossResources>();
        item = GetComponent<DropFire>();
        end = GetComponent<GenerateLevelEnd>();
        br.SetName("Spawn of Terra");

        FindObjectOfType<AudioManager>().TerraSpawnSound();

    }

    private void Update()
    {
        if (forms.Count == 0)
        {
            br.death = true;
            if (LevelManager.level != 0)
            {
                if (LevelManager.itemPool.Count > 0)
                {
                    item.SpawnItem();
                }
            }
            else
            {
                item.SpawnItem();
            }
            end.GenerateEnd();
            br.OnValueChanged.Invoke();
            Destroy(this.gameObject);
        }

        for (var i = forms.Count - 1; i > -1; i--)
        {
            if (forms[i] == null)
                forms.RemoveAt(i);
        }

        foreach (GameObject g in forms)
        {
            hp += g.GetComponent<SplittingBehavior>().health;
        }
        br.health = hp;
        br.OnValueChanged.Invoke();
        hp = 0;
    }
}
