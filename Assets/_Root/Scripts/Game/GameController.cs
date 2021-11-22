using Game.Background;
using Game.Car;
using Game.InputLogic;
using Profile;
using Tools;

namespace Game
{
    internal class GameController: BaseController
    {
        private CarModel _carModel;
        public GameController(ProfilePlayer profilePlayer)
        {
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CarModel);
            AddController(inputGameController);

            var carController = new CarController(profilePlayer.CarModel);
            AddController(carController);
        }
    }
}
