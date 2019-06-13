using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tutorial021.Sprites
{
  public class Player : Sprite
  {
    private bool _onGround;

    private bool _jumping;

    public Player(Texture2D texture)
      : base(texture)
    {
    }

    public override void Update(GameTime gameTime)
    {
      if (Keyboard.GetState().IsKeyDown(Keys.A))
        _velocity.X = -2f;
      else if (Keyboard.GetState().IsKeyDown(Keys.D))
        _velocity.X = 2f;
      else _velocity.X = 0f;

      if (Keyboard.GetState().IsKeyDown(Keys.Space))
        _jumping = true;
    }

    public override void OnCollide(Sprite sprite)
    {
      var test = sprite.Centre - (this.Centre);

      var onTop = this.WillIntersectTop(sprite);
      var onLeft = this.WillIntersectLeft(sprite);
      var onRight = this.WillIntersectRight(sprite);
      var onBotton = this.WillIntersectBottom(sprite);

      if (onTop)
      {
        _onGround = true;
        _velocity.Y = sprite.Rectangle.Top - this.Rectangle.Bottom;
        //this.Position = new Vector2(this.Position.X, sprite.Rectangle.Top - this.Origin.Y);
        //this._velocity.Y = 0;
      }
      else if (onLeft && _velocity.X > 0)
      {
        _velocity.X = 0;
      }
      else if (onRight && _velocity.X < 0)
      {
        _velocity.X = 0;
      }
      else if (onBotton)
      {
        _velocity.Y = 1;
      }
    }

    public override void ApplyPhysics(GameTime gameTime)
    {
      if (!_onGround)
        _velocity.Y += 0.2f;

      if (_onGround && _jumping)
      {
        _velocity.Y = -5f;
      }

      _onGround = false;
      _jumping = false;

      Position += _velocity;
    }
  }
}
