using Game;
using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private float SpeedCar = 15f;
    [SerializeField] private GameState InitialState = GameState.Start;
    [SerializeField] private TransportType TransportType = Game.TransportType.Bus;
    
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
