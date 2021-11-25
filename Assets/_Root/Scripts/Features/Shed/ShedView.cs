using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Features.Shed
{
    internal interface IShedView
    {
        void Init(UnityAction apply, UnityAction back);
        void DeInit();
    }

    internal class ShedView : MonoBehaviour, IShedView
    {
        [SerializeField] private Button _buttonApply;
        [SerializeField] private Button _buttonBack;


        private void OnDestroy() => DeInit();

        public void Init(UnityAction apply, UnityAction back)
        {
            _buttonApply.onClick.AddListener(apply);
            _buttonBack.onClick.AddListener(back);
        }

        public void DeInit()
        {
            _buttonApply.onClick.RemoveAllListeners();
            _buttonBack.onClick.RemoveAllListeners();
        }
    }
}
