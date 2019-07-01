using System;
using Factories;
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
        private GameObjectsManager _gameObjectsManagerPrefab;
        [SerializeField]
        private UserInterfaceManager _userInterfaceManagerPrefab;
        [SerializeField]
        private BuildingsManager _buildingsManagerPrefab;

        [NonSerialized]
        public GameObjectsManager GameObjectsManager;
        [NonSerialized]
        public UserInterfaceManager UserInterfaceManager;
        [NonSerialized]
        public BuildingsManager BuildingsManager;

        public BuildingsFactory BuildingsFactory;
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
            GameObjectsManager = Instantiate(_gameObjectsManagerPrefab, this.gameObject.transform);
            GameObjectsManager.Initialize();
            UserInterfaceManager = Instantiate(_userInterfaceManagerPrefab, this.gameObject.transform);
            BuildingsManager = Instantiate(_buildingsManagerPrefab, this.gameObject.transform);
            BuildingsManager.Initialize("Buildings");

            BuildingsFactory = new BuildingsFactory();
            BuildingsFactory.Initialize();

            CityBuildingService = new CityBuildingService();
            // user interface need spawned buildings from city building service
            UserInterfaceManager.Initialize();
            CityBuildingService.Initialize(); // start point
        }
    }
}
