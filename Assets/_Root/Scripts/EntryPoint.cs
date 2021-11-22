using Profile;
using UnityEngine;
using Game.Car;
using Services.Analytics;
using Services.Ads.UnityAds;
using System.Collections;
using Services.IAP;

internal class EntryPoint : MonoBehaviour
{
    private const GameState InitialGameState = GameState.Start;
    private const float _carSpeed = 5f;
    private CarModel _carModel = new CarModel(_carSpeed, CarType.Bus);
    
    [SerializeField] private Transform _placeForUI;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private AnalyticsManager _analytics;
    [SerializeField] private IAPService _iapService;

    private MainController _mainController;

    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(InitialGameState, _carModel, _carSpeed);
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
