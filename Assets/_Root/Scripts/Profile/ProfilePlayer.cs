using Game.Car;
using Tools;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> State;
        public readonly CarModel CarModel;
        //private CarType CarType;

        public ProfilePlayer(GameState state, CarModel carModel, float speed)
        {
            State = new SubscriptionProperty<GameState>
            {
                Value = state
            };
            CarModel = new CarModel(speed, carModel.CarType);
        }

    }
}

