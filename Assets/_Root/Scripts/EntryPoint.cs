using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [Header("Initial Settings")]
    [SerializeField] private InitialProfilePlayer _initialProfilePlayer;

    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUI;

    private MainController _mainController;

    private void Awake()
    {
        var profilePlayer = CreateProfilePlayer(_initialProfilePlayer);
        _mainController = new MainController(_placeForUI, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }


    private ProfilePlayer CreateProfilePlayer(InitialProfilePlayer initialProfilePlayer) =>
        new ProfilePlayer
        (
            initialProfilePlayer.Transport.Speed,
            initialProfilePlayer.Transport.JumpHeight,
            initialProfilePlayer.Transport.Type,
            initialProfilePlayer.State
        );
}
