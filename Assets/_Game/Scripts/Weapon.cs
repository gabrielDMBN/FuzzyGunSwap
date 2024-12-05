using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon instance { get; private set; }
    
    [SerializeField] private Transform shotPosition;
    [SerializeField] private ParticleSystem shotEffect;
    
    void Awake()
    {
        instance = this;
    }
    
    public void ShootEffect()
    {
        ParticleSystem effect = Instantiate(shotEffect, shotPosition.position, Quaternion.identity);
        Destroy(effect.gameObject, 2f);
    }
    
}
