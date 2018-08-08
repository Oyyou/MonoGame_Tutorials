using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Tutorial010.Models;
using Tutorial010.Sprites;

namespace Tutorial010
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    public static int ScreenWidth;
    public static int ScreenHeight;
    public static Random Random;

    private Score _score;
    private List<Sprite> _sprites;

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
      ScreenWidth = graphics.PreferredBackBufferWidth;
      ScreenHeight = graphics.PreferredBackBufferHeight;
      Random = new Random();

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

      var batTexture = Content.Load<Texture2D>("Bat");
      var ballTexture = Content.Load<Texture2D>("Ball");

      _score = new Score(Content.Load<SpriteFont>("Font"));

      _sprites = new List<Sprite>()
      {
        new Sprite(Content.Load<Texture2D>("Background")),
        new Bat(batTexture)
        {
          Position = new Vector2(20, (ScreenHeight / 2) - (batTexture.Height / 2)),
          Input = new Input()
          {
            Up = Keys.W,
            Down = Keys.S,
          }
        },
        new Bat(batTexture)
        {
          Position = new Vector2(ScreenWidth - 20 - batTexture.Width, (ScreenHeight / 2) - (batTexture.Height / 2)),
          Input = new Input()
          {
            Up = Keys.Up,
            Down = Keys.Down,
          }
        },
        new Ball(ballTexture)
        {
          Position = new Vector2((ScreenWidth / 2) - (ballTexture.Width / 2), (ScreenHeight / 2) - (ballTexture.Height / 2)),
          Score = _score,
        }
      };
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
      foreach(var sprite in _sprites)
      {
        sprite.Update(gameTime, _sprites);
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

      _score.Draw(spriteBatch);

      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
