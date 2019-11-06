using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MonoBehaviour
{
    public Vector3 destination;
    public float speed = 1f;

    IEnumerator Start()
    {
        var pointA = transform.position;
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, destination, speed));
            yield return StartCoroutine(MoveObject(transform, destination, pointA, speed));
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.localPosition = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
}
