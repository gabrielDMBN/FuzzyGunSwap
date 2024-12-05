using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject botPrefab;
    
    //Efeitos
    [SerializeField] private ParticleSystem enemySummonEffect;
    
    
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private BoxCollider spawnArea;
    [SerializeField] private GameObject distanceInfo;
    
    private Animator botAnimator;
    private WeaponSelector gunManager;
    private GameObject spawnedEnemy;
    private Queue<GameObject> enemyQueue = new Queue<GameObject>();
    private Animator enemyAnimator;
    private bool _canSpawn = true;
    
    private void Start()
    {
        GameObject distanceGameObject = distanceInfo;
        botAnimator = botPrefab.GetComponent<Animator>();
        if (distanceGameObject != null) gunManager = distanceGameObject.GetComponent<WeaponSelector>();
        
    }
    
    private void Spawn()
    {
        if (!_canSpawn) return;
        Vector3 spawnPosition = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), 0, Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
        
        //efeito
        ParticleSystem effect = Instantiate(enemySummonEffect, spawnPosition, Quaternion.identity);
        Destroy(effect.gameObject, 4f);
        
        gunManager.NewDistance(spawnPosition.x);
        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0, -90, 0));
        enemyAnimator = spawnedEnemy.GetComponent<Animator>();
        enemyQueue.Enqueue(spawnedEnemy);
        _canSpawn = false;

        StartCoroutine(Eliminate());
        StartCoroutine(Despawn());
        StartCoroutine(ResetSpawn());
    }
    
    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(9);
        
        if (enemyQueue.Count > 0)
        {
            GameObject enemyToDespawn = enemyQueue.Dequeue();
            Destroy(enemyToDespawn);
        }
    
    }
    
    private IEnumerator Eliminate()
    {
        yield return new WaitForSeconds(3);
        
        gunManager.Shoot();
        
        Enemy.instance.Hit();
        
        if (enemyQueue.Count > 0)
        {
            if (enemyAnimator != null)
            {
                enemyAnimator.SetTrigger("Die");
            }
        }

    }
    
    private IEnumerator ResetSpawn()
    {
        yield return new WaitForSeconds(5);
        
        _canSpawn = true;
    }

    private void Update()
    {
        if (gunManager.IsAmmoEmpty() == false)
        {
            Spawn();
        }
        else botAnimator.SetTrigger("Victory");
    }
}
