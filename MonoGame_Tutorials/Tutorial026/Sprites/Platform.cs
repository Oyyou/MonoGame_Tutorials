using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tutorial026.Interfaces;

namespace Tutorial026.Sprites
{
  public class Platform : Sprite, IMoveable
  {
    public Vector2 Velocity { get; set; }

    public Platform(Texture2D texture) : base(texture)
    {
    }

    public override void Update(GameTime gameTime)
    {
      Position += Velocity;

      if (Rectangle.Right < 0)
        IsRemoved = true;
    }
  }
}
