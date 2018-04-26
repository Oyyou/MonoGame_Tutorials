using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial003
{
  public class Sprite
  {
    private Texture2D _texture;

    public Vector2 Position;

    public float Speed = 2f;

    public Sprite(Texture2D texture)
    {
      _texture = texture;
    }

    public void Update()
    {
      if (Keyboard.GetState().IsKeyDown(Keys.W))
      {
        Position.Y -= Speed;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.S))
      {
        Position.Y += Speed;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.A))
      {
        Position.X -= Speed;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.D))
      {
        Position.X += Speed;
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(_texture, Position, Color.White);
    }
  }
}
