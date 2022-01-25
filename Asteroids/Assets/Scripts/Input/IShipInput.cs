
namespace Asteroids
{
    public interface IShipInput
    {
        bool Accelerating { get; }
        float TurnDirection { get; }
    }
}
