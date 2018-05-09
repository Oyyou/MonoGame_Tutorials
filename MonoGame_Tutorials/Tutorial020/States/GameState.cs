using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tutorial020Test.Sprites;
using Microsoft.Xna.Framework.Input;

namespace Tutorial020Test.States
{
  public class GameState : State
  {
    private List<Sprite> _sprites;
    private List<Sprite> _newSprites;

    public GameState(Game1 game, ContentManager content)
      : base(game, content)
    {
    }

    public override void LoadContent()
    {
      var playerTexture = _content.Load<Texture2D>("Player");
      var enemyTexture = _content.Load<Texture2D>("Enemy_1");

      _sprites = new List<Sprite>()
      {
        new Player(playerTexture)
        {
          Colour = Color.Blue,
          Position = new Vector2(100, 50),
          Layer = 0.3f,
          Input = new Models.Input()
          {
            Up = Keys.W,
            Down = Keys.S,
            Shoot = Keys.Space,
          },
          Health = 10,
          Shoot = new System.EventHandler(Shoot),
        },
        new Player(playerTexture)
        {
          Colour = Color.Green,
          Position = new Vector2(125, 50),
          Layer = 0.4f,
          Input = new Models.Input()
          {
            Up = Keys.Up,
            Down = Keys.Down,
            Shoot = Keys.Enter,
          },
          Health = 10,
          Shoot = new System.EventHandler(Shoot),
        },
        new Enemy(enemyTexture)
        {
          Colour = Color.Red,
          Position = new Vector2(Game1.ScreenWidth, 300),
          Layer = 0.2f,
          Health = 1,
          Shoot = new System.EventHandler(Shoot),
        }
      };

      _newSprites = new List<Sprite>();
    }

    private void Shoot(object sender, EventArgs e)
    {
      var sprite = sender as Sprite;
      float speed = 0f;

      if (sprite is Player)
        speed = 5f;
      else if (sprite is Enemy)
        speed = -5f;

      _newSprites.Add(new Bullet(_content.Load<Texture2D>("Bullet"))
      {
        Position = sprite.Position,
        Colour = sprite.Colour,
        Layer = 0.1f,
        LifeSpan = 5f,
        Velocity = new Vector2(speed, 0f),
        Parent = sprite,
      });
    }

    public override void Update(GameTime gameTime)
    {
      if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        _game.ChangeState(new MenuState(_game, _content));

      foreach (var sprite in _sprites)
        sprite.Update(gameTime);
    }

    public override void PostUpdate(GameTime gameTime)
    {
      foreach (var leftSprite in _sprites.Where(c => c is ICollidable))
      {
        foreach (var rightSprite in _sprites.Where(c => c is ICollidable))
        {
          // Don't do anything if they're the same sprite!
          if (leftSprite == rightSprite)
            continue;

          // Don't do anything if they're not colliding
          if (!leftSprite.Rectangle.Intersects(rightSprite.Rectangle))
            continue;

          if (leftSprite is Bullet)
          {
            if (leftSprite.Parent == rightSprite)
              continue;

            if (leftSprite.Parent is Player && rightSprite is Enemy)
            {
              leftSprite.IsRemoved = true;

              var enemy = rightSprite as Enemy;

              enemy.Health--;

              if (enemy.Health <= 0)
                enemy.IsRemoved = true;
            }

            if (leftSprite.Parent is Enemy && rightSprite is Player)
            {
              leftSprite.IsRemoved = true;

              var player = rightSprite as Player;

              player.Health--;

              if (player.Health <= 0)
                player.IsRemoved = true;
            }
          }

          if (leftSprite is Player && rightSprite is Enemy)
          {
            var player = leftSprite as Player;
            var enemy = rightSprite as Enemy;

            player.Health--;

            if (player.Health <= 0)
              player.IsRemoved = true;

            enemy.Health--;

            if (enemy.Health <= 0)
              enemy.IsRemoved = true;
          }

          if (leftSprite is Enemy && rightSprite is Player)
          {
            var player = rightSprite as Player;
            var enemy = leftSprite as Enemy;

            player.Health--;

            if (player.Health <= 0)
              player.IsRemoved = true;

            enemy.Health--;

            if (enemy.Health <= 0)
              enemy.IsRemoved = true;
          }
        }
      }

      for (int i = 0; i < _sprites.Count; i++)
      {
        if (_sprites[i].IsRemoved)
        {
          _sprites.RemoveAt(i);
          i--;
        }
      }

      for (int i = 0; i < _newSprites.Count; i++)
      {
        _sprites.Add(_newSprites[i]);
      }

      _newSprites = new List<Sprite>();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      spriteBatch.Begin(SpriteSortMode.FrontToBack);

      foreach (var sprite in _sprites)
        sprite.Draw(gameTime, spriteBatch);

      spriteBatch.End();
    }
  }
}
