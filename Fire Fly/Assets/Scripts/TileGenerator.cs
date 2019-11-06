using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    public Tilemap background;
    public Tile[] ground;
    public Tile[] groundLevel2;

    // Start is called before the first frame update
    void Start()
    {
        int rand;
        if (LevelManager.level >=1)
        {
            ground = groundLevel2;
        }
        foreach (Vector3Int p in background.cellBounds.allPositionsWithin)
        {
            rand = Random.Range(0, ground.Length);

            background.SetTile(p, ground[rand]);
        }
        Destroy(this);
       
    }
}
