using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Features.AbilitySystem.Abilities
{
    interface IAbilityButtonView
    {
        void Init(Sprite icon, UnityAction click);
        void DeInit();
    }

    internal class AbilityButtonView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;


        private void OnDestroy() => DeInit();


        public void Init(Sprite icon, UnityAction click)
        {
            _icon.sprite = icon;
            _button.onClick.AddListener(click);
        }

        public void DeInit()
        {
            _icon.sprite = null;
            _button.onClick.RemoveAllListeners();
        }
    }
}
