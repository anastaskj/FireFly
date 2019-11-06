using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager 
{
    public static int level = 0;
    public static float armor = 0;
    public static float exp = 0;
    public static float health = 0;

    public static GameObject[] fires = new GameObject[3];
    public static GameObject[] fireIcons = new GameObject[3];
    public static string[] essenceText = new string[3];

    public static List<GameObject> itemPool = new List<GameObject>();

    public static void LoadNewLevel()
    {
        level++;
        SceneManager.LoadScene(1); 
    }
}
