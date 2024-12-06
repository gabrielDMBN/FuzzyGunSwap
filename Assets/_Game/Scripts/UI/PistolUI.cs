using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PistolUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI desirabilityText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private FuzzyPistol fuzzyPistol;

    // Update is called once per frame
    void Update()
    {
        if (fuzzyPistol != null)
        {
            float distanceToPlayer = fuzzyPistol.distanceToPlayer;
            float desirability = fuzzyPistol.FuzzyPistolSystem();
            float ammoCount = fuzzyPistol.ammoCount;

            distanceText.text = "Distance: " + distanceToPlayer.ToString("F2");
            desirabilityText.text = "Desirability: " + desirability.ToString("F2");
            ammoText.text = "Ammo: " + ammoCount.ToString("F2");
        }
    }
}