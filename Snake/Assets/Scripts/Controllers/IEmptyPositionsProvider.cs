using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Controllers
{
    public interface IEmptyPositionsProvider
    {
        IEnumerable<Vector2> EmptyPositions { get; }
    }
}
