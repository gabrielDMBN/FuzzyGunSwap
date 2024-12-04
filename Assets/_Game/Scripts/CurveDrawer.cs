using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurveDrawer : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private RawImage rawImage;
    [SerializeField] private int textureWidth = 256;
    [SerializeField] private int textureHeight = 256;

    private void Start()
    {
        DrawCurve();
    }

    private void DrawCurve()
    {
        Texture2D texture = new Texture2D(textureWidth, textureHeight);
        for (int x = 0; x < textureWidth; x++)
        {
            float t = (float)x / (textureWidth - 1);
            float y = curve.Evaluate(t);
            int pixelY = Mathf.RoundToInt(y * (textureHeight - 1));
            for (int yPixel = 0; yPixel < textureHeight; yPixel++)
            {
                Color color = yPixel == pixelY ? Color.white : Color.black;
                texture.SetPixel(x, yPixel, color);
            }
        }
        texture.Apply();
        rawImage.texture = texture;
    }
}