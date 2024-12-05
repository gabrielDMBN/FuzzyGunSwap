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
    [SerializeField] private ParticleSystem switchWeaponEffect;
    [SerializeField] private Transform switchWeaponEffectPosition;

    private FuzzyPistol fuzzyPistol;
    private FuzzyShotgun fuzzyShotgun;
    private FuzzySniper fuzzySniper;


    private void Awake()
    {
        //_currentWeapon = null;
    }

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
        
        ParticleSystem effect = Instantiate(switchWeaponEffect, switchWeaponEffectPosition.position, switchWeaponEffectPosition.rotation);
        Destroy(effect.gameObject, 2f);
    }
    
    public void Shoot()
    {
        if (_currentWeapon == sniper)
        {
            fuzzySniper.ReduceAmmo();
        }
        if (_currentWeapon == shotgun)
        {
            fuzzyShotgun.ReduceAmmo();
        }
        if (_currentWeapon == pistol)
        {
            fuzzyPistol.ReduceAmmo();
        }
    }
    
    public void NewDistance(float distance)
    {
        fuzzySniper.SetDistance(distance);
        fuzzyShotgun.SetDistance(distance);
        fuzzyPistol.SetDistance(distance);
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
    
    public bool IsAmmoEmpty()
    {
        if( fuzzyPistol.GetAmmo() <= 0 && fuzzyShotgun.GetAmmo() <= 0 && fuzzySniper.GetAmmo() <= 0)
        {
            return true;
        }
        return false;
        
    }

    private void Update()
    {
        SelectWeaponByFuzzyLogic();
    }
}