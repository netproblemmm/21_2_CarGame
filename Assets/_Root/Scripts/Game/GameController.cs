using Tools;
using System;
using Profile;
using Services;
using UnityEngine;
using Game.InputLogic;
using Game.TapeBackground;
using Game.Transport;
using Game.Transport.Bus;
using Game.Transport.Car;
using Features.AbilitySystem;
using Features.AbilitySystem.Abilities;

namespace Game
{
    internal class GameController: BaseController
    {
        private readonly ProfilePlayer _profilePlayer;
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly TapeBackgroundController _tapeBackgroundController;
        private readonly InputGameController _inputGameController;
        private readonly TransportController _transportController;
        private readonly AbilitiesController _abilitiesController;


        public GameController(Transform placeForUI, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();

            _tapeBackgroundController = CreateTapeBackground();
            _inputGameController = CreateInputGameController();
            _transportController = CreateTransportController();
            _abilitiesController = CreateAbilitiesController(placeForUI);

            ServiceLocator.Analytics.SendGameStarted();
        }


        private TapeBackgroundController CreateTapeBackground()
        {
            var tapeBackgroundController = new TapeBackgroundController(_leftMoveDiff, _rightMoveDiff);
            AddController(tapeBackgroundController);

            return tapeBackgroundController;
        }

        private InputGameController CreateInputGameController()
        {
            var inputGameController = new InputGameController(_leftMoveDiff, _rightMoveDiff, _profilePlayer.CurrentTransport);
            AddController(inputGameController);

            return inputGameController;
        }

        private TransportController CreateTransportController()
        {
            TransportController transportController;

            switch (_profilePlayer.CurrentTransport.Type)
            {
                case TransportType.Car:
                    transportController = new CarController();
                    break;
                case TransportType.Bus:
                    transportController = new BusController();
                    break;
                default:
                    throw new ArgumentException(nameof(TransportType));
            }
            AddController(transportController);

            return transportController;
        }

        private AbilitiesController CreateAbilitiesController(Transform placeForUI)
        {
            IAbilityItem[] abilityItems = LoadAbilityItemConfigs();
            IAbilitiesView abilitiesView = LoadAbilitiesView(placeForUI);
            IAbilitiesRepository abilitiesRepository = CreateAbilitiesRepository(abilityItems);
            IAbilityActivator abilityActivator = _transportController;

            var abilitiesController = new AbilitiesController(abilityItems, abilityActivator, abilitiesView, abilitiesRepository);
            
            AddController(abilitiesController);
            return abilitiesController;
        }

        private IAbilityItem[] LoadAbilityItemConfigs()
        {
            ResourcePath path = new ResourcePath("Configs/Ability/AbilityItemConfigDataSource");
            return ContentDataSourceLoader.LoadAbilityItemConfigs(path);
        }
            

        private IAbilitiesRepository CreateAbilitiesRepository(IAbilityItem[] abilityItemConfigs)
        {
            var repository = new AbilitiesRepository(abilityItemConfigs);
            AddRepository(repository);

            return repository;
        }

        private IAbilitiesView LoadAbilitiesView(Transform placeForUi)
        {
            ResourcePath path = new ResourcePath("Prefabs/Ability/AbilitiesView");
            GameObject prefab = ResourcesLoader.LoadPrefab(path);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }
    }
}
