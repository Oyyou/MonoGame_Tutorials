using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tutorial030.Sprites
{
  public class Particle : Sprite
  {
    public Vector2 Velocity;

    public Particle(Texture2D texture) 
      : base(texture)
    {
      Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
    }

    public override void Update(GameTime gameTime)
    {
      Position += Velocity;

      if (Position.Y > (Game1.ScreenHeight + _texture.Height))
        IsRemoved = true;
    }
  }
}
