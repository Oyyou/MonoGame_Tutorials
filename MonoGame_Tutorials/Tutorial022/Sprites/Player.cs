using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tutorial022.Sprites
{
  public class Player : Sprite
  {
    public Player(Texture2D texture)
      : base(texture)
    {
    }

    public override void Update(GameTime gameTime)
    {
      var velocity = Vector2.Zero;

      if (Keyboard.GetState().IsKeyDown(Keys.W))
        velocity.Y = -LinearVelocity;
      else if (Keyboard.GetState().IsKeyDown(Keys.S))
        velocity.Y = LinearVelocity;

      if (Keyboard.GetState().IsKeyDown(Keys.A))
        velocity.X = -LinearVelocity;
      else if (Keyboard.GetState().IsKeyDown(Keys.D))
        velocity.X = LinearVelocity;

      Position += velocity;
    }
  }
}
