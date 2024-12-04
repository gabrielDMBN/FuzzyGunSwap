using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WeaponSelector : MonoBehaviour
{
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject sniper;
    [SerializeField] private GameObject _currentWeapon;
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

    public void SelectWeapon(GameObject weapon)
    {
        _currentWeapon.SetActive(false);
        weapon.SetActive(true);
        _currentWeapon = weapon;
    }

    public void SelectWeaponByFuzzyLogic()
    {
        float sniperDesirability = fuzzySniper.FuzzySniperSystem();
        float shotgunDesirability = fuzzyShotgun.FuzzyShotgunSystem();
        float pistolDesirability = fuzzyPistol.FuzzyPistolSystem();

        if (sniperDesirability > shotgunDesirability && sniperDesirability > pistolDesirability && _currentWeapon != sniper)
        {
            SelectWeapon(sniper);
        }
        if (shotgunDesirability > sniperDesirability && shotgunDesirability > pistolDesirability && _currentWeapon != shotgun)
        {
            SelectWeapon(shotgun);
        }
        if (pistolDesirability > sniperDesirability && pistolDesirability > shotgunDesirability && _currentWeapon != pistol)
        {
            SelectWeapon(pistol);
        }
        
    }

    private void Update()
    {
        SelectWeaponByFuzzyLogic();
    }
}