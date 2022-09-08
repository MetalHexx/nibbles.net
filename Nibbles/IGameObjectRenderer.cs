namespace Nibbles
{
    internal interface IGameObjectRenderer
    {
        void Clear(IEnumerable<IGameObject> gameObjects);
        void Render(IEnumerable<IGameObject> gameObjects);
    }
}