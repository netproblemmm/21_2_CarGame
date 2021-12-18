using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    internal class GameMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonMenu;
        [SerializeField] private Button _buttonPause;

        public void Init(UnityAction back, UnityAction pause)
        {
            _buttonMenu.onClick.AddListener(back);
            _buttonPause.onClick.AddListener(pause);
        }

        public void OnDestroy()
        {
            _buttonMenu.onClick.RemoveAllListeners();
            _buttonPause.onClick.RemoveAllListeners();
        }
    }
}
