using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tutorial024.Sprites;

namespace Tutorial024.Emitters
{
  public class SnowEmitter : Emitter
  {
    public SnowEmitter(Sprite particle)
      : base(particle)
    {

    }

    protected override void ApplyGlobalVelocity()
    {
      var xSway = (float)Game1.Random.Next(-2, 2);
      foreach (var particle in _particles)
        particle.Velocity.X = (xSway * particle.Scale) / 50;
    }

    protected override Sprite GenerateParticle()
    {
      var sprite = _particlePrefab.Clone() as Sprite;

      var xPosition = Game1.Random.Next(0, Game1.ScreenWidth);
      var ySpeed = Game1.Random.Next(10, 100) / 100f;

      sprite.Position = new Vector2(xPosition, -sprite.Rectangle.Height);
      sprite.Opacity = (float)Game1.Random.NextDouble();
      sprite.Rotation = MathHelper.ToRadians(Game1.Random.Next(0, 360));
      sprite.Scale = (float)Game1.Random.NextDouble() + Game1.Random.Next(0, 3);
      sprite.Velocity = new Vector2(0, ySpeed);

      return sprite;
    }
  }
}
