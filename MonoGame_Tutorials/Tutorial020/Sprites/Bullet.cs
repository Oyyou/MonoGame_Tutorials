using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tutorial020.Sprites
{
  public class Bullet : Sprite, ICollidable
  {
    private float _timer;

    public float LifeSpan { get; set; }

    public Vector2 Velocity { get; set; }

    public Bullet(Texture2D texture)
      : base(texture)
    {

    }

    public override void Update(GameTime gameTime)
    {
      _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

      if (_timer >= LifeSpan)
        IsRemoved = true;

      Position += Velocity;
    }

    public void OnCollide(Sprite sprite)
    {
      if (this.Parent is Player && sprite is Enemy)
      {
        this.IsRemoved = true;

        var enemy = sprite as Enemy;

        enemy.Health--;

        if (enemy.Health <= 0)
          enemy.IsRemoved = true;
      }

      if (this.Parent is Enemy && sprite is Player)
      {
        this.IsRemoved = true;

        var player = sprite as Player;

        player.Health--;

        if (player.Health <= 0)
          player.IsRemoved = true;
      }
    }
  }
}
