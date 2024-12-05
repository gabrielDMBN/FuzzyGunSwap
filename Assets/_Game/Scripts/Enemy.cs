using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy instance { get; private set; }
    
    [SerializeField] private Transform hitPosition;
    [SerializeField] private ParticleSystem enemyHitEffect;
    
    private void Awake()
    {
        instance = this;
    }
    
    public void Hit()
    {
        ParticleSystem effect = Instantiate(enemyHitEffect, hitPosition.position, Quaternion.identity, hitPosition);
        Destroy(effect.gameObject, 3f);
    }
}
