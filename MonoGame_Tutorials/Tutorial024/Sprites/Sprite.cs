using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tutorial024.Sprites
{
  public class Sprite : Component, ICloneable
  {
    protected Texture2D _texture;

    public float Opacity { get; set; }

    public Vector2 Origin { get; set; }

    public float Rotation { get; set; }

    public float Scale { get; set; }

    public Vector2 Position;

    public Vector2 Velocity;

    public Rectangle Rectangle
    {
      get
      {
        return new Rectangle((int)(Position.X - Origin.X), (int)(Position.Y - Origin.Y), _texture.Width, _texture.Height);
      }
    }

    public bool IsRemoved { get; set; }

    public Sprite(Texture2D texture)
    {
      _texture = texture;

      Opacity = 1f;

      Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
    }

    public override void Update(GameTime gameTime)
    {
      Position += Velocity;

      if (Position.Y > (Game1.ScreenHeight + _texture.Height))
        IsRemoved = true;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(_texture, Position, null, Color.White * Opacity, Rotation, Origin, Scale, SpriteEffects.None, 0);
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }
  }
}
