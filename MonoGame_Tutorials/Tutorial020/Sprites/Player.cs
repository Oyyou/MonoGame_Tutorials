using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tutorial020Test.Models;
using Microsoft.Xna.Framework.Input;

namespace Tutorial020Test.Sprites
{
  public class Player : Sprite, ICollidable
  {
    private float _speed = 3f;

    private KeyboardState _currentKey;

    private KeyboardState _previousKey;

    public int Health { get; set; }

    public Input Input { get; set; }

    public EventHandler Shoot;

    public Player(Texture2D texture)
      : base(texture)
    {
    }

    public override void Update(GameTime gameTime)
    {
      _previousKey = _currentKey;
      _currentKey = Keyboard.GetState();

      var velocity = Vector2.Zero;
      _rotation = 0;

      if (_currentKey.IsKeyDown(Input.Up))
      {
        velocity.Y = -_speed;
        _rotation = MathHelper.ToRadians(-15);
      }
      else if (_currentKey.IsKeyDown(Input.Down))
      {
        velocity.Y += _speed;
        _rotation = MathHelper.ToRadians(15);
      }

      if (_currentKey.IsKeyDown(Input.Shoot) && _previousKey.IsKeyUp(Input.Shoot))
      {
        Shoot(this, new EventArgs());
      }

      Position += velocity;

      Position = Vector2.Clamp(Position, new Vector2(Position.X, 0), new Vector2(Position.X, Game1.ScreenHeight));
    }

    public void OnCollide(Sprite sprite)
    {
      throw new NotImplementedException();
    }
  }
}
