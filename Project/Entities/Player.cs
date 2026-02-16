using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Input;
using UmbrellaToolsKit.Components.Sprite;
using UmbrellaToolsKit.EditorEngine.Attributes;
using UmbrellaToolsKit.EditorEngine;

namespace Project.Entities
{
    public class Player : GameObject
    {
        private float _totalTime = 0f;
        
        private SpriteComponent _spriteComponent;
        private Vector2 _initialPosition;
        [ShowEditor] private Vector2 _currentTile;
        [ShowEditor] private Vector2 _oldTile;
        private bool _isMoving = false;
        private Point _nextTileOnLevel => (Position / Scene.CellSize + (_currentTile - _oldTile)).ToPoint();

        private const float MOVE_SPEED = 0.08f;
        
        public override void Start()
        {
            _spriteComponent = AddComponent<SpriteComponent>();
            _spriteComponent.SetAtlas("player sprite");
            _spriteComponent.Origin = new Vector2(6, 22);
            _spriteComponent.SpriteEffect = SpriteEffects.FlipHorizontally;

            _initialPosition = Position;
        }

        public override void Update(float deltaTime)
        {
            UpdateKeyboard();

            if (_isMoving)
            {
                if (_nextTileOnLevel.X < Scene.Grid.GridCollides.Count && _nextTileOnLevel.Y < Scene.Grid.GridCollides[_nextTileOnLevel.X].Count)
                {
                    if (Scene.Grid.GridCollides[_nextTileOnLevel.X][_nextTileOnLevel.Y] != "2")
                    {
                        OnNotAvoidMovement();
                        return;
                    }
                }
                else
                {
                    OnNotAvoidMovement();
                    return;
                }


                var currentPosition = _initialPosition + _oldTile * Scene.CellSize;
                var currentTilePosition = (_currentTile - _oldTile) * Scene.CellSize;
                var nextTilePosition = _currentTile * Scene.CellSize;

                _totalTime += deltaTime;

                if (_totalTime >= MOVE_SPEED)
                {
                    ResetMoveAnimation();
                    Position = _initialPosition + nextTilePosition;
                    return;
                }

                Position = new Vector2
                (
                    Tweening.LinearTween(currentPosition.X, currentTilePosition.X, _totalTime, MOVE_SPEED),
                    Tweening.LinearTween(currentPosition.Y, currentTilePosition.Y, _totalTime, MOVE_SPEED)
                );
            }
        }

        private void OnNotAvoidMovement()
        {
            _currentTile = _oldTile;
            ResetMoveAnimation();
        }

        private void ResetMoveAnimation()
        {
            _totalTime = 0f;
            _isMoving = false;
            _oldTile = _currentTile;
        }

        private void UpdateKeyboard()
        {
            if (_isMoving) return;

            if (KeyBoardHandler.KeyPressed("up"))
            {
                _isMoving = true;
                _oldTile = _currentTile;
                _currentTile -= Vector2.UnitY;
                return;
            }

            if (KeyBoardHandler.KeyPressed("down"))
            {
                _isMoving = true;
                _oldTile = _currentTile;
                _currentTile += Vector2.UnitY;
                return;
            }

            if (KeyBoardHandler.KeyPressed("left"))
            {
                _isMoving = true;
                _oldTile = _currentTile;
                _currentTile -= Vector2.UnitX;
                _spriteComponent.SpriteEffect = SpriteEffects.None;
                _spriteComponent.Origin = new Vector2(15, 22);
                return;
            }

            if (KeyBoardHandler.KeyPressed("right"))
            {
                _isMoving = true;
                _oldTile = _currentTile;
                _currentTile += Vector2.UnitX;
                _spriteComponent.SpriteEffect = SpriteEffects.FlipHorizontally;
                _spriteComponent.Origin = new Vector2(6, 22);
                return;
            }
        }
    }
}
