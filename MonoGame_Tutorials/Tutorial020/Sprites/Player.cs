using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tutorial020.Models;
using Microsoft.Xna.Framework.Input;

namespace Tutorial020.Sprites
{
  public class Player : Ship
  {
    private KeyboardState _currentKey;

    private KeyboardState _previousKey;

    private float _shootTimer = 0;

    public bool IsDead
    {
      get
      {
        return Health <= 0;
      }
    }

    public Input Input { get; set; }

    public Score Score { get; set; }

    public Player(Texture2D texture)
      : base(texture)
    {
      Speed = 3f;
    }

    public override void Update(GameTime gameTime)
    {
      if (IsDead)
        return;

      _previousKey = _currentKey;
      _currentKey = Keyboard.GetState();

      var velocity = Vector2.Zero;
      _rotation = 0;

      if (_currentKey.IsKeyDown(Input.Up))
      {
        velocity.Y = -Speed;
        _rotation = MathHelper.ToRadians(-15);
      }
      else if (_currentKey.IsKeyDown(Input.Down))
      {
        velocity.Y += Speed;
        _rotation = MathHelper.ToRadians(15);
      }

      if (_currentKey.IsKeyDown(Input.Left))
      {
        velocity.X -= Speed;
      }
      else if (_currentKey.IsKeyDown(Input.Right))
      {
        velocity.X += Speed;
      }

      _shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

      if (_currentKey.IsKeyDown(Input.Shoot) && _shootTimer > 0.25f)
      {
        Shoot(Speed * 2);
        _shootTimer = 0f;
      }

      Position += velocity;

      Position = Vector2.Clamp(Position, new Vector2(80, 0), new Vector2(Game1.ScreenWidth / 4, Game1.ScreenHeight));
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      if (IsDead)
        return;

      base.Draw(gameTime, spriteBatch);
    }

    public override void OnCollide(Sprite sprite)
    {
      if (IsDead)
        return;

      if (sprite is Bullet && ((Bullet)sprite).Parent is Enemy)
        Health--;

      if (sprite is Enemy)
        Health -= 3;
    }
  }
}
