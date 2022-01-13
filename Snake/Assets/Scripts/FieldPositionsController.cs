using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SnakeGame
{
    public class FieldPositionsController : IEmptyPositionsProvider
    {
        private readonly ISnake _snake;
        private Vector2 _previousHeadPosition;
        private Vector2 _previousTipPosition;
        private HashSet<Vector2> _emptyPositions;

        public FieldPositionsController(ISnake snake, Bounds fieldArea)
        {
            _snake = snake;
            InitializeEmptyPositions(fieldArea);
            AdjustPreviousHeadTipPositions();
        }

        public event Action AllPositionsOccupied = delegate { };
        
        public IEnumerable<Vector2> EmptyPositions
        {
            get { return _emptyPositions; }
        }

        public void Update()
        {
            bool snakeMoved = _previousHeadPosition != _snake.Head.Position;
            if (snakeMoved)
            {
                AdjustEmptyPositions();
            }
        }

        private void AdjustEmptyPositions()
        {
            if (!_emptyPositions.Any())
            {
                AllPositionsOccupied();
                return;
            }

            Debug.Assert(_emptyPositions.Remove(_snake.Head.Position), _snake.Head.Position);
            bool tipMoved = _previousTipPosition != _snake.Tip.Position;
            if (tipMoved)
            {
                Debug.Assert(_emptyPositions.Add(_previousTipPosition), _previousTipPosition);
            }
            AdjustPreviousHeadTipPositions();
        }

        private void InitializeEmptyPositions(Bounds fieldArea)
        {
            // TODO: Introduce a GridSize instead of offsets and "x++ / y++".

            _emptyPositions = new HashSet<Vector2>();
            float yMin = Mathf.Round(fieldArea.min.y) + 1f;
            float yMax = Mathf.Round(fieldArea.max.y);
            float xMin = Mathf.Round(fieldArea.min.x);
            float xMax = Mathf.Round(fieldArea.max.x) - 1f;

            for (float y = yMin; y <= yMax; y++)
            {
                for (float x = xMin; x <= xMax; x++)
                {
                    _emptyPositions.Add(new Vector2(x, y));
                }
            }
            Debug.Assert(_emptyPositions.Remove(_snake.Head.Position), _snake.Head.Position);
            Debug.Assert(_emptyPositions.Remove(_snake.Tip.Position), _snake.Tip.Position);
        }

        private void AdjustPreviousHeadTipPositions()
        {
            _previousHeadPosition = _snake.Head.Position;
            _previousTipPosition = _snake.Tip.Position;
        }
    }
}
