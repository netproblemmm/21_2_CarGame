namespace Game.Car
{
    public class CarModel
    {
        public readonly float Speed;
        public readonly CarType CarType;

        public CarModel (float speed, CarType carType)
        {
            Speed = speed;
            CarType = carType;
        }
    }
}

