using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

namespace Factories
{
    public class HumansFactory
    {
        private List<GameObject> _prefabs = new List<GameObject>();

        private static System.Random _random = new System.Random();

        public void Initialize()
        {
            _prefabs = GameManager.Instance.HumansManager.GetAll().ToList().ConvertAll(x => (GameObject)x);
        }

        public GameObject Create()
        {
            var randomPrefab = _prefabs[_random.Next(0, _prefabs.Count)];

            GameObject instance = null;
            if (_random.Next(0, 2) > 0) // 50% chance to spawn
            {
                instance = GameObject.Instantiate(randomPrefab);
            }

            return instance;
        }
    }
}
