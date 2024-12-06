using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShotgunUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI desirabilityText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private FuzzyShotgun fuzzyShotgun;

    // Update is called once per frame
    void Update()
    {
        if (fuzzyShotgun != null)
        {
            float distanceToPlayer = fuzzyShotgun.distanceToPlayer;
            float desirability = fuzzyShotgun.FuzzyShotgunSystem();
            float ammoCount = fuzzyShotgun.ammoCount;

            distanceText.text = "Distance: " + distanceToPlayer.ToString("F2");
            desirabilityText.text = "Desirability: " + desirability.ToString("F2");
            ammoText.text = "Ammo: " + ammoCount.ToString("F2");
        }
    }
}
