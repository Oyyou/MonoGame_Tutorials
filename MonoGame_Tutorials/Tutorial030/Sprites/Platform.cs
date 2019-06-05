using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tutorial030.Interfaces;

namespace Tutorial030.Sprites
{
  public enum PlatformTypes
  {
    Dangerous,
    Safe,
  }

  public class Platform : Sprite, IMoveable
  {
    public Vector2 Velocity { get; set; }

    public PlatformTypes PlatformType;

    public Platform(Texture2D texture) 
      : base(texture)
    {
      PlatformType = PlatformTypes.Safe;
    }

    public override void Update(GameTime gameTime)
    {
      Position += Velocity;

      if (Rectangle.Right < 0)
        IsRemoved = true;
    }
  }
}
