using System;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySpawnSystem
{
    [Serializable]
    public class SpawnPointDto
    {
        public int id;
        public List<Vector3> pathWaypoints;
    }
}