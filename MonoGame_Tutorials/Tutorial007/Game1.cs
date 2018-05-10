using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Tutorial007.Models;
using Tutorial007.Sprites;

namespace Tutorial007
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    public static Random Random;

    public static int ScreenWidth = 1280;
    public static int ScreenHeight = 720;

    private List<Sprite> _sprites;

    private float _timer;

    private bool _hasStarted = false;

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";

      Random = new Random();

      ScreenWidth = graphics.PreferredBackBufferWidth;
      ScreenHeight = graphics.PreferredBackBufferHeight;
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
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

      Restart();
    }

    private void Restart()
    {
      var playerTexture = Content.Load<Texture2D>("Player");

      _sprites = new List<Sprite>()
      {
        new Player(playerTexture)
        {
          Position = new Vector2((ScreenWidth / 2) - (playerTexture.Width / 2), ScreenHeight - playerTexture.Height),
          Input = new Input()
          {
            Left = Keys.A,
            Right = Keys.D,
          },
          Speed = 10f,
        }
      };

      _hasStarted = false;
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
      if (Keyboard.GetState().IsKeyDown(Keys.Space))
        _hasStarted = true;

      if (!_hasStarted)
        return;

      _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

      foreach (var sprite in _sprites)
        sprite.Update(gameTime, _sprites);

      if (_timer > 0.25f)
      {
        _timer = 0f;
        _sprites.Add(new Bomb(Content.Load<Texture2D>("Bomb")));
      }

      for (int i = 0; i < _sprites.Count; i++)
      {
        var sprite = _sprites[i];

        if (sprite.IsRemoved)
        {
          _sprites.RemoveAt(i);
          i--;
        }

        if (sprite is Player)
        {
          var player = sprite as Player;

          if (player.HasDied)
          {
            Restart();
          }
        }
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

      foreach (var sprite in _sprites)
        sprite.Draw(spriteBatch);

      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
