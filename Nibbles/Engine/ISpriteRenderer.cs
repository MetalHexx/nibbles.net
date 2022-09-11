using Nibbles.GameObject;

namespace Nibbles.Engine
{
    internal interface ISpriteRenderer
    {
        void Clear(IEnumerable<ISprite> gameObjects);
        void Render(IEnumerable<ISprite> gameObjects);
        void RenderScore(GameState state);
        void RenderBoard(Board board);
    }
}