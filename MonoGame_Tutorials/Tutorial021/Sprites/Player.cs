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

    public Player(Texture2D texture)
      : base(texture)
    {
    }

    public override void Update(GameTime gameTime)
    {
      _velocity.Y += 0.2f;

      if (_onGround)
        _velocity.Y = 0f;

      if (Keyboard.GetState().IsKeyDown(Keys.A))
        _velocity.X = -2f;
      else if (Keyboard.GetState().IsKeyDown(Keys.D))
        _velocity.X = 2f;
      else _velocity.X = 0f;

      if (Keyboard.GetState().IsKeyDown(Keys.Space) && _onGround)
      {
        _velocity.Y = -5f;
      }

      _onGround = false;
      Position += _velocity;
    }

    public override void OnCollide(Sprite sprite)
    {
      if (_velocity.X > 0)
      {
        if (this.Rectangle.Bottom > sprite.Rectangle.Top &&
            this.Rectangle.Top < sprite.Rectangle.Bottom &&
            this.Rectangle.Left < sprite.Rectangle.Left &&
            this.Rectangle.Right >= sprite.Rectangle.Left)
        {
          this.Position = new Vector2(sprite.Position.X - this.Rectangle.Width, this.Position.Y);
        }
      }

      if (_velocity.X < 0)
      {
        if (this.Rectangle.Bottom > sprite.Rectangle.Top &&
            this.Rectangle.Top < sprite.Rectangle.Bottom &&
            this.Rectangle.Right > sprite.Rectangle.Right &&
            this.Rectangle.Left <= sprite.Rectangle.Right)
        {
          this.Position = new Vector2(sprite.Position.X + sprite.Rectangle.Width, this.Position.Y);
        }
      }

      if (_velocity.Y > 0)
      {
        if (this.Rectangle.Right > sprite.Rectangle.Left &&
           this.Rectangle.Left < sprite.Rectangle.Right &&
           this.Rectangle.Top < sprite.Rectangle.Top &&
           this.Rectangle.Bottom >= sprite.Rectangle.Top)
        {
          _onGround = true;
          this.Position = new Vector2(this.Position.X, sprite.Position.Y - this.Rectangle.Height);
        }
      }

      if (_velocity.Y < 0)
      {
        if (this.Rectangle.Right > sprite.Rectangle.Left &&
           this.Rectangle.Left < sprite.Rectangle.Right &&
           this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
           this.Rectangle.Top <= sprite.Rectangle.Bottom)
        {
          this.Position = new Vector2(this.Position.X, sprite.Position.Y + sprite.Rectangle.Height);
        }
      }
    }
  }
}
