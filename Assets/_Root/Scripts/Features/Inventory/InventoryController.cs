using Tools;
using System;
using UnityEngine;
using JetBrains.Annotations;
using Features.Inventory.Items;
using Object = UnityEngine.Object;
using System.Collections.Generic;

namespace Features.Inventory
{
    internal interface IInventoryController
    {
    }

    internal class InventoryController : BaseController, IInventoryController
    {
        private readonly IInventoryView _view;
        private readonly IInventoryModel _model;
        private readonly IItemsRepository _repository;

        public InventoryController(
            [NotNull] IInventoryView inventoryView,
            [NotNull] IInventoryModel inventoryModel,
            [NotNull] IItemsRepository inventoryRepository)
        {
            _view = inventoryView ?? throw new ArgumentNullException(nameof(inventoryView));
            _model = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));
            _repository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));

            _view.Display(_repository.Items.Values, OnItemClicked);
            InitSelectedItems(_model.EquippedItems);
        }

        private void InitSelectedItems(IEnumerable<string> selectedItemIds)
        {
            foreach (string itemId in selectedItemIds)
                _view.Select(itemId);
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
