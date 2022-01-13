using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public interface IEmptyPositionsProvider
    {
        IEnumerable<Vector2> EmptyPositions { get; }
    }
}
