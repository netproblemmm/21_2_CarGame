using Game;
using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;
    private const TransportType TransportType = Game.TransportType.Bus;
    
    [SerializeField] private Transform _placeForUI;

    private MainController _mainController;

    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, TransportType, InitialState);
        _mainController = new MainController(_placeForUI, profilePlayer);
   }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
