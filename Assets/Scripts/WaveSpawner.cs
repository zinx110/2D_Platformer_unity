using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;

    }


    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    public Wave[] waves;
    public Transform[] spawnPoints; 
    private int nextWave = 0;
    public float timeBetweenWaves = 4f;
    public float waveCountdown;
    private float searchCountDown = 1f;
    private SpawnState state = SpawnState.COUNTING;

    public SpawnState State
    {
        get
        {
            return state;
        }
    }

    public float WaveCountdown
    {
        get
        {
            return waveCountdown;
        }
    }

    public int NextWave
    {
        get { return nextWave+1; }
    }



    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawnpoints referenced");
        }

    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            //  check if enemies are still alive.
            if (!isEnemyAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }


        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }


    void WaveCompleted()
    {

        Debug.Log("Wave Completed");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves completed. Looping.");
        }
        else
        {

        nextWave++;
        }

    }



    bool isEnemyAlive()
    {

        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }

        }
        return true;
    }




    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave" + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }




        state = SpawnState.WAITING;



        yield break;
    }


    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning enemy " + _enemy.name);
        
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, sp.position, sp.rotation);
    }
}
