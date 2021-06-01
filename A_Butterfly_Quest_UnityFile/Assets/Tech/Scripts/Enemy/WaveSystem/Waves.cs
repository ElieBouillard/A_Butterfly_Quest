using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waves
{
    public int StartSpawnCount;
    public int HowManyPerRate;
    public float RateTimingSpawn;
    public float WaveTime;

    public float GetRateSpawn()
    {
        return RateTimingSpawn;
    }

    public float GetWaveTime()
    {
        return WaveTime;
    }

    public int GetHowManyPerRate()
    {
        return HowManyPerRate;
    }
}
