using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Tutorial008.Models;
using Tutorial008.Sprites;

namespace Tutorial008
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    public static Random Random;

    public static int ScreenWidth;
    public static int ScreenHeight;

    private List<Sprite> _sprites;

    private SpriteFont _font;

    private float _timer;

    private Texture2D _appleTexture;

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

      var playerTexture = Content.Load<Texture2D>("Player");

      _sprites = new List<Sprite>()
      {
        new Player(playerTexture)
        {
          Input = new Input()
          {
            Left = Keys.A,
            Right = Keys.D,
            Up = Keys.W,
            Down = Keys.S,
          },
          Position = new Vector2(100, 100),
          Colour = Color.Blue,
          Speed = 5f,
        },
        new Player(playerTexture)
        {
          Input = new Input()
          {
            Left = Keys.Left,
            Right = Keys.Right,
            Up = Keys.Up,
            Down = Keys.Down,
          },
          Position = new Vector2(ScreenWidth - 100 - playerTexture.Width, 100),
          Colour = Color.Green,
          Speed = 5f,
        },
      };

      _font = Content.Load<SpriteFont>("Font");
      _appleTexture = Content.Load<Texture2D>("Apple");
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
      _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

      foreach (var sprite in _sprites)
        sprite.Update(gameTime, _sprites);

      PostUpdate();

      SpawnApple();

      base.Update(gameTime);
    }

    private void SpawnApple()
    {
      if (_timer > 1)
      {
        _timer = 0;

        var xPos = Random.Next(0, ScreenWidth - _appleTexture.Width);
        var yPos = Random.Next(0, ScreenHeight - _appleTexture.Height);

        _sprites.Add(new Sprite(_appleTexture)
        {
          Position = new Vector2(xPos, yPos),
        });
      }
    }

    private void PostUpdate()
    {
      for (int i = 0; i < _sprites.Count; i++)
      {
        if (_sprites[i].IsRemoved)
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
        sprite.Draw(spriteBatch);

      var fontY = 10;
      var i = 0;

      foreach(var sprite in _sprites)
      {
        if (sprite is Player)
          spriteBatch.DrawString(_font, string.Format("Player {0}: {1}", ++i, ((Player)sprite).Score), new Vector2(10, fontY += 20), Color.Black);
      }

      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
