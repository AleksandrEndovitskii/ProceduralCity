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

        public void Initialize()
        {
            // demo implementation
            _map = Create(100, 100, 2);

            for (var x = 0; x < _map.GetLength(0); x++)
            {
                for (var z = 0; z < _map.GetLength(1); z++)
                {
                    _instances.Add(_map[x, z]);
                }
            }

            _instances = _instances.Where(x => x != null).ToList();

            InstancesCreated.Invoke(_instances);
        }

        public GameObject[,] Create(int length, int width, int spacing)
        {
            var map = new GameObject[length, width];

            var mapBlueprint = new GameObject[length, width];

            for (var x = 0; x < length; x++)
            {
                for (var z = 0; z < width; z++)
                {
                    mapBlueprint[x,z] = GameManager.Instance.BuildingsFactory.GetPrefab(x, z);
                }
            }

            for (var x = 0; x < mapBlueprint.GetLength(0); x++)
            {
                for (var z = 0; z < mapBlueprint.GetLength(1); z++)
                {
                    var instance = GameManager.Instance.BuildingsFactory.Create(mapBlueprint[x,z]);
                    instance.transform.parent = GameManager.Instance.GameObjectsManager.gameObject.transform;

                    var spawnPositionX = x;
                    var spawnPositionZ = z;
                    var positionX = spawnPositionX;// * instance.gameObject.transform.lossyScale.x * spacing;
                    var positionY = instance.gameObject.transform.lossyScale.y / 2; // height
                    var positionZ = spawnPositionZ;// * instance.gameObject.transform.lossyScale.z; * spacing;
                    instance.transform.localPosition = new Vector3(positionX, positionY, positionZ);
                }
            }

            return map;
        }
    }
}
