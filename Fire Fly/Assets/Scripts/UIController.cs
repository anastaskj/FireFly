using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOver = false;
    public float alpha = 0.15f;
    private CanvasGroup group;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();
    }
    public void MakeTransparent()
    {
        if (!isOver)
        {
            group.alpha = 1f;
        }
        else
        {
            group.alpha = alpha;
        } 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
        MakeTransparent();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
        MakeTransparent();
    }
    
}
