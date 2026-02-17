using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Input;
using UmbrellaToolsKit.Components.Sprite;
using UmbrellaToolsKit.EditorEngine.Attributes;

namespace Project.Entities
{
    public class Player : GameObject
    {
        private SpriteComponent _spriteComponent;
        
        private float _totalTime = 0f;
        private bool _isMoving = false;
        private Vector2 _initialPosition;

        [ShowEditor] private Vector2 _currentTile;
        [ShowEditor] private Vector2 _oldTile;
        private Point _nextTileOnLevel => (_initialPosition / Scene.CellSize + _oldTile + (_currentTile - _oldTile)).ToPoint();

        private const float MOVE_SPEED = 0.13f;
        
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
                if (_nextTileOnLevel.Y < Scene.Grid.GridCollides.Count && _nextTileOnLevel.X < Scene.Grid.GridCollides[_nextTileOnLevel.Y].Count)
                {
                    if (Scene.Grid.GridCollides[_nextTileOnLevel.Y][_nextTileOnLevel.X] != "2")
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
                    Tweening.EaseInQuad(currentPosition.X, currentTilePosition.X, _totalTime, MOVE_SPEED),
                    Tweening.EaseInQuad(currentPosition.Y, currentTilePosition.Y, _totalTime, MOVE_SPEED)
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
                SetMoviment();
                _currentTile -= Vector2.UnitY;
                return;
            }

            if (KeyBoardHandler.KeyPressed("down"))
            {
                SetMoviment();
                _currentTile += Vector2.UnitY;
                return;
            }

            if (KeyBoardHandler.KeyPressed("left"))
            {
                SetMoviment();
                _currentTile -= Vector2.UnitX;
                _spriteComponent.SpriteEffect = SpriteEffects.None;
                _spriteComponent.Origin = new Vector2(15, 22);
                return;
            }

            if (KeyBoardHandler.KeyPressed("right"))
            {
                SetMoviment();
                _currentTile += Vector2.UnitX;
                _spriteComponent.SpriteEffect = SpriteEffects.FlipHorizontally;
                _spriteComponent.Origin = new Vector2(6, 22);
                return;
            }
        }

        private void SetMoviment()
        {
            _isMoving = true;
            _oldTile = _currentTile;
        }
    }
}
