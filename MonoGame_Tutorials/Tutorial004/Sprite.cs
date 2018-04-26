using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial004
{
  public class Sprite
  {
    private Texture2D _texture;

    public Vector2 Position;

    public float Speed = 2f;

    public Input Input;

    public Sprite(Texture2D texture)
    {
      _texture = texture;
    }

    public void Update()
    {
      Move();
    }

    public void Move()
    {
      if (Input == null)
        return;

      if (Keyboard.GetState().IsKeyDown(Input.Left))
      {
        Position.X -= Speed;
      }

      if (Keyboard.GetState().IsKeyDown(Input.Right))
      {
        Position.X += Speed;
      }

      if (Keyboard.GetState().IsKeyDown(Input.Up))
      {
        Position.Y -= Speed;
      }

      if (Keyboard.GetState().IsKeyDown(Input.Down))
      {
        Position.Y += Speed;
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(_texture, Position, Color.White);
    }
  }
}
