using Game;
using Profile;
using UnityEngine;
using Services.Analytics;
using Services.Ads.UnityAds;
using Services.IAP;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;
    private const TransportType TransportType = Game.TransportType.Bus;
    
    [SerializeField] private Transform _placeForUI;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private AnalyticsManager _analytics;
    [SerializeField] private IAPService _iapService;

    private MainController _mainController;

    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, TransportType, InitialState);
        _mainController = new MainController(_placeForUI, profilePlayer);

        _iapService.Initialized.AddListener(OnIapInitialized);
        _adsService.Initialized.AddListener(_adsService.InterstitialPlayer.Play);
        _analytics.SendMainMenuOpened();
    }

    private void OnDestroy()
    {
        _iapService.Initialized.RemoveListener(OnIapInitialized);
        _adsService.Initialized.RemoveListener(OnAdsInitialized);
        _mainController.Dispose();
    }


    private void OnIapInitialized() => _iapService.Buy("product_1");
    private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();
}
