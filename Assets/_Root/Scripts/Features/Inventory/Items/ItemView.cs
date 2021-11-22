using Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Features.Inventory.Items
{
    internal interface IItemView
    {
        void Init(IItem item, UnityAction clickAction);
        void DeInit();

        void Select();
        void Unselect();
    }

    internal class ItemView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private CustomText _text;
        [SerializeField] private Button _button;

        [SerializeField] private GameObject _selectedBackground;
        [SerializeField] private GameObject _unselectedBackground;

        private string _itemId = string.Empty;

        private void OnDestroy() => DeInit();

        public void Init(IItem item, UnityAction clickAction)
        {
            _image.sprite = item.Info.Icon;
            _text.Text = item.Info.Title;
            _itemId = item.Id;
            _button.onClick.AddListener(clickAction);
        }

        public void DeInit()
        {
            _image.sprite = null;
            _text.Text = string.Empty;
            _itemId = string.Empty;
            _button.onClick.RemoveAllListeners();
        }


        public void Select() => SetSelected(true);
        public void Unselect() => SetSelected(false);

        private void SetSelected(bool isSelected)
        {
            _selectedBackground.SetActive(isSelected);
            _unselectedBackground.SetActive(!isSelected);
        }
    }
}
