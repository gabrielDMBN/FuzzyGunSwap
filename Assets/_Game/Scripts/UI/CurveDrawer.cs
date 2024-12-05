using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurveDrawer : MonoBehaviour
{
    public AnimationCurve curve; // A curva que será desenhada
    public RectTransform graphArea; // O RectTransform do Canvas onde o gráfico será desenhado
    public Color graphColor = Color.green; // Cor do gráfico
    public int resolution = 256; // Resolução da curva
    public float xMin = 0f; // Valor mínimo do eixo X
    public float xMax = 50f; // Valor máximo do eixo X
    public int thickness = 2; // Espessura da curva// Espessura da curva

        
    private Texture2D graphTexture; // A textura que será usada para desenhar
    private Image graphImage; // A imagem onde a textura será aplicada

    void Start()
    {
        // Inicializar o gráfico
        InitializeGraph();
        DrawGraph();
    }

    private void InitializeGraph()
    {
        // Criar uma textura e aplicá-la a um componente Image
        graphTexture = new Texture2D(resolution, resolution);
        graphTexture.filterMode = FilterMode.Bilinear;

        graphImage = graphArea.GetComponent<Image>();
        if (graphImage == null)
        {
            graphImage = graphArea.gameObject.AddComponent<Image>();
        }
        graphImage.sprite = Sprite.Create(graphTexture, new Rect(0, 0, resolution, resolution), Vector2.zero);
        graphImage.color = Color.white; // Certifique-se de que a cor da imagem seja branca para exibir a textura corretamente
    }

    private void DrawGraph()
    {
        // Limpar a textura
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                graphTexture.SetPixel(x, y, Color.clear);
            }
        }

        // Descobrir o intervalo da curva
        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        for (int i = 0; i < resolution; i++)
        {
            float t = Mathf.Lerp(xMin, xMax, (float)i / (resolution - 1)); // Escala correta de X
            float value = curve.Evaluate(t);

            if (value < minValue) minValue = value;
            if (value > maxValue) maxValue = value;
        }

        float range = maxValue - minValue;

        // Desenhar a curva com espessura
        for (int x = 0; x < resolution; x++)
        {
            float t = Mathf.Lerp(xMin, xMax, (float)x / (resolution - 1)); // Escala correta de X
            float value = curve.Evaluate(t);

            float normalizedValue = (value - minValue) / range; // Normalizar valor
            int y = Mathf.Clamp((int)(normalizedValue * (resolution - 1)), 0, resolution - 1);

            // Pintar pixels ao redor do ponto principal para adicionar espessura
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

        // Aplicar mudanças na textura
        graphTexture.Apply();
    }
}