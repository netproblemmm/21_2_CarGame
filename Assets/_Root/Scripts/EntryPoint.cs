using Profile;
using UnityEngine;
using Game.Car;

internal class EntryPoint : MonoBehaviour
{
    private const GameState InitialGameState = GameState.Start;
    private const float _carSpeed = 5f;
    private CarModel _carModel = new CarModel(_carSpeed, CarType.Bus);
    [SerializeField] private Transform _placeForUI;
    private MainController _mainController;


    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(InitialGameState, _carModel, _carSpeed);
        _mainController = new MainController(_placeForUI, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
