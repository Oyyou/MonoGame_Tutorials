using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tutorial020.Sprites;
using Microsoft.Xna.Framework.Input;
using Tutorial020.Managers;

namespace Tutorial020.States
{
  public class GameState : State
  {
    private EnemyManager _enemyManager;

    private List<Sprite> _sprites;

    public int PlayerCount;

    public GameState(Game1 game, ContentManager content)
      : base(game, content)
    {
    }

    public override void LoadContent()
    {
      var playerTexture = _content.Load<Texture2D>("Player");
      var bulletTexture = _content.Load<Texture2D>("Bullet");
      var enemyTexture = _content.Load<Texture2D>("Enemy_1");

      _sprites = new List<Sprite>();

      if (PlayerCount >= 1)
      {
        _sprites.Add(new Player(playerTexture)
        {
          Colour = Color.Blue,
          Position = new Vector2(100, 50),
          Layer = 0.3f,
          Bullet = new Bullet(bulletTexture),
          Input = new Models.Input()
          {
            Up = Keys.W,
            Down = Keys.S,
            Shoot = Keys.Space,
          },
          Health = 10,
        });
      }

      if (PlayerCount >= 2)
      {
        _sprites.Add(new Player(playerTexture)
        {
          Colour = Color.Green,
          Position = new Vector2(125, 200),
          Layer = 0.4f,
          Bullet = new Bullet(bulletTexture),
          Input = new Models.Input()
          {
            Up = Keys.Up,
            Down = Keys.Down,
            Shoot = Keys.Enter,
          },
          Health = 10,
        });
      }

      _enemyManager = new EnemyManager(_content);
    }

    public override void Update(GameTime gameTime)
    {
      if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        _game.ChangeState(new MenuState(_game, _content));

      foreach (var sprite in _sprites)
        sprite.Update(gameTime);

      _enemyManager.Update(gameTime);
      if (_enemyManager.CanAdd && _sprites.Where(c => c is Enemy).Count() < _enemyManager.MaxEnemies)
      {
        _sprites.Add(_enemyManager.GetEnemy());
      }
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

      // Add the children sprites to the list of sprites
      int spriteCount = _sprites.Count;
      for (int i = 0; i < spriteCount; i++)
      {
        var sprite = _sprites[i];
        foreach (var child in sprite.Children)
        {
          _sprites.Add(child);
        }
        sprite.Children = new List<Sprite>();
      }
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
