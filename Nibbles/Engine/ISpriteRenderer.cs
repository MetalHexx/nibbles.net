using Nibbles.GameObject;

namespace Nibbles.Engine
{
    internal interface ISpriteRenderer
    {
        void RenderSprites(SpriteRenderUpdate update);
        void RenderScore(GameState state);
        void RenderBoard(Board board);
    }
}