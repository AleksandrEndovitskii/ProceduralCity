using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

namespace Factories
{
    public class BuildingsFactory
    {
        private List<GameObject> _prefabs = new List<GameObject>();

        public void Initialize()
        {
            _prefabs = GameManager.Instance.BuildingsManager.GetAll().ToList().ConvertAll(x => (GameObject)x);
        }

        public GameObject Create(int x, int z)
        {
            var noiseUpperBound = _prefabs.Count * 2;

            var noise = (int) (Mathf.PerlinNoise(
                                   z / (float) noiseUpperBound,
                                   x / (float) noiseUpperBound)
                               * noiseUpperBound);

            GameObject randomPrefab = null;

            for (var i = _prefabs.Count - 1; i >= 0; i--)
            {
                if (noise <= (i + 1) * 2)
                {
                    randomPrefab = _prefabs[i];
                }
            }

            var instance = GameObject.Instantiate(randomPrefab);

            return instance;
        }

        public GameObject Create(GameObject prefab)
        {
            var instance = GameObject.Instantiate(prefab);

            return instance;
        }

        public GameObject GetPrefab(int x, int z)
        {
            var noiseUpperBound = _prefabs.Count * 2;

            var noise = (int)(Mathf.PerlinNoise(
                                  z / (float)noiseUpperBound,
                                  x / (float)noiseUpperBound)
                              * noiseUpperBound);

            GameObject randomPrefab = null;

            for (var i = _prefabs.Count - 1; i >= 0; i--)
            {
                if (noise <= (i + 1) * 2)
                {
                    randomPrefab = _prefabs[i];
                }
            }

            return randomPrefab;
        }
    }
}
