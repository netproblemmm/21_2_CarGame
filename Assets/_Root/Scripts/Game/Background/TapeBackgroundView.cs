using Tools;
using UnityEngine;

namespace Game.Background
{
    internal class TapeBackgroundView: MonoBehaviour
    {
        [SerializeField] private Background[] _backgrounds;

        private ISubscriptionProperty<float> _diff;

        public void Init(ISubscriptionProperty<float> diff)
        {
            _diff = diff;
            _diff.SubscribeOnChange(Move);
        }

        private void OnDestroy()
        {
            _diff?.UnSubscribeOnChange(Move);
        }

        private void Move(float value)
        {
            foreach (var background in _backgrounds)
                background.Move(-value);
        }
    }
}
