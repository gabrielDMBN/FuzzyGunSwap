using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class FuzzyLogic : MonoBehaviour
{
    //variables
    [SerializeField] protected float distanceToPlayer;
    [SerializeField] protected int ammoCount;

    //desirability curves
    [SerializeField] private AnimationCurve undesirable;
    [SerializeField] private AnimationCurve desirable;
    [SerializeField] private AnimationCurve veryDesirable;
    
    //clipped desirability curves
    // [SerializeField] private AnimationCurve clippedUndesirable;
    // [SerializeField] private AnimationCurve clippedDesirable;
    // [SerializeField] private AnimationCurve clippedVeryDesirable;
    
    //distance curves
    [SerializeField] private AnimationCurve closeCurve;
    [SerializeField] private AnimationCurve mediumCurve;
    [SerializeField] private AnimationCurve farCurve;
    
    //Fuzzification
    public float[] FuzzifyDistance()
    {
        float[] fuzzifiedValues = new float[3];
        fuzzifiedValues[0] = closeCurve.Evaluate(distanceToPlayer);
        fuzzifiedValues[1] = mediumCurve.Evaluate(distanceToPlayer);
        fuzzifiedValues[2] = farCurve.Evaluate(distanceToPlayer);
        return fuzzifiedValues;
    }
    
    public abstract float[] FuzzifyAmmo();
    public abstract float[] FuzziRulesOutput(float[] distanceValues, float[] ammoValues);
    
    //Defuzzification (Centroid)
    public float CalculateCentroid(float[] truncationValues, int steps = 10)
    {
        float numerator = 0f;
        float denominator = 0f;

        // Calcula o intervalo de amostragem
        float minDomain = 0f;
        float maxDomain = 100f; 
        float stepSize = (maxDomain - minDomain) / steps;

        // Soma as contribuições de todas as curvas truncadas
        for (int i = 1; i <= steps; i++)
        {
            float x = minDomain + i * stepSize;

            // Obter os valores truncados
            float uUndesirable = Mathf.Min(undesirable.Evaluate(x), truncationValues[0]);
            float uDesirable = Mathf.Min(desirable.Evaluate(x), truncationValues[1]);
            float uVeryDesirable = Mathf.Min(veryDesirable.Evaluate(x), truncationValues[2]);

            // Soma ponderada (numerador)
            numerator += x * (uUndesirable + uDesirable + uVeryDesirable);

            // Soma das pertinências (denominador)
            denominator += (uUndesirable + uDesirable + uVeryDesirable);
            
        }

        // Evitar divisão por zero
        if (denominator == 0f)
        {
            Debug.LogWarning("Nenhuma área relevante sob as curvas truncadas.");
            return 0f;
        }

        // Retorna o centroide
        return numerator / denominator;
    }
    
    
    
    
    
}
