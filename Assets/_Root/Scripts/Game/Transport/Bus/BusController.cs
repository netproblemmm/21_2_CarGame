using Tools;
using UnityEngine;

namespace Game.Transport.Bus
{
    internal class BusController : TransportController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Bus");
        private readonly BusView _view;

        public override GameObject ViewGameObject => _view.gameObject;


        public BusController(TransportModel model) : base(model) =>
            _view = LoadView();

        private BusView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<BusView>();
        }
    }
}
