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
    
    [SerializeField] private GameObject switchWeaponEffect;

    private FuzzyPistol fuzzyPistol;
    private FuzzyShotgun fuzzyShotgun;
    private FuzzySniper fuzzySniper;
    

    void Start()
    {
        //_currentWeapon = pistol;
        GameObject sniperGameObject = gunsInfo;
        GameObject shotgunGameObject = gunsInfo;
        GameObject pistolGameObject = gunsInfo;

        if (sniperGameObject != null) fuzzySniper = sniperGameObject.GetComponent<FuzzySniper>(); fuzzySniper.RandomizeAmmo(3,9);
        if (shotgunGameObject != null) fuzzyShotgun = shotgunGameObject.GetComponent<FuzzyShotgun>(); fuzzyShotgun.RandomizeAmmo(4,15);
        if (pistolGameObject != null) fuzzyPistol = pistolGameObject.GetComponent<FuzzyPistol>(); fuzzyPistol.RandomizeAmmo(5,30);

    }

    public void SelectWeapon(GameObject weapon)
    {
        _currentWeapon.SetActive(false);
        weapon.SetActive(true);
        _currentWeapon = weapon;
        
        SwitchEffect();
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
        _currentWeapon.GetComponent<Weapon>().ShootEffect();

        //Weapon.instance.ShootEffect();
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
    
    public void SwitchEffect()
    {
        switchWeaponEffect.SetActive(false);
        switchWeaponEffect.SetActive(true);
    }

    private void Update()
    {
        SelectWeaponByFuzzyLogic();
    }
}