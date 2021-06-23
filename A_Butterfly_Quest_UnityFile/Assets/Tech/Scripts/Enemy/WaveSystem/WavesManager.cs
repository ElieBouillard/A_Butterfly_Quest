using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesManager : MonoBehaviour
{
    public static WavesManager instance;
    public float TimeBetweenWaves;

    public Waves[] Waves;
    public GameObject[] Spawners;

    public GameObject succesItem;

    //Debug
    private List<GameObject> CurrWaveEnemys;
    private int waveIndex;
    private float clockSpawn;
    [HideInInspector]
    public float clockBetweenWaves;
    private GameObject WaveText;
    private GameObject WaveBackground;

    private bool canWave;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CurrWaveEnemys = new List<GameObject>();
        WaveText = UIManager.instance.WaveText;
        WaveBackground = UIManager.instance.WaveBackGround;

        canShowBackGround = true;
        canHideBackGround = true;
        canShowText = true;
        canHideText = true;
        succesItem.SetActive(false);
    }

    private void Update()
    {
        WaveSystemUpdate();
    }

    int spawnerRandomValue;
    private void InitiateNextWave()
    {
        if(CurrWaveEnemys.Count > 0)
        {
            CurrWaveEnemys.Clear();
        }

        for (int i = 0; i < Waves[waveIndex].EnemyCount; i++)
        {
            spawnerRandomValue = Random.Range(0, Spawners.Length);
            Spawner currSpawnerScpt = Spawners[spawnerRandomValue].GetComponent<Spawner>();
            CurrWaveEnemys.Add(currSpawnerScpt.InstantiateEnemy());
        }
        clockBetweenWaves = TimeBetweenWaves;
        waveIndex++;
        canInitiateWave = false;
    }

    bool canInitiateWave;
    bool canShowBackGround;
    bool canShowText;
    bool canHideBackGround;
    bool canHideText;
    private void WaveSystemUpdate()
    {
        if (canWave)
        {
            if (CheckIfAllEnemyDead())
            {
                if (waveIndex >= Waves.Length)
                {
                    Win();
                }
                if (canInitiateWave)
                {
                    InitiateNextWave();
                }
                else
                {
                    if (clockBetweenWaves > 0)
                    {
                        if (canShowBackGround)
                        {
                            WaveBackground.GetComponent<Animator>().SetBool("On", true);
                            canShowBackGround = false;
                        }
                        if (canShowText && clockBetweenWaves <= 3.5f)
                        {
                            WaveText.GetComponent<Text>().text = "Wave " + (waveIndex + 1).ToString();
                            WaveText.GetComponent<Animator>().SetBool("On", true);
                            canShowText = false;
                        }
                        if (canHideText && clockBetweenWaves <= 1.5f)
                        {
                            WaveText.GetComponent<Animator>().SetBool("On", false);
                            canHideText = false;
                        }
                        if (canHideBackGround && clockBetweenWaves <= 1f)
                        {
                            WaveBackground.GetComponent<Animator>().SetBool("On", false);
                            canHideBackGround = false;
                        }
                        clockBetweenWaves -= Time.deltaTime;
                    }
                    else
                    {
                        canInitiateWave = true;

                        canShowBackGround = true;
                        canHideBackGround = true;
                        canShowText = true;
                        canHideText = true;
                    }
                }
            }
        }
    }

    public void StartWaveSystem()
    {
        clockBetweenWaves = TimeBetweenWaves;
        waveIndex = 0;
        if(CurrWaveEnemys.Count > 0)
        {
            foreach (GameObject enemy in CurrWaveEnemys)
            {
                Destroy(enemy);
            }
            CurrWaveEnemys.Clear();
        }
        canWave = true;
    }

    public void ClearEnemys()
    {
        if (CurrWaveEnemys.Count > 0)
        {
            foreach (GameObject enemy in CurrWaveEnemys)
            {
                Destroy(enemy);
            }
            CurrWaveEnemys.Clear();
        }
        canWave = false;
    }

    private bool CheckIfAllEnemyDead()
    {
        if(CurrWaveEnemys.Count == 0)
        {
            return true;
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

    private void Win()
    {
        canShowBackGround = false;
        canHideBackGround = false;
        canShowText = false;
        canHideText = false;
        succesItem.SetActive(true);
        canInitiateWave = false;
    }
}
