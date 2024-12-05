using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotãoFecharAbrir : MonoBehaviour
{
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private Vector3 offScreenPosition = new Vector3(-10000, -10000, 0);

    private RectTransform canvasRectTransform;
    private Vector3 originalPosition;
    private bool isOffScreen = false;

    private void Start()
    {
        if (canvasObject != null)
        {
            canvasRectTransform = canvasObject.GetComponent<RectTransform>();
            if (canvasRectTransform != null)
            {
                originalPosition = canvasRectTransform.anchoredPosition;
            }
        }
    }

    public void ToggleCanvasPosition()
    {
        if (canvasRectTransform != null)
        {
            if (isOffScreen)
            {
                canvasRectTransform.anchoredPosition = originalPosition;
            }
            else
            {
                canvasRectTransform.anchoredPosition = offScreenPosition;
            }
            isOffScreen = !isOffScreen;
        }
    }
}
