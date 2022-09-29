namespace Nibbles.Engine.Abstractions
{
    public interface IGameStateReducer
    {
        Action? GameOver { get; set; }
        void GenerateFrame();
    }
}