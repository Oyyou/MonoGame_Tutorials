using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tutorial029.Sprites
{
  public class Player : Sprite
  {
    public Vector2 Velocity;

    public Player(Texture2D texture)
      : base(texture)
    {
    }

    public override void Update(GameTime gameTime)
    {
      Velocity.X = 3f;
    }
  }
}
