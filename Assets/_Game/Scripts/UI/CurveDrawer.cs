using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurveDrawer : MonoBehaviour
{
    public static CurveDrawer instance { get; private set; }

    public enum GraphType { Desirability, Distance, Ammo }
    public enum WeaponType { Pistol, Sniper, Shotgun }
    public GraphType graphType; // Option to choose the graph type in the inspector
    public WeaponType weaponType; // Option to choose the weapon type in the inspector

    public AnimationCurve curve; // The curve to be drawn
    public RectTransform graphArea; // The RectTransform of the Canvas where the graph will be drawn
    public Color graphColor = Color.green; // Color of the graph
    public int resolution = 256; // Resolution of the curve
    public float xMin = 0f; // Minimum value of the x-axis
    public float xMax = 50f; // Maximum value of the x-axis
    public int thickness = 2; // Thickness of the curve

    public float verticalLineX = 25f; // X position of the vertical line
    public Color verticalLineColor = Color.red; // Color of the vertical line

    public FuzzyPistol fuzzyPistol; // Reference to the FuzzyPistol instance
    public FuzzySniper fuzzySniper; // Reference to the FuzzySniper instance
    public FuzzyShotgun fuzzyShotgun; // Reference to the FuzzyShotgun instance

    private Texture2D graphTexture; // The texture that will be used to draw
    private Image graphImage; // The image where the texture will be applied

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Initialize the graph
        InitializeGraph();
        DrawGraph();
    }

    void Update()
    {
        DrawVerticalLine();
    }

    private void InitializeGraph()
    {
        // Create a texture and apply it to an Image component
        graphTexture = new Texture2D(resolution, resolution);
        graphTexture.filterMode = FilterMode.Bilinear;

        graphImage = graphArea.GetComponent<Image>();
        if (graphImage == null)
        {
            graphImage = graphArea.gameObject.AddComponent<Image>();
        }
        graphImage.sprite = Sprite.Create(graphTexture, new Rect(0, 0, resolution, resolution), Vector2.zero);
        graphImage.color = Color.white; // Ensure the image color is white to display the texture correctly
    }

    public void DrawGraph()
    {
        DrawGraphCurve();
        DrawVerticalLine();
        graphTexture.Apply();
    }

    public void DrawGraphCurve()
    {
        // Clear the texture
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                graphTexture.SetPixel(x, y, Color.clear);
            }
        }

        // Determine the range of the curve
        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        for (int i = 0; i < resolution; i++)
        {
            float t = Mathf.Lerp(xMin, xMax, (float)i / (resolution - 1)); // Correct x scaling
            float value = curve.Evaluate(t);

            if (value < minValue) minValue = value;
            if (value > maxValue) maxValue = value;
        }

        float range = maxValue - minValue;

        // Draw the curve with thickness
        for (int x = 0; x < resolution; x++)
        {
            float t = Mathf.Lerp(xMin, xMax, (float)x / (resolution - 1)); // Correct x scaling
            float value = curve.Evaluate(t);

            float normalizedValue = (value - minValue) / range; // Normalize value
            int y = Mathf.Clamp((int)(normalizedValue * (resolution - 1)), 0, resolution - 1);

            // Paint pixels around the main point to add thickness
            for (int dx = -thickness; dx <= thickness; dx++)
            {
                for (int dy = -thickness; dy <= thickness; dy++)
                {
                    int px = Mathf.Clamp(x + dx, 0, resolution - 1);
                    int py = Mathf.Clamp(y + dy, 0, resolution - 1);

                    graphTexture.SetPixel(px, py, graphColor);
                }
            }
        }
    }

    public void DrawVerticalLine()
    {
        // Clear the previous vertical line
        for (int y = 0; y < resolution; y++)
        {
            for (int dx = -thickness; dx <= thickness; dx++)
            {
                int px = Mathf.Clamp((int)((verticalLineX - xMin) / (xMax - xMin) * (resolution - 1)) + dx, 0, resolution - 1);
                graphTexture.SetPixel(px, y, Color.clear);
            }
        }

        // Update the vertical line position based on the selected graph type and weapon type
        switch (weaponType)
        {
            case WeaponType.Pistol:
                switch (graphType)
                {
                    case GraphType.Desirability:
                        verticalLineX = fuzzyPistol.FuzzyPistolSystem();
                        break;
                    case GraphType.Distance:
                        verticalLineX = fuzzyPistol.distanceToPlayer;
                        break;
                    case GraphType.Ammo:
                        verticalLineX = fuzzyPistol.ammoCount;
                        break;
                }
                break;
            case WeaponType.Sniper:
                switch (graphType)
                {
                    case GraphType.Desirability:
                        verticalLineX = fuzzySniper.FuzzySniperSystem();
                        break;
                    case GraphType.Distance:
                        verticalLineX = fuzzySniper.distanceToPlayer;
                        break;
                    case GraphType.Ammo:
                        verticalLineX = fuzzySniper.ammoCount;
                        break;
                }
                break;
            case WeaponType.Shotgun:
                switch (graphType)
                {
                    case GraphType.Desirability:
                        verticalLineX = fuzzyShotgun.FuzzyShotgunSystem();
                        break;
                    case GraphType.Distance:
                        verticalLineX = fuzzyShotgun.distanceToPlayer;
                        break;
                    case GraphType.Ammo:
                        verticalLineX = fuzzyShotgun.ammoCount;
                        break;
                }
                break;
        }

        // Draw the new vertical line
        int verticalLineXPos = Mathf.Clamp((int)((verticalLineX - xMin) / (xMax - xMin) * (resolution - 1)), 0, resolution - 1);
        for (int y = 0; y < resolution; y++)
        {
            for (int dx = -thickness; dx <= thickness; dx++)
            {
                int px = Mathf.Clamp(verticalLineXPos + dx, 0, resolution - 1);
                graphTexture.SetPixel(px, y, verticalLineColor);
            }
        }

        graphTexture.Apply();
    }
}