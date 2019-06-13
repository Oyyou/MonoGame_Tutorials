using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tutorial026.Interfaces;
using Tutorial026.Models;

namespace Tutorial026.Sprites
{
  public class PowerUp : Sprite, IMoveable
  {
    private Vector2 _velocity;

    private bool _goingDown;

    private float _distanceTravelled;

    public float FloatingSpeed = 0.1f;

    public float FloatingDistance = 10f;

    /// <summary>
    /// What is gained if the power-up is collected
    /// </summary>
    public readonly Attributes Attributes;

    public Vector2 Velocity
    {
      get { return _velocity; }
      set
      {
        _velocity = value;
      }
    }

    public PowerUp(Texture2D texture, Attributes attributes)
      : base(texture)
    {
      Attributes = attributes;
    }

    public override void Update(GameTime gameTime)
    {
      if (_goingDown)
        _velocity.Y = FloatingSpeed;
      else _velocity.Y = -FloatingSpeed;

      _distanceTravelled += FloatingSpeed;

      if (_distanceTravelled >= FloatingDistance)
      {
        _distanceTravelled = 0;
        _goingDown = !_goingDown;
      }

      Position += _velocity;

      if (Rectangle.Right < 0)
        IsRemoved = true;
    }
  }
}
