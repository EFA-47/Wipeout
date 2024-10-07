using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyCollection;
    public GameObject[] powerupCollection;
    private GameObject enemy;
    private GameObject powerup;
    private Vector3 spawnPoint;
    public float spawnRange = 9.0f;
    private float spawnRangeX;
    private float spawnRangeZ;
    public float spawnTimer = 5;
    private int enemyCount = 0;
    private float time;
    private int waveNumber = 1;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        time = Time.time - spawnTimer;
        spawnEnemyWave(waveNumber);
        spawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Time.time - time > spawnTimer){
        //     time = Time.time;
        //     SpawnEnemy();
        // }
        if(player.transform.position.y>-10){
            enemyCount = FindObjectsOfType<EnemyController>().Length;
            if(enemyCount < 1){
                waveNumber++;
                spawnEnemyWave(waveNumber);
                spawnPowerup();
            }
        }else{
            foreach(EnemyController enemy in FindObjectsOfType<EnemyController>()){
            Destroy(enemy);
            }
        
        }
        
    }

    private void spawnEnemyWave(int enemy){
        for(int i =0; i< enemy; i++){
            SpawnEnemy();
        }
    }

    private void SpawnEnemy(){

        enemy = enemyCollection[Random.Range(0,enemyCollection.Length)];
        spawnPoint = GenerateSpawnPoint();
        Instantiate(enemy, spawnPoint, enemy.transform.rotation);
    }

    private void spawnPowerup(){

        powerup = powerupCollection[Random.Range(0,powerupCollection.Length)];
        spawnPoint = GenerateSpawnPoint();
        Instantiate(powerup, spawnPoint, powerup.transform.rotation);
    }

    private Vector3 GenerateSpawnPoint(){

        spawnRangeX = Random.Range(-spawnRange, spawnRange);
        spawnRangeZ = Random.Range(-spawnRange, spawnRange);

        return new(spawnRangeX, 0, spawnRangeZ);
    }
}
