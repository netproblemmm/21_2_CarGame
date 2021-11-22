using Features.Inventory;
using Game;
using Game.Transport;
using Tools;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly InventoryModel InventoryModel;
        public readonly TransportModel CurrentTransport;

        public ProfilePlayer(float transportSpeed, TransportType transportType, GameState initialState)
        {
            CurrentState = new SubscriptionProperty<GameState>(initialState);
            InventoryModel = new InventoryModel();
            CurrentTransport = new TransportModel(transportSpeed, transportType);
        }
    }
}

