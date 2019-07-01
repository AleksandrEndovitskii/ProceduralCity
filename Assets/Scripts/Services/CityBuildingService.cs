using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Services
{
    public class CityBuildingService
    {
        public void Initialize()
        {
            // demo implementation
            var instances = Create(100, 100, 2);
        }

        public List<GameObject> Create(int length, int width, int spacing)
        {
            var instances = new List<GameObject>();

            for (var x = 0; x < length; x++)
            {
                for (var z = 0; z < width; z++)
                {
                    var instance = GameManager.Instance.BuildingsFactory.Create(x, z);
                    instance.transform.parent = GameManager.Instance.GameObjectsManager.gameObject.transform;

                    var spawnPositionX = x;
                    var spawnPositionZ = z;
                    var positionX = spawnPositionX * instance.gameObject.transform.lossyScale.x *
                                    spacing;
                    var positionY = instance.gameObject.transform.lossyScale.y / 2; // height
                    var positionZ = spawnPositionZ * instance.gameObject.transform.lossyScale.z *
                                    spacing;
                    instance.transform.localPosition = new Vector3(positionX, positionY, positionZ);

                    instances.Add(instance);
                }
            }

            return instances;
        }
    }
}
