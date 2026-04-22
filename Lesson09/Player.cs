using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Lesson09;

public class Player
{
    private const int WindowWidth = 550;
    private const int WindowHeight = 400;
    private Rectangle _gameBoundingBox = new Rectangle(0, 0, WindowWidth, WindowHeight);

    private Player _player;
    private const int _Speed = 150, _JumpVelocity = -130;
    private enum State
    {
        Idle,
        Walking,
        Jumping
    }
    private State _state;
    private bool _facingRight = true;

    private SimpleAnimation _animationIdle, _animationJump, _animationWalk, _animationCurrent;

    private Vector2 _position, _velocity, _dimensions;

    private Rectangle _gameBoundingBox;

    internal Vector2 Velocity { get => _velocity; }

    internal Rectangle BoundingBox
    {
        get {return new Rectangle((int)_position.X, (int)_position.Y, (int)_dimensions.X, (int)_dimensions.Y);}
    }

    public Player(Vector2 position, Rectangle gameBoundingBox)
    {
        _position = position;
        _gameBoundingBox = gameBoundingBox;
        _dimensions = new Vector2(35, 34);
    }

    internal void Initialize()
    {
        _state = State.Idle;
        _graphics.PreferredBackBufferWidth = WindowWidth;
        _graphics.PreferredBackBufferHeight = WindowHeight;
        _graphics.ApplyChanges();

        _player = new Player(new Vector2(50, 50), _gameBoundingBox);
        base.initialize();
    }
    internal void LoadContent(ContentManager content)
    {
        // Idle: cells 30 px wide, 1/8 s per frame => 8 fps
        Texture2D idleTexture = content.Load<Texture2D>("Idle");
        int idleFrameWidth = 30;
        int idleFrameHeight = idleTexture.Height;
        int idleFrameCount = idleTexture.Width / idleFrameWidth;
        _animationIdle = new SimpleAnimation(idleTexture, idleFrameWidth, idleFrameHeight, idleFrameCount, 8f);

        // Walk: cells 35 px wide, 1/8 s per frame => 8 fps
        Texture2D walkTexture = content.Load<Texture2D>("Walk");
        int walkFrameWidth = 35;
        int walkFrameHeight = walkTexture.Height;
        int walkFrameCount = walkTexture.Width / walkFrameWidth;
        _animationWalk = new SimpleAnimation(walkTexture, walkFrameWidth, walkFrameHeight, walkFrameCount, 8f);

        // Jump: cells 30 px wide, 1/8 s per frame => 8 fps
        Texture2D jumpTexture = content.Load<Texture2D>("JumpOne");
        int jumpFrameWidth = 30;
        int jumpFrameHeight = jumpTexture.Height;
        int jumpFrameCount = jumpTexture.Width / jumpFrameWidth;
        _animationJump = new SimpleAnimation(jumpTexture, jumpFrameWidth, jumpFrameHeight, jumpFrameCount, 8f);

        // After loading, make sure Initialize will have something to use
        _animationCurrent = _animationIdle;
    }

    internal void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _animationCurrent?.Update(gameTime);

        _velocity.Y += PlatformerGame._Gravity * dt;

        _position += _velocity * dt;
         
         if(Math.Abs(_velocity.Y) > Platformer._Gravity * dt)
         {
            _state = State.Jumping;
            _animationCurrent = _animationJump;
            _animationCurrent.Reset();
         }

        switch(_state)
        {
            case State.Jumping:
                break;
            case State.Walking:
                break;
            case State.Idle:
                break;
        }
        
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        switch(_state)
        {
            case State.Jumping:
                _animationCurrent = _animationJump;
                break;
            case State.Walking:
                SpriteEffects effect = _facingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally; 
                _animationCurrent?.Draw(spriteBatch, _position, effect);
                break;
            case State.Idle:
                _animationCurrent = _animationIdle;
                break;
        }
    }

    internal void MoveHorizontally(float direction)
    {
        bool originalFacingRight = _facingRight;

        _velocity.X = direction * _Speed;

        if (_velocity.X != 0)
            _facingRight = _velocity.X > 0;

        if (_state == State.Idle)
        {
            _animationCurrent = _animationWalk;
            _state = State.Walking;
        }

        if (originalFacingRight != _facingRight)
        {
            _animationCurrent?.Reset();
        }
    }
    
    internal void MoveVertically(float direction)
    {
        _velocity.Y = direction * _Speed;
    }

    internal void Stop()
    {
        _velocity.X = 0;
        if(_state != State.Idle)
        {
            _animationCurrent = _animationIdle;
            _state = State.Idle;
            _animationCurrent.Reset();
        }
    }

    internal void Jump()
    {
        if (_state != State.Jumping)
        {
            _velocity.Y = -350;
            _animationCurrent = _animationJump;
            _state = State.Jumping;
        }
    }

    internal void Land()
    {
        if (_state == State.Jumping)
        {
            _position.Y = whatIlandedOn.Top - _dimensions.Y + 1;
            _velocity.Y = 0;
            _state = State.Walking;
        }
    }

    internal void StandOn(Rectangle whatiAmStandingOn, float dt)
    {
        _velocity.Y -= PlatformerGame.Gravity * dt;
        _position.Y = whatiAmStandingOn.Top - _dimensions.Y + 1;

    }

    internal void Jump()
    {
        if (_state != State.Jumping)
        {
            _velocity.Y = _JumpVelocity;
        }
    }
}