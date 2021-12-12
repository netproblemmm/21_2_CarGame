using Tools;
using Profile;
using UnityEngine;

namespace UI
{
    internal class GameMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Game/GameMenu");
        private readonly PauseMenuController _pauseMenuController;
        private readonly ProfilePlayer _profilePlayer;
        private readonly GameMenuView _view;

        public GameMenuController(Transform placeForUI, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUI);
            _view.Init(Back, Pause);

            _pauseMenuController = new PauseMenuController(placeForUI, profilePlayer);
            AddController(_pauseMenuController);
            _pauseMenuController.Hide();
        }

        private GameMenuView LoadView(Transform placeForUI)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUI, false);
            AddGameObject(objectView);

            return objectView.GetComponent<GameMenuView>();
        }

        private void Back() =>
            _profilePlayer.CurrentState.Value = GameState.Start;

        private void Pause() =>
            _pauseMenuController.Show();
    }
}
