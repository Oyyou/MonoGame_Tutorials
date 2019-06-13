using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using Tutorial026.Interfaces;
using Tutorial026.Sprites;

namespace Tutorial026
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    private Player _player;

    private ObservableCollection<Sprite> _sprites;

    private IEnumerable<IMoveable> _worldObjects;

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
      // TODO: Add your initialization logic here

      base.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);

      _player = new Player(Content.Load<Texture2D>("Player/boy"))
      {
        Position = new Vector2(50, 300),
        BaseAttributes = new Models.Attributes()
        {
          Speed = 3f,
        },
      };

      _sprites = new ObservableCollection<Sprite>();

      _sprites.CollectionChanged += _components_CollectionChanged;

      _sprites.Add(_player);

      for (int i = 0; i < 100; i++)
      {
        var powerUp = new PowerUp(Content.Load<Texture2D>("Collectables/snowcog"), new Models.Attributes() { Speed = 1, })
        {
          Position = new Vector2(200 * i, 300),
        };

        _sprites.Add(powerUp);
      }
    }

    private void _components_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      _worldObjects = _sprites.Where(c => c is IMoveable).Cast<IMoveable>();
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// game-specific content.
    /// </summary>
    protected override void UnloadContent()
    {
      // TODO: Unload any non ContentManager content here
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
      foreach (var component in _sprites)
        component.Update(gameTime);

      CheckCollision();

      ApplyPhysics();

      RemoveComponents();

      base.Update(gameTime);
    }

    private void CheckCollision()
    {
      foreach (var sprite in _sprites)
      {
        if (sprite == _player)
          continue;

        if (_player.Rectangle.Intersects(sprite.Rectangle))
        {
          _player.OnCollide(sprite);
        }
      }
    }

    private void ApplyPhysics()
    {
      foreach (var worldObject in _worldObjects)
      {
        worldObject.Velocity = new Vector2(-_player.TotalAttributes.Speed, worldObject.Velocity.Y);
      }
    }

    private void RemoveComponents()
    {
      for (int i = 0; i < _sprites.Count; i++)
      {
        if ((_sprites[i]).IsRemoved)
        {
          _sprites.RemoveAt(i);
          i--;
        }
      }
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      spriteBatch.Begin();

      foreach (var sprite in _sprites)
        sprite.Draw(gameTime, spriteBatch);

      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
