using UI;
using Game;
using Profile;
using UnityEngine;
using Features.Shed;

internal class MainController: BaseController
{
    private readonly Transform _placeForUI;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private ShedController _shedController;
    private GameController _gameController;

    public MainController(Transform placeForUI, ProfilePlayer profilePlayer)
    {
        _placeForUI = placeForUI;
        _profilePlayer = profilePlayer;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        DisposeAllControllers();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                DisposeAllControllers();
                _mainMenuController = new MainMenuController(_placeForUI, _profilePlayer);
                break;
            case GameState.Settings:
                DisposeAllControllers();
                _settingsMenuController = new SettingsMenuController(_placeForUI, _profilePlayer);
                break;
            case GameState.Shed:
                DisposeAllControllers();
                _shedController = new ShedController(_placeForUI, _profilePlayer);
                break;
            case GameState.Game:
                DisposeAllControllers();
                _gameController = new GameController(_placeForUI, _profilePlayer);
                break;
            default:
                DisposeAllControllers();
                break;
        }
    }

    private void DisposeAllControllers()
    {
        _mainMenuController?.Dispose();
        _settingsMenuController?.Dispose();
        _gameController?.Dispose();
        _shedController?.Dispose();
    }
}

