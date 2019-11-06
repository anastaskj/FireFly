using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseMovement : MonoBehaviour
{
    private FireFlyResources fire;

    private Vector3 mousePosition;

    private void Start()
    {
        fire = GetComponent<FireFlyResources>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, fire.speed);
    }
}
