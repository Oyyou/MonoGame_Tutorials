using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial019.Sprites
{
  public class Ship : Sprite
  {
    public Bullet Bullet;

    public Ship(Texture2D texture)
      : base(texture)
    {

    }

    public override void Update(GameTime gameTime)
    {
      _previousKey = _currentKey;
      _currentKey = Keyboard.GetState();

      if (Keyboard.GetState().IsKeyDown(Keys.A))
        _rotation -= MathHelper.ToRadians(RotationVelocity);
      else if (Keyboard.GetState().IsKeyDown(Keys.D))
        _rotation += MathHelper.ToRadians(RotationVelocity);

      Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

      if (Keyboard.GetState().IsKeyDown(Keys.W))
        Position += Direction * LinearVelocity;

      if (_currentKey.IsKeyDown(Keys.Space) &&
          _previousKey.IsKeyUp(Keys.Space))
      {
        Shoot();
      }
    }

    private void Shoot()
    {
      var bullet = Bullet.Clone() as Bullet;
      bullet.Direction = this.Direction;
      bullet.Position = this.Position;
      bullet.Colour = this.Colour;
      bullet.LinearVelocity = this.LinearVelocity * 1.1f;
      bullet.LifeSpan = 3f;
      bullet.Parent = this;

      Children.Add(bullet);
    }
  }
}