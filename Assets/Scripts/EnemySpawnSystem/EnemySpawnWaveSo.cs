using System;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySpawnSystem
{
    public class EnemySpawnWaveSo : ScriptableObject
    {
        public List<SpawnPointDto> spawnPoints;
        public float spawnIntervalInSeconds;
        public List<WaveDto> waveData;
    }

    [Serializable]
    public class WaveDto
    {
        public EnemyType enemyType;
        public int spawnPointId;
    }

    [Serializable]
    public enum EnemyType
    {
        Type1 = 1,
        Type2 = 2,
        Type3 = 3,
    }
}

