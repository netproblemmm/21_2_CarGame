using Profile;
using Tools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI
{
    internal class SettingsMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/SettingsMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly SettingsMenuView _view;

        public SettingsMenuController(Transform placeForUI, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUI);
            _view.Init(Back);
        }

        private SettingsMenuView LoadView(Transform placeForUI)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUI, false);
            AddGameObject(objectView);

            return objectView.GetComponent<SettingsMenuView>();
        }

        private void Back() =>
            _profilePlayer.State.Value = GameState.Start;
    }
}
