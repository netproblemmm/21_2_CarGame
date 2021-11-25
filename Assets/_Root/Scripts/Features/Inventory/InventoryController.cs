using Tools;
using System;
using UnityEngine;
using JetBrains.Annotations;
using Features.Inventory.Items;
using Object = UnityEngine.Object;

namespace Features.Inventory
{
    internal interface IInventoryController
    {
    }

    internal class InventoryController : BaseController, IInventoryController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Inventory/InventoryView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Inventory/ItemConfigDataSource");

        private readonly InventoryView _view;
        private readonly IInventoryModel _model;
        private readonly ItemsRepository _repository;


        public InventoryController(
            [NotNull] Transform placeForUI,
            [NotNull] IInventoryModel inventoryModel)
        {
            if (placeForUI == null)
                throw new ArgumentNullException(nameof(placeForUI));

            _model
                = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));

            _repository = CreateRepository();
            _view = LoadView(placeForUI);

            _view.Display(_repository.Items.Values, OnItemClicked);

            foreach (string itemId in _model.EquippedItems)
                _view.Select(itemId);
        }


        private ItemsRepository CreateRepository()
        {
            ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(_dataSourcePath);
            var repository = new ItemsRepository(itemConfigs);
            AddRepository(repository);

            return repository;
        }

        private InventoryView LoadView(Transform placeForUI)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUI);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }


        private void OnItemClicked(string itemId)
        {
            bool equipped = _model.IsEquipped(itemId);

            if (equipped)
                UnEquipItem(itemId);
            else
                EquipItem(itemId);
        }

        private void EquipItem(string itemId)
        {
            _view.Select(itemId);
            _model.EquipItem(itemId);
        }

        private void UnEquipItem(string itemId)
        {
            _view.Unselect(itemId);
            _model.UnequipItem(itemId);
        }
    }
}
