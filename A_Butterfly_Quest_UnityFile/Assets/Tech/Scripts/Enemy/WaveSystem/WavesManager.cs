using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    public static WavesManager instance;
    public float TimeBetweenWaves;

    public Waves[] Waves;
    public GameObject[] Spawners;

    [Header("Debug")]
    public List<GameObject> CurrWaveEnemys;
    public int waveIndex;
    private float clockWave;
    private float clockSpawn;
    public float clockBetweenWaves;

    public GameObject WinBlackScreen;

    bool canWin = true;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CurrWaveEnemys = new List<GameObject>();
        waveIndex = 0;
        InitiateNextWave();
        clockBetweenWaves = TimeBetweenWaves;
        canWin = true;
    }

    private void Update()
    {
        WaveSystemUpdate();
        WinGame();
    }

    private void InitiateNextWave()
    {
        foreach (GameObject spawner in Spawners)
        {
            Spawner currScpt = spawner.GetComponent<Spawner>();
            for (int i = 0; i < Waves[waveIndex].StartSpawnCount; i++)
            {
                CurrWaveEnemys.Add(currScpt.InstantiateEnemy());
            }
        }
        clockWave = Waves[waveIndex].WaveTime;
        clockSpawn = Waves[waveIndex].RateTimingSpawn;
    }

    //private void InstantiateRateGroup()
    //{
    //    foreach (GameObject spawner in Spawners)
    //    {
    //        Spawner currScpt = spawner.GetComponent<Spawner>();
    //        for (int i = 0; i < Waves[waveIndex].HowManyPerRate; i++)
    //        {
    //            CurrWaveEnemys.Add(currScpt.InstantiateEnemy());
    //        }
    //    }
    //}

    public void KillAllEnemys()
    {
        for (int i = 0; i < CurrWaveEnemys.Count; i++)
        {
            CurrWaveEnemys[i].gameObject.SetActive(false);
        }
    }

    private void WaveSystemUpdate()
    {
        //if(clockWave > 0)
        //{
        //    clockWave -= Time.deltaTime;

        //    if (clockSpawn > 0)
        //    {
        //        clockSpawn -= Time.deltaTime;
        //    }
        //    else
        //    {
        //        InstantiateRateGroup();
        //        clockSpawn = Waves[waveIndex].RateTimingSpawn;
        //    }
        //}
        //else
        //{
        //    if (CheckIfAllEnemyDead())
        //    {
        //        if(clockBetweenWaves > 0)
        //        {
        //            clockBetweenWaves -= Time.deltaTime;
        //        }
        //        else
        //        {
        //            CurrWaveEnemys.Clear();
        //            waveIndex++;
        //            InitiateNextWave();
        //            clockBetweenWaves = TimeBetweenWaves;
        //        }
        //    }
        //}
    }

    private bool CheckIfAllEnemyDead()
    {
        if(CurrWaveEnemys.Count == 0)
        {
            return false;
        }

        foreach (GameObject enemy in CurrWaveEnemys)
        {
            if(enemy.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    private void WinGame()
    {
        if (CheckIfAllEnemyDead())
        {
            if (canWin)
            {
                WinBlackScreen.GetComponent<Animator>().SetBool("Opace", true);
                canWin = false;
            }
        }
    }
}
