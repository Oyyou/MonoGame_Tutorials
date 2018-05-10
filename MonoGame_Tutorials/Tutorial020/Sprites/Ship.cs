using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tutorial020.Sprites
{
  public class Ship : Sprite, ICollidable
  {
    public int Health { get; set; }

    public Bullet Bullet { get; set; }

    public float Speed;

    public Ship(Texture2D texture) : base(texture)
    {
    }

    protected void Shoot(float speed)
    {
      var bullet = Bullet.Clone() as Bullet;
      bullet.Position = this.Position;
      bullet.Colour = this.Colour;
      bullet.Layer = 0.1f;
      bullet.LifeSpan = 5f;
      bullet.Velocity = new Vector2(speed, 0f);
      bullet.Parent = this;

      Children.Add(bullet);
    }

    public virtual void OnCollide(Sprite sprite)
    {
      throw new NotImplementedException();
    }
  }
}
