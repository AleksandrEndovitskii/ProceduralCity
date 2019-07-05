using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

namespace Services
{
    public class CityBuildingService
    {
        public Action<List<GameObject>> InstancesCreated = delegate { };

        private List<GameObject> _instances = new List<GameObject>();

        private GameObject[,] _map;

        private List<GameObject> MapFreeSpaces
        {
            get
            {
                var result = new List<GameObject>();

                for (var x = 0; x < _map.GetLength(0); x++)
                {
                    for (var z = 0; z < _map.GetLength(1); z++)
                    {
                        result.Add(_map[x, z]);
                    }
                }

                result = result.Where(x => x == null).ToList();

                return result;
            }
        }

        private List<GameObject> MapBusySpaces
        {
            get
            {
                var result = new List<GameObject>();

                for (var x = 0; x < _map.GetLength(0); x++)
                {
                    for (var z = 0; z < _map.GetLength(1); z++)
                    {
                        result.Add(_map[x, z]);
                    }
                }

                result = result.Where(x => x != null).ToList();

                return result;
            }
        }

        public void Initialize()
        {
            // demo implementation
            _map = Create(100, 100);

            _instances = MapBusySpaces;

            InstancesCreated.Invoke(_instances);
        }

        private GameObject[,] Create(int length, int width)
        {
            var map = new GameObject[length, width];

            var mapBlueprint = new GameObject[length, width];

            // generating blueprint map
            for (var x = 0; x < length; x++)
            {
                for (var z = 0; z < width; z++)
                {
                    mapBlueprint[x,z] = GameManager.Instance.BuildingsFactory.GetPrefab(x, z);
                }
            }

            // generating spacings in blueprint map
            for (var x = 1; x < mapBlueprint.GetLength(0); x += 2)
            {
                for (var z = 0; z < mapBlueprint.GetLength(1); z += 1)
                {
                    mapBlueprint[x, z] = null;
                }
            }
            for (var z = 1; z < mapBlueprint.GetLength(1); z += 2)
            {
                for (var x = 0; x < mapBlueprint.GetLength(0); x += 1)
                {
                    mapBlueprint[x, z] = null;
                }
            }

            // generating map based on map blueprint
            for (var x = 0; x < mapBlueprint.GetLength(0); x++)
            {
                for (var z = 0; z < mapBlueprint.GetLength(1); z++)
                {
                    // its empty cell - nothing to do with it - skip it here
                    if (mapBlueprint[x, z] == null)
                    {
                        continue;
                    }

                    var instance = GameManager.Instance.BuildingsFactory.Create(mapBlueprint[x,z]);
                    instance.transform.parent = GameManager.Instance.GameObjectsManager.gameObject.transform;

                    var spawnPositionX = x;
                    var spawnPositionZ = z;
                    var positionX = spawnPositionX;
                    var positionY = instance.gameObject.transform.lossyScale.y / 2; // height
                    var positionZ = spawnPositionZ;
                    instance.transform.localPosition = new Vector3(positionX, positionY, positionZ);

                    map[x, z] = instance;
                }
            }

            // generating humans on map based on map blueprint
            for (var x = 0; x < mapBlueprint.GetLength(0); x++)
            {
                for (var z = 0; z < mapBlueprint.GetLength(1); z++)
                {
                    // its busy cell - nothing to do with it - skip it here
                    if (mapBlueprint[x, z] != null)
                    {
                        continue;
                    }

                    // TODO: copy/paste from above - refactor this in future
                    var instance = GameManager.Instance.HumansFactory.Create();
                    // human was not spawned at all - nothing to do with it - skip it here
                    if (instance == null)
                    {
                        continue;
                    }
                    instance.transform.parent = GameManager.Instance.GameObjectsManager.gameObject.transform;

                    var spawnPositionX = x;
                    var spawnPositionZ = z;
                    var positionX = spawnPositionX;
                    var positionY = instance.gameObject.transform.lossyScale.y / 2; // height
                    var positionZ = spawnPositionZ;
                    instance.transform.localPosition = new Vector3(positionX, positionY, positionZ);

                    map[x, z] = instance;
                }
            }

            return map;
        }
    }
}
