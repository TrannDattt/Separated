using System;
using Separated.Enums;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Data/MapData")]
    public class MapData : ScriptableObject
    {
        public EMap Location;
        public GameObject[] Backgrounds;
        public GameObject[] Foregrounds;
        public Tilemap Map;
        public MapGate[] Gates;
        public MapEnemy[] Enemies;
    }

    [Serializable]
    public class MapObject
    {
        public Vector2 Position;
    }

    public class MapGate : MapObject
    {
        public EMap Destination;
    }

    public class MapEnemy : MapObject
    {
        public EBeastType EnemyType;
    }

    // public class MapOnetimeEvent (chess, quiz, boss)
    // public class MapObstacle
}