using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tutorial020Test.Sprites
{
  public class Enemy : Sprite, ICollidable
  {
    private float _timer;

    public int Health { get; set; }

    public EventHandler Shoot;

    public float ShootingTimer = 1.5f;

    public int Speed = 2;

    public Enemy(Texture2D texture)
      : base(texture)
    {

    }

    public override void Update(GameTime gameTime)
    {
      _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

      if (_timer >= ShootingTimer)
      {
        Shoot(this, new EventArgs());
        _timer = 0;
      }

      Position += new Vector2(-Speed, 0);
    }

    public void OnCollide(Sprite sprite)
    {
      throw new NotImplementedException();
    }
  }
}
