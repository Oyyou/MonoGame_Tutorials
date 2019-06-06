using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tutorial030.Interfaces;
using Tutorial030.Models;
using Tutorial030.Sprites;

namespace Tutorial030.States
{
  public class PlayingState : State
  {
    private LevelModel _level;

    private ObservableCollection<Component> _components;

    private IEnumerable<IMoveable> _worldObjects;

    private IEnumerable<Sprite> _sprites;

    private List<string> _map
    {
      get
      {
        return new List<string>()
        {
          "00000000000000000000000000000000000000",
          "00000000000000000000000000000000000000",
          "00000000000000000000000000000000000000",
          "00000000000000000000000000000000000000",
          "00000000000000000000000000000000000000",
          "00000000000000000000000000000000000000",
          "00000000000000000000000000000000000000",
          "00000000000000000000000000000000000000",
          "00000000000000000000000000000000000000",
          "00000000000000000000000000000000000000",
          "00000000000000000000000000000000000000",
          "00000000000000000000000000000000000000",
          "00000000000000000000000000000000000000",
          "00000000000000000000000000000000000000",
          "00000000000000000000000110000000000000",
          "00000000000000000000011000000000000000",
          "00000000000200000001100000000000000000",
          "11111111111111111110000000000111111111"
        };
      }
    }

    public PlayingState(GameModel gameModel, LevelModel level)
      : base(gameModel)
    {
      _level = level;
    }

    public override void LoadContent()
    {
      _components = new ObservableCollection<Component>();
      _components.CollectionChanged += UpdateWorldObjects;

      _components.Add(_level.Player);

      foreach (var sb in _level.ScrollingBackgrounds)
        _components.Add(sb);

      int y = 0;
      foreach (var line in _map)
      {
        int x = 0;
        foreach (var character in line)
        {
          var texture = _content.Load<Texture2D>("Block");

          var platform = new Platform(texture)
          {
            Position = new Vector2(x * texture.Width, y * texture.Height),
            Layer = 0.999f,
          };

          x++;

          if (character == '1')
          {
            platform.PlatformType = PlatformTypes.Safe;
          }
          else if (character == '2')
          {
            platform.PlatformType = PlatformTypes.Dangerous;
            platform.Colour = Color.Red;
          }
          else
          {
            continue;
          }

          _components.Add(platform);
        }
        y++;
      }
    }

    private void UpdateWorldObjects(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      _worldObjects = _components.Where(c => c is IMoveable).Cast<IMoveable>();
      _sprites = _components.Where(c => c is Sprite).Cast<Sprite>();
    }

    public override void Update(GameTime gameTime)
    {
      foreach (var component in _components)
        component.Update(gameTime);

      _level.Emitter?.Update(gameTime);

      foreach (var worldObject in _worldObjects)
      {
        worldObject.Velocity = new Vector2(-_level.Player.Velocity.X, worldObject.Velocity.Y);
      }

      PostUpdate(gameTime);
    }

    public void PostUpdate(GameTime gameTime)
    {
      foreach (var spriteA in _sprites)
      {
        // Don't do anything if they're the same sprite!
        if (spriteA == _level.Player)
          continue;

        if (_level.Player.IsTouching(spriteA))
        {
          if (spriteA is Platform)
          {
            var platform = (Platform)spriteA;

            if (platform.PlatformType == PlatformTypes.Dangerous)
            {
              LoadContent();
              break;
            }
          }


          _level.Player.OnCollide(spriteA);
        }
      }

      _level.Player.ApplyVelocity(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      _spriteBatch.Begin(SpriteSortMode.FrontToBack);

      foreach (var component in _components)
        component.Draw(gameTime, _spriteBatch);

      _spriteBatch.End();

      DrawEmitter(gameTime);
    }

    private void DrawEmitter(GameTime gameTime)
    {
      if (_level.Emitter == null)
        return;

      _spriteBatch.Begin();

      _level.Emitter.Draw(gameTime, _spriteBatch);

      _spriteBatch.End();
    }
  }
}
