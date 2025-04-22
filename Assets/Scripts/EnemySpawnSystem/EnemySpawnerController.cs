using System.Collections;
using System.Collections.Generic;
using Enemies;
using UiTool;
using UnityEngine;

namespace EnemySpawnSystem
{
    public class EnemySpawnerController : MonoBehaviour
    {
        [HideInInspector] public List<EnemySpawnWaveSo> waves;
        [SerializeField] private OverlayToast toast;
        [Space]
        [SerializeField] private EnemyController enemy1;
        [SerializeField] private EnemyController enemy2;
        [SerializeField] private EnemyController enemy3;

        private Dictionary<EnemyType, EnemyController> enemyPrefabs;

        private void Awake()
        {
            enemyPrefabs = new Dictionary<EnemyType, EnemyController>
            {
                { EnemyType.Type1, enemy1 },
                { EnemyType.Type2, enemy2 },
                { EnemyType.Type3, enemy3 }
            };
        }

        private void OnEnable()
        {
            StartCoroutine(StartWaves());
        }

        //It's better to use UniTask package for a method like this but i wanted to provide something without using any packages  
        private IEnumerator StartWaves()
        {
            for (int waveIndex = 0; waveIndex < waves.Count; waveIndex++)
            {
                var wave = waves[waveIndex];

                var uiToast = Instantiate(toast, transform);
                uiToast.ShowToast("Wave " + (waveIndex + 1));

                yield return new WaitForSeconds(wave.spawnIntervalInSeconds);

                foreach (var waveData in wave.waveData)
                {
                    yield return new WaitForSeconds(wave.spawnIntervalInSeconds);

                    var data = waveData;
                    var spawnPoint = wave.spawnPoints.Find(x => x.id == data.spawnPointId);
                    if (spawnPoint == null)
                    {
                        Debug.LogError("No spawnPoint found with this ID: " + waveData.spawnPointId);
                        continue;
                    }

                    SpawnEnemy(waveData.enemyType, spawnPoint.pathWaypoints);
                }

                yield return new WaitForSeconds(wave.spawnIntervalInSeconds);
            }

            yield return null;
        }

        private void SpawnEnemy(EnemyType type, List<Vector3> path)
        {
            if (enemyPrefabs.TryGetValue(type, out var prefab))
            {
                var enemy = Instantiate(prefab, transform);
                enemy.Set(path);
            }
            else
            {
                Debug.LogError($"No prefab assigned for EnemyType: {type}");
            }
        }
    }
}