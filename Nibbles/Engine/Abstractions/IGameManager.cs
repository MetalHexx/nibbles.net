namespace Nibbles.Engine.Abstractions
{
    public interface IGameManager
    {
        Action? GameOver { get; set; }
        void GenerateFrame();
    }
}