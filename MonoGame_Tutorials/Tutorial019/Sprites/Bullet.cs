using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial019.Sprites
{
  public class Bullet : Sprite
  {
    private float _timer;

    public Bullet(Texture2D texture)
      : base(texture)
    {

    }

    public override void Update(GameTime gameTime)
    {
      _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

      if (_timer >= LifeSpan)
        IsRemoved = true;

      Position += Direction * LinearVelocity;
    }

    public override void OnCollide(Sprite sprite)
    {
      if (sprite == this.Parent)
        return;

      // Bullets don't collide with eachother
      if (sprite is Bullet)
        return;

      IsRemoved = true;
    }
  }
}
