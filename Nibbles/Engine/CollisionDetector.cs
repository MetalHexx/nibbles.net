using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibbles.Engine
{
    public class CollisionPair
    {   
        public ISprite Sprite1 { get; init; }
        public ISprite Sprite2 { get; init; }
        public Action[] GameEvents { get; init; }

        public CollisionPair(ISprite sprite1, ISprite sprite2, params Action[] eventsToTake)
        {
            if (eventsToTake is null || eventsToTake.Length == 0) throw new Exception("You must supply at least one event");

            Sprite1 = sprite1;
            Sprite2 = sprite2;
            GameEvents = eventsToTake;
        }
    }

    public interface ICollisionDetector
    {
        void Register(ISprite sprite1, ISprite sprite2, params Action [] eventsToTake);
        void Detect();
    }

    public class CollisionDetector : ICollisionDetector
    {
        private readonly GameState _state;

        private readonly List<CollisionPair> _collisionPairs = new();

        public CollisionDetector(GameState state) => _state = state;

        public void Register(ISprite sprite1, ISprite sprite2, params Action[] eventsToTake)
        {
            var collisionPair = new CollisionPair(sprite1, sprite2, eventsToTake);
            _collisionPairs.Add(collisionPair);
        }

        public void Detect()
        {
            var collisionsToInvoke = new List<CollisionPair>();

            foreach (var pair in _collisionPairs)
            {
                if (pair.Sprite1.Position == pair.Sprite2.Position)
                {
                    collisionsToInvoke.Add(pair);                    
                }
            }
            foreach (var pair in collisionsToInvoke)
            {
                if (pair.GameEvents is null) continue;

                foreach (var gameEvent in pair.GameEvents)
                {
                    gameEvent.Invoke();
                }
                _collisionPairs.Remove(pair);
            }
        }
    }
}
