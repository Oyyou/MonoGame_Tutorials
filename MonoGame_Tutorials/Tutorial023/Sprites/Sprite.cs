using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial023.Sprites
{
  public class Sprite : Component
  {
    protected float _layer { get; set; }

    protected Texture2D _texture;

    public Color Colour { get; set; }

    public bool IsRemoved { get; set; }

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
        return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
      }
    }

    public Sprite(Texture2D texture)
    {
      _texture = texture;
      
      Colour = Color.White;
    }

    public override void Update(GameTime gameTime)
    {

    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(_texture, Position, null, Colour, 0, new Vector2(0, 0), 1f, SpriteEffects.None, Layer);
    }
  }
}
