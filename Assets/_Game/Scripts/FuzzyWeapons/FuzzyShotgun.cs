using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyShotgun : FuzzyLogic
{
    //ammo curves
    [SerializeField] private AnimationCurve lowAmmoCurve;
    [SerializeField] private AnimationCurve mediumAmmoCurve;
    [SerializeField] private AnimationCurve highAmmoCurve;
    
    //Fuzzification
    public override float[] FuzzifyAmmo()
    {
        float[] fuzzifiedValues = new float[3];
        fuzzifiedValues[0] = lowAmmoCurve.Evaluate(ammoCount);
        fuzzifiedValues[1] = mediumAmmoCurve.Evaluate(ammoCount);
        fuzzifiedValues[2] = highAmmoCurve.Evaluate(ammoCount);
        return fuzzifiedValues;
    }
    
    //Fuzzy Rules
    public override float[] FuzziRulesOutput(float[] distanceValues, float[] ammoValues)
    {
        float[] desirabilityValues = new float[3];

        // Rule 1 IF Target_Far AND Ammo_Loads THEN Undesirable
        desirabilityValues[0] = Mathf.Max(desirabilityValues[0], Mathf.Min(distanceValues[2], ammoValues[2]));
        // Rule 2 IF Target_Far AND Ammo_Okay THEN Undesirable
        desirabilityValues[0] = Mathf.Max(desirabilityValues[0], Mathf.Min(distanceValues[2], ammoValues[1]));
        // Rule 3 IF Target_Far AND Ammo_Low THEN Undesirable
        desirabilityValues[0] = Mathf.Max(desirabilityValues[0], Mathf.Min(distanceValues[2], ammoValues[0]));
        // Rule 4 IF Target_Medium AND Ammo_Loads THEN Desirable
        desirabilityValues[1] = Mathf.Max(desirabilityValues[1], Mathf.Min(distanceValues[1], ammoValues[2]));
        // Rule 5 IF Target_Medium AND Ammo_Okay THEN Undesirable
        desirabilityValues[0] = Mathf.Max(desirabilityValues[0], Mathf.Min(distanceValues[1], ammoValues[1]));
        // Rule 6 IF Target_Medium AND Ammo_Low THEN Undesirable
        desirabilityValues[0] = Mathf.Max(desirabilityValues[0], Mathf.Min(distanceValues[1], ammoValues[0]));
        // Rule 7 IF Target_Close AND Ammo_Loads THEN VeryDesirable
        desirabilityValues[2] = Mathf.Max(desirabilityValues[2], Mathf.Min(distanceValues[0], ammoValues[2]));
        // Rule 8 IF Target_Close AND Ammo_Okay THEN VeryDesirable
        desirabilityValues[2] = Mathf.Max(desirabilityValues[2], Mathf.Min(distanceValues[0], ammoValues[1]));
        // Rule 9 IF Target_Close AND Ammo_Low THEN VeryDesirable
        desirabilityValues[2] = Mathf.Max(desirabilityValues[2], Mathf.Min(distanceValues[0], ammoValues[0]));
        
        //Empty Rule: IF Ammo is Empty THEN 100% Undesirable
        if (ammoCount <= 0)
        {
            desirabilityValues[0] = 1;
            desirabilityValues[1] = 0;
            desirabilityValues[2] = 0;
        }
        
        return desirabilityValues;
    }

    private void Update()
    {
        // float[] distanceValues = FuzzifyDistance(); 
        // float[] ammoValues = FuzzifyAmmo();
        // float[] desirabilityValues = FuzziRulesOutput(distanceValues, ammoValues);
        // Debug.Log(desirabilityValues[0] + " " + desirabilityValues[1] + " " + desirabilityValues[2]);
        // float desirability = CalculateCentroid(desirabilityValues);
        // Debug.Log(desirability);
    }
}
