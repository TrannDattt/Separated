using System.Collections.Generic;
using Separated.Data;
using Separated.Enums;
using Separated.Maps;
using UnityEngine;

namespace Separated.GameManager
{
    public class MapManager : MonoBehaviour
    {
        // Serialize map datas

        [SerializeField] private Transform _parent;
        [SerializeField] private Gate _gatePrefab;

        private GameMap _curMap;
        private Dictionary<EMap, GameMap> _mapDict = new()
        {

        };

        public void LoadMap(EMap location)
        {
            _curMap = _mapDict[location];
        }

        public void UnloadMap(EMap location)
        {
            //TODO: Spawn map and assign event
        }

        public void ChangeMap(EMap location)
        {
            UnloadMap(_curMap.Location);
            LoadMap(location);
        }
    }

    public class GameMap
    {
        public EMap Location => _curData.Location;

        private MapData _curData;

        public GameMap(MapData data)
        {

        }
    }
}