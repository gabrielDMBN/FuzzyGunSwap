using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject gunsInfo;

    private FuzzyPistol fuzzyPistol;
    private FuzzyShotgun fuzzyShotgun;
    private FuzzySniper fuzzySniper;

    void Start()
    {
        //_currentWeapon = pistol;
        GameObject sniperGameObject = gunsInfo;
        GameObject shotgunGameObject = gunsInfo;
        GameObject pistolGameObject = gunsInfo;

        if (sniperGameObject != null) fuzzySniper = sniperGameObject.GetComponent<FuzzySniper>();
        if (shotgunGameObject != null) fuzzyShotgun = shotgunGameObject.GetComponent<FuzzyShotgun>();
        if (pistolGameObject != null) fuzzyPistol = pistolGameObject.GetComponent<FuzzyPistol>();

    }
    
}
