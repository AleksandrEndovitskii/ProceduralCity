using System;
using Managers.ResourcesManagers;
using Services;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        // static instance of GameManager which allows it to be accessed by any other script 
        public static GameManager Instance;

        [SerializeField]
        private BuildingsManager _buildingsManagerPrefab;

        [NonSerialized]
        public BuildingsManager BuildingsManager;

        public CityBuildingService CityBuildingService;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                DontDestroyOnLoad(gameObject); // sets this to not be destroyed when reloading scene 
            }
            else
            {
                if (Instance != this)
                {
                    // this enforces our singleton pattern, meaning there can only ever be one instance of a GameManager 
                    Destroy(gameObject);
                }
            }

            Initialize();
        }

        public void Initialize()
        {
            BuildingsManager = Instantiate(_buildingsManagerPrefab, this.gameObject.transform);
            BuildingsManager.Initialize("Buildings");

            // start point
            CityBuildingService.Initialize();
        }
    }
}
