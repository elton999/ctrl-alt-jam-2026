using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.Input;
using UmbrellaToolsKit.EditorEngine.Attributes;
using UmbrellaToolsKit.Components.Physics;
using System.Collections.Generic;
using Project.Entities.Obstacles;
using UmbrellaToolsKit.Sprite;

namespace Project.Entities
{
    public class Player : GameObject
    {
        public static Action OnPlayerMove;

        private ActorComponent _actorComponent;
        private AsepriteAnimation _animation;
        
        private float _totalTime = 0f;
        private bool _isMoving = false;
        [ShowEditor] private Vector2 _initialPosition;

        [ShowEditor] private Vector2 _currentTile;
        [ShowEditor] private Vector2 _oldTile;
        private Point _initialTile => (_initialPosition / Scene.CellSize + Vector2.One).ToPoint();
        private Point _nextTileOnLevel => (_initialTile.ToVector2() + _oldTile + (_currentTile - _oldTile)).ToPoint();

        private const float MOVE_SPEED = 0.1f;
        
        public override void Start()
        {
            _actorComponent = AddComponent<ActorComponent>();
            _actorComponent.HasGravity = false;
            _actorComponent.Size = new Point(23);

            Origin = new Vector2(6, Scene.CellSize);
            SpriteEffect = SpriteEffects.FlipHorizontally;

            Sprite = Content.Load<Texture2D>("Sprites/player");
            _animation = new AsepriteAnimation(Content.Load<AsepriteDefinitions>("Sprites/player_animation"));

            _initialPosition = Position;
            tag = "player";
        }

        public override void Update(float deltaTime)
        {
            UpdateKeyboard();

            if (!_isMoving)
                _animation.Play(deltaTime, "idle", AnimationDirection.LOOP);
            if (_isMoving)
                _animation.Play(deltaTime, "running", AnimationDirection.LOOP);

            if (_isMoving)
            {
                var currentPosition = _initialPosition + _oldTile * Scene.CellSize;
                var currentTilePosition = (_currentTile - _oldTile) * Scene.CellSize;
                var nextTilePosition = _currentTile * Scene.CellSize;
                string validPath = "2";

                if (_nextTileOnLevel.Y < Scene.Grid.GridCollides.Count && _nextTileOnLevel.X < Scene.Grid.GridCollides[_nextTileOnLevel.Y].Count)
                {
                    if (Scene.Grid.GridCollides[_nextTileOnLevel.Y][_nextTileOnLevel.X] != validPath)
                    {
                        Log.Write($"[{nameof(Player)}] not avoid movement on current tile: {_nextTileOnLevel} with value: {Scene.Grid.GridCollides[_nextTileOnLevel.Y][_nextTileOnLevel.X]}");
                        OnNotAvoidMovement();
                        return;
                    }

                    var sceneActors = new List<ActorComponent>(Scene.AllActors.ToArray());
                    sceneActors.Remove(_actorComponent);

                    foreach (var actor in sceneActors)
                    {
                        var collisionCheckPosition = _initialPosition + nextTilePosition + _actorComponent.Size.ToVector2().Half();
                        if (UmbrellaToolsKit.Utils.Collision.OverlapCheck(Vector2.One.ToPoint(), collisionCheckPosition, actor.Size, actor.Position))
                        {
                            if (actor.GameObject is ObstacleGameObject)
                            {
                                var obstacle = (ObstacleGameObject)actor.GameObject;
                                if (!obstacle.PassObstacle())
                                {
                                    OnNotAvoidMovement();
                                    return;
                                }
                            }
                        }
                    }

                }
                else
                {
                    OnNotAvoidMovement();
                    return;
                }
                

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

        public override void Draw(SpriteBatch spriteBatch)
        {
            Body = _animation.Body;
            base.Draw(spriteBatch);
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

            if (!LevelManagerEntity.CanRegisterAMove()) return;

            if (KeyBoardHandler.KeyPressed("up"))
            {
                SetMovement();
                _currentTile -= Vector2.UnitY;
                return;
            }

            if (KeyBoardHandler.KeyPressed("down"))
            {
                SetMovement();
                _currentTile += Vector2.UnitY;
                return;
            }

            if (KeyBoardHandler.KeyPressed("left"))
            {
                SetMovement();
                _currentTile -= Vector2.UnitX;
                SpriteEffect = SpriteEffects.None;
                Origin = new Vector2(15, Scene.CellSize);
                return;
            }

            if (KeyBoardHandler.KeyPressed("right"))
            {
                SetMovement();
                _currentTile += Vector2.UnitX;
                SpriteEffect = SpriteEffects.FlipHorizontally;
                Origin = new Vector2(6, Scene.CellSize);
                return;
            }
        }

        private void SetMovement()
        {
            _isMoving = true;
            _oldTile = _currentTile;
            OnPlayerMove?.Invoke();
        }
    }
}
