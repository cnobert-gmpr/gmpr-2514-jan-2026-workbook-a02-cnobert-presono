using System.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson08MosquitoAttack;

public class Mosquito
{
    private SimpleAnimation _animation;

    private Vector2 _position;
    private Vector2 _direction;
    private float _speed;

    private Rectangle _gameBoundingBox;
    private enum State
    {
        Alive, Dying, Dead
    }
    private State _state;
    internal Rectangle BoundingBox
    {
        get
        {
            return new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                (int)_animation.FrameDimensions.X,
                (int)_animation.FrameDimensions.Y
            );
        }
    }

    internal void Initialize(Vector2 position, float speed, Vector2 direction, Rectangle gameBoundingBox)
    {
        _position = position;
        _speed = speed;
        _direction = direction;
        _gameBoundingBox = gameBoundingBox;
    }

    internal void LoadContent(ContentManager content)
    {
        Texture2D texture = content.Load<Texture2D>("Mosquito");
texture.Height, 11, 8f);
        _animation.Paused = false;
    }

    internal void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        switch(_state)
        {
            case State.Alive:
                _position += _direction * _speed * dt;
                if(BoundingBox.Left < _gameBoundingBox.Left || BoundingBox.Right > _gameBoundingBox.Right)
                {
                    _direction.X *= -1;
                }
                break;
            case State.Dying:
                _animation.Paused = true;
                _state = State.Dead;
                break;
            case State.Dead:
                break;
        }
        
        _position += _direction * _speed * dt;

        if(BoundingBox.Left < _gameBoundingBox.Left || BoundingBox.Right > _gameBoundingBox.Right)
        {
            _direction.X *= -1;
        }

        _animation.Update(gameTime);
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
                switch(_state)
        {
            case State.Alive:
                _position += _direction * _speed * dt;
                if(BoundingBox.Left < _gameBoundingBox.Left || BoundingBox.Right > _gameBoundingBox.Right)
                {
                    _direction.X *= -1;
                }
                break;
            case State.Dying:
                _animation.Paused = true;
                _state = State.Dead;
                break;
            case State.Dead:
                break;
        }
    }

}

_animationPoofing = new SimpleAnimation(texture, 11, 8f);
new SimpleAnimatin(Texture, Texture.Width / 8, Texture.Height, )


INTERNAL void Poof()
    {
        _state = State.Dying;
    }