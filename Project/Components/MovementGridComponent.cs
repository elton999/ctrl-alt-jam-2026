using System;
using UmbrellaToolsKit;
using Microsoft.Xna.Framework;
using UmbrellaToolsKit.EditorEngine.Attributes;

namespace Project.Components
{
    public class MovementGridComponent : Component
    {
        public event Action OnFinishAnimation;

        [ShowEditor] private bool _isMoving = false;
        [ShowEditor] private float _totalTime = 0f;
        private Vector2 _initialPosition;
        private Vector2 _direction;

        private const float MOVE_SPEED = 0.13f;
        private const string VALID_CELL = "2";

        public bool IsMoving => _isMoving;

        public override void Start()
        {
            _initialPosition = GameObject.Position;
        }

        public void StartAnimation(Vector2 direction)
        {
            if (IsMoving) return;

            int cellSize = GameObject.Scene.CellSize;
            var grid = GameObject.Scene.Grid;

            var tempPosition = direction * cellSize + GameObject.Position;
            var nextTile = (tempPosition / cellSize).ToPoint();

            if (nextTile.Y < grid.GridCollides.Count && nextTile.X < grid.GridCollides[nextTile.Y].Count)
            {
                string nextCell = grid.GridCollides[nextTile.Y][nextTile.X];
                if (nextCell == VALID_CELL)
                {
                    _initialPosition = GameObject.Position;
                    _isMoving = true;
                    _direction = direction;
                    return;
                }
            }

            OnFinishAnimation?.Invoke();
        }

        public override void Update(float deltaTime)
        {
            if (IsMoving)
            {
                _totalTime += deltaTime;

                if (_totalTime >= MOVE_SPEED)
                {
                    GameObject.Position = _initialPosition + _direction * GameObject.Scene.CellSize;
                    OnFinishAnimation?.Invoke();
                    return;
                }

                var cellSizeDirection = _direction * GameObject.Scene.CellSize;

                GameObject.Position = new Vector2
                (
                    Tweening.EaseInQuad(_initialPosition.X, cellSizeDirection.X, _totalTime, MOVE_SPEED),
                    Tweening.EaseInQuad(_initialPosition.Y, cellSizeDirection.Y, _totalTime, MOVE_SPEED)
                );
            }
        }
    }
}
