using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

namespace Factories
{
    public class BuildingsFactory
    {
        private List<GameObject> _prefabs = new List<GameObject>();

        private static System.Random _random = new System.Random();

        public void Initialize()
        {
            _prefabs = GameManager.Instance.BuildingsManager.GetAll().ToList().ConvertAll(x => (GameObject)x);
        }

        public GameObject Create()
        {
            var randomPrefab = _prefabs[_random.Next(0, _prefabs.Count)];
            var instance = GameObject.Instantiate(randomPrefab);

            return instance;
        }
    }
}
