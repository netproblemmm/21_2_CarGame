using Tools;
using Profile;
using System;
using System.Collections.Generic;
using UnityEngine;
using Features.Inventory;
using Features.Shed.Upgrade;
using JetBrains.Annotations;
using Features.Inventory.Items;
using UnityObject = UnityEngine.Object;

namespace Features.Shed
{
    internal interface IShedController
    {
    }

    internal class ShedController : BaseController, IShedController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Shed/ShedView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource");

        private readonly ShedView _view;
        private readonly ProfilePlayer _profilePlayer;
        private readonly InventoryController _inventoryController;
        private readonly UpgradeHandlersRepository _upgradeHandlersRepository;

        public ShedController(
            [NotNull] Transform placeForUI,
            [NotNull] ProfilePlayer profilePlayer)
        {
            if (placeForUI == null)
                throw new ArgumentNullException(nameof(placeForUI));

            _profilePlayer
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _upgradeHandlersRepository = CreateRepository();
            _inventoryController = CreateInventoryController(placeForUI);
            _view = LoadView(placeForUI);

            _view.Init(Apply, Back);
        }

        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private InventoryController CreateInventoryController(Transform placeForUI)
        {
            var inventoryView = LoadInventoryView(placeForUI);
            var itemsRepository = CreateItemsRepository();
            var inventoryModel = _profilePlayer.Inventory;

            var inventoryController = new InventoryController(inventoryView, inventoryModel, itemsRepository);
            AddController(inventoryController);

            return inventoryController;
        }

        private IItemsRepository CreateItemsRepository()
        {
            ResourcePath path = new ResourcePath("Configs/Inventory/ItemConfigDataSource");

            ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(path);
            var repository = new ItemsRepository(itemConfigs);
            AddRepository(repository);

            return repository;
        }

        private IInventoryView LoadInventoryView(Transform placeForUI)
        {
            ResourcePath path = new ResourcePath("Prefabs/Inventory/InventoryView");

            GameObject prefab = ResourcesLoader.LoadPrefab(path);
            GameObject objectView = UnityObject.Instantiate(prefab, placeForUI);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }

        private ShedView LoadView(Transform placeForUI)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUI, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }

        private void Apply()
        {
            UpgradeWithEquippedItems(
                _profilePlayer.CurrentTransport,
                _profilePlayer.Inventory.EquippedItems,
                _upgradeHandlersRepository.Items);

            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Apply. Current Speed: {_profilePlayer.CurrentTransport.Speed}");
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Back. Current Speed: {_profilePlayer.CurrentTransport.Speed}");
        }

        private void UpgradeWithEquippedItems(
            IUpgradable upgradable,
            IReadOnlyList<string> equippedItems,
            IReadOnlyDictionary<string, IUpgradeHandler> upgradeHandlers)
        {
            foreach (string itemId in equippedItems)
                if (upgradeHandlers.TryGetValue(itemId, out var handler))
                    handler.Upgrade(upgradable);
        }

        private new void Log(string message) =>
            Debug.Log($"[{GetType().Name}] {message}");
    }
}
