using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial030.Sprites;

namespace Tutorial030.Emitters
{
  public class SnowEmitter : Emitter
  {
    public SnowEmitter(Particle particle)
      : base(particle)
    {

    }

    protected override void ApplyGlobalVelocity()
    {
      var xSway = (float)Game1.Random.Next(-2, 2);
      foreach (var particle in _particles)
        particle.Velocity.X = (xSway * particle.Scale) / 50;
    }

    protected override Particle GenerateParticle()
    {
      var particle = _particlePrefab.Clone() as Particle;

      var xPosition = Game1.Random.Next(0, Game1.ScreenWidth);
      var ySpeed = Game1.Random.Next(10, 100) / 100f;

      particle.Position = new Vector2(xPosition, -particle.Rectangle.Height);
      particle.Opacity = (float)Game1.Random.NextDouble();
      particle.Rotation = MathHelper.ToRadians(Game1.Random.Next(0, 360));
      particle.Scale = (float)Game1.Random.NextDouble() + Game1.Random.Next(0, 3);
      particle.Velocity = new Vector2(0, ySpeed);

      return particle;
    }
  }
}
