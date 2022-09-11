namespace Nibbles.GameObject
{
    internal interface IMoveableSprite: ISprite
    {
        void Move(PositionTransform transform);
    }
}
