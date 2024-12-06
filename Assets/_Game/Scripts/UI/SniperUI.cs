using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SniperUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI desirabilityText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private FuzzySniper fuzzySniper;

    // Update is called once per frame
    void Update()
    {
        if (fuzzySniper != null)
        {
            float distanceToPlayer = fuzzySniper.distanceToPlayer;
            float desirability = fuzzySniper.FuzzySniperSystem();
            float ammoCount = fuzzySniper.ammoCount;

            distanceText.text = "Distance: " + distanceToPlayer.ToString("F2");
            desirabilityText.text = "Desirability: " + desirability.ToString("F2");
            ammoText.text = "Ammo: " + ammoCount.ToString("F2");
        }
    }
}