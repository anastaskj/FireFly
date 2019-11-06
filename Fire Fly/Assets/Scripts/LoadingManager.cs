using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    //public GameObject resources;
    public GameObject UIPanel;
    public GameObject loadingScreen;
    public Slider loadingBar;
    public LevelGeneration levelGen;
    public BorderController borders;
    public GameObject player;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        loadingBar.maxValue = 25;
        player.SetActive(false);
        UIPanel.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        if (!borders.genеrаtionCompletion)
        {
            if (time <= 0)
            {
                loadingBar.value += 1;

                time = levelGen.startTimeBtwRoom;
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
        else
        {
            player.SetActive(true);
            loadingBar.value = loadingBar.maxValue;
            loadingScreen.SetActive(false);
            UIPanel.SetActive(true);
            gameObject.SetActive(false);
        }
       
        
    }
}
