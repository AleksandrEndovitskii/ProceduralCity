using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UserInterfaceManager : MonoBehaviour
    {
        [SerializeField]
        private Canvas _windowsCanvasPrefab;
        [SerializeField]
        private VerticalLayoutGroup _countersContainerPrefab;
        [SerializeField]
        private TextMeshProUGUI _counterTMPTextPrefab;

        private Canvas _windowsCanvasInstance;
        private VerticalLayoutGroup _countersContainerInstance;
        private List<TextMeshProUGUI> _countersInstances = new List<TextMeshProUGUI>();

        public void Initialize()
        {
            _windowsCanvasInstance = Instantiate(_windowsCanvasPrefab);
            _countersContainerInstance = Instantiate(_countersContainerPrefab, _windowsCanvasInstance.gameObject.transform);

            GameManager.Instance.CityBuildingService.InstancesCreated += CityBuildingServiceOnInstancesCreated;
        }

        private void CityBuildingServiceOnInstancesCreated(List<GameObject> gameObjects)
        {
            ClearCounters();
            InstantiateCountersForGameObjects(gameObjects);
        }

        private void ClearCounters()
        {
            foreach (var counterInstance in _countersInstances)
            {
                Destroy(counterInstance.gameObject);
            }
            _countersInstances.Clear();
        }

        private void InstantiateCountersForGameObjects(List<GameObject> gameObjects)
        {
            var groupedGameObjects = gameObjects.GroupBy(x => x.name);
            foreach (var groupOfGameObjects in groupedGameObjects)
            {
                var counterInstance =
                    Instantiate(_counterTMPTextPrefab, _countersContainerInstance.gameObject.transform);
                counterInstance.text = string.Format("{0}: {1}", groupOfGameObjects.Key, groupOfGameObjects.Count());

                _countersInstances.Add(counterInstance);
            }
        }
    }
}
