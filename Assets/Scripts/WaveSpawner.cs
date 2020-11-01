using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBetweeenSpawns;
        public Powerup[] powerups;
        public int powerupCount;
    }

    public Wave[] Waves;
    public Transform[] SpawnPoints;
    public float TimeBetweenWaves;

    private Wave CurrentWave;
    private int CurrentWaveIndex;
    private Transform PlayerTransform;

    private bool finishedSpawning;

    [SerializeField]
    private GameObject _NextWaveObj;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _NextWaveObj.SetActive(false);
        StartCoroutine(StartNextWave(CurrentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        _NextWaveObj.SetActive(true);
        yield return new WaitForSeconds(TimeBetweenWaves);
        _NextWaveObj.SetActive(false);
        StartCoroutine(StartWave(index));
    }

    public void RestartWaveSpawner()
    {
        GameObject[] SpawnedPowerups = GameObject.FindGameObjectsWithTag("Powerup");
        foreach (GameObject spawnedPowerup in SpawnedPowerups)
            Destroy(spawnedPowerup);
        GameObject[] SpawnedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject spawnedEnemy in SpawnedEnemies)
            Destroy(spawnedEnemy);
        StartCoroutine(StartNextWave(0));
    }

    IEnumerator StartWave(int index)
    {
        CurrentWave = Waves[index];

        for (int k = 0; k < CurrentWave.powerupCount; k++)
        {
            Vector2 pos = new Vector2(Random.Range(-20f, 20f), Random.Range(-20f, 20f));
            Powerup randomPowerup = CurrentWave.powerups[Random.Range(0, CurrentWave.powerups.Length)];
            Instantiate(randomPowerup, pos, Quaternion.identity);
        }

        for (int i = 0; i < CurrentWave.count; i++)
        {
            if (PlayerTransform == null)
            {
                yield break;
            }
            Enemy randomEnemy = CurrentWave.enemies[Random.Range(0, CurrentWave.enemies.Length)];
            Transform randomSpot = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

            if (i == CurrentWave.count - 1)
            {
                finishedSpawning = true;
            }

            yield return new WaitForSeconds(CurrentWave.timeBetweeenSpawns);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (finishedSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawning = false;
            if (CurrentWaveIndex + 1 < Waves.Length)
            {
                CurrentWaveIndex += 1;
                StartCoroutine(StartNextWave(CurrentWaveIndex));
            }
            else
            {
                Debug.Log("Game finished");
            }
        }
    }
}
