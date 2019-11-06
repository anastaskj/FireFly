using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLifetime : MonoBehaviour
{
    public float lifetime = 2.5f;
    

    private void Update()
    {
        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            lifetime -= Time.deltaTime;
        }
    }
}
