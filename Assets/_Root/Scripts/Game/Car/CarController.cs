using Tools;
using UnityEngine;

namespace Game.Car
{
    internal class CarController : BaseController
    {
        private ResourcePath _viewPath;
        private readonly CarView _view;
        public GameObject ViewGameObject => _view.gameObject;

        public CarController(CarModel _carModel)
        {
            _view = LoadView(_carModel);
        }

        private CarView LoadView(CarModel _carModel)
        {
            switch (_carModel.CarType)
            {
                case CarType.Car:
                    _viewPath = new ResourcePath("Prefabs/Car");
                    break;
                case CarType.Bus:
                    _viewPath = new ResourcePath("Prefabs/Bus");
                    break;
                default:
                    break;
            }

            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<CarView>();
        }
    }
}

