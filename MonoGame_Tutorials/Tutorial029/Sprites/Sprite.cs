using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial029.Sprites
{
  public class Sprite : Component
  {
    protected float _layer { get; set; }

    protected Texture2D _texture;

    public float Layer
    {
      get { return _layer; }
      set
      {
        _layer = value;
      }
    }

    public Vector2 Position;

    public Rectangle Rectangle
    {
      get
      {
        return new Rectangle((int)Position.X, (int)Position.Y, (int)(_texture.Width * Scale), (int)(_texture.Height * Scale));
      }
    }

    public float Scale { get; set; } = 1f;

    public Sprite(Texture2D texture)
    {
      _texture = texture;
    }

    public override void Update(GameTime gameTime)
    {

    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(_texture, Position, null, Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, Layer);
    }
  }
}
