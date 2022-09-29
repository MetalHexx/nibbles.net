
namespace Nibbles.Engine
{
    public interface IGameLoop
    {
        Action? GameOver { get; set; }
        void Start();
    }
}