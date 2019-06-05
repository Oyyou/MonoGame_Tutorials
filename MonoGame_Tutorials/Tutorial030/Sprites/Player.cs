using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tutorial030.Models;

namespace Tutorial030.Sprites
{
  public class Player : Sprite
  {
    private KeyboardState _previousKey;

    private KeyboardState _currentKey;

    private bool _isOnGround = false;

    private bool _jumping = false;

    public Vector2 Velocity;
    /// <summary>
    /// These are the types of attributes to only change on level-up
    /// </summary>
    public Attributes BaseAttributes { get; set; }

    /// <summary>
    /// These are extra attributes that can be gained from different sources (equipment, power-ups, spells etc)
    /// </summary>
    public List<Attributes> AttributeModifiers { get; set; }

    public Attributes TotalAttributes
    {
      get
      {
        return BaseAttributes + AttributeModifiers.Sum();
      }
    }

    public Player(Dictionary<string, Animation> animations) : base(animations)
    {
      BaseAttributes = new Attributes();

      AttributeModifiers = new List<Attributes>();
    }

    public override void Update(GameTime gameTime)
    {
      _previousKey = _currentKey;
      _currentKey = Keyboard.GetState();

      Velocity.X = TotalAttributes.Speed;

      if (Velocity.Y >= 0)
        _jumping = false;

      if (_isOnGround)
      {
        if (_previousKey.IsKeyUp(Keys.Space) && _currentKey.IsKeyDown(Keys.Space))
        {
          Velocity.Y = -10f;
          _jumping = true;
        }
      }
      else
      {
        Velocity.Y += 0.75f;
      }

      SetAnimation();

      _animationManager.Update(gameTime);

      _isOnGround = false;
    }

    public void ApplyVelocity(GameTime gameTime)
    {
      //Position = new Vector2(Position.X, Position.Y + Velocity.Y);
      this.Y += Velocity.Y;
    }

    private void SetAnimation()
    {
      if (Velocity.Y < 0)
      {
        _animationManager.Play(_animations["Jumping"]);
      }
      else if (Velocity.Y > 0)
      {
        _animationManager.Play(_animations["Falling"]);
      }
      else
      {
        _animationManager.Play(_animations["Running"]);
      }
    }

    public override void OnCollide(Sprite sprite)
    {
      if (IsOnTopOf(sprite) && !_jumping)
      {
        this._position.Y = sprite.Rectangle.Top - this.Rectangle.Height;
        Velocity.Y = 0;
        _isOnGround = true;
      }
    }
  }
}
