using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFire : MonoBehaviour
{
    public GameObject[] itemPool;

    private void Start()
    {
        if (LevelManager.level != 0)
        {
            itemPool = LevelManager.itemPool.ToArray();
        }
    }
    public void SpawnItem()
    {
        int rand = Random.Range(0, itemPool.Length);
        Instantiate(itemPool[rand], this.transform.position, Quaternion.identity);
        itemPool[rand] = null;
        ClearOwnedItems();
    }

    private void ClearOwnedItems()
    {
        List<GameObject> items = new List<GameObject>(itemPool);
        for (var i = items.Count - 1; i > -1; i--)
        {
            if (items[i] == null)
                items.RemoveAt(i);
        }
        itemPool = items.ToArray();
        LevelManager.itemPool = items;
    }
}

