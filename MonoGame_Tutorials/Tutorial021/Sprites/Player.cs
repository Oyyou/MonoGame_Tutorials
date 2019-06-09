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
      _velocity.Y += 0.2f;

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
      var test = sprite.Centre - (this.Centre);// + new Vector2(10, 25));

      var rotation = (float)Math.Atan2(test.Y, test.X);

      var rotation2 = Math.Abs(MathHelper.ToDegrees(rotation));

      bool onLeft = false;
      bool onRight = false;
      bool onTop = false;
      bool onBotton = false;

      int index = 0;

      for (int i = -45; i <= 315; i += 90)
      {
        if (rotation2 >= i && rotation2 < i + 90)
        {
          switch (index)
          {
            case 0:
              onLeft = true;
              break;

            case 1:
              onTop = true;
              break;

            case 2:
              onRight = true;
              break;

            case 3:
              onBotton = true;
              break;

            default:
              break;
          }
        }
        index++;
      }

      if (onTop)
      {
        _onGround = true;
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

      }
    }

    public override void ApplyPhysics(GameTime gameTime)
    {
      if (_onGround)
        _velocity.Y = 0f;

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
