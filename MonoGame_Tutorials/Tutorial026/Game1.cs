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

    private ObservableCollection<Component> _components;

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

      _components = new ObservableCollection<Component>();

      _components.CollectionChanged += _components_CollectionChanged;

      _components.Add(_player);

      for (int i = 0; i < 100; i++)
      {
        var powerUp = new PowerUp(Content.Load<Texture2D>("Collectables/PowerUp"))
        {
          Position = new Vector2(200 * i, 300),
        };

        _components.Add(powerUp);
      }
    }

    private void _components_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      _worldObjects = _components.Where(c => c is IMoveable).Cast<IMoveable>();
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
      foreach (var component in _components)
        component.Update(gameTime);

      for (int i = 0; i < _components.Count; i++)
      {
        if (_components[i] is Sprite)
        {
          if (((Sprite)_components[i]).IsRemoved)
          {
            _components.RemoveAt(i);
            i--;
          }
        }
      }

      foreach (var worldObject in _worldObjects)
      {
        worldObject.Velocity = new Vector2(-_player.TotalAttributes.Speed, worldObject.Velocity.Y);
      }

      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      spriteBatch.Begin();

      foreach (var component in _components)
        component.Draw(gameTime, spriteBatch);

      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
