using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Tutorial025.Models;
using Tutorial025.Sprites;

namespace Tutorial025
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    private Player _player;

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

      var hatAttributes = new Attributes()
      {
        HealthPoint = 3,
        Speed = 0,
      };

      var jumperAttributes = new Attributes()
      {
        HealthPoint = 2,
        Speed = 2,
      };

      var trouserAttributes = new Attributes()
      {
        HealthPoint = 0,
        Speed = 3,
      };

      _player = new Player(Content.Load<Texture2D>("Player/boy"))
      {
        Position = new Vector2(50, 50),
        BaseAttributes = new Attributes()
        {
          HealthPoint = 10,
          Speed = 3,
        },
        AttributeModifiers = new List<Attributes>()
        {
          hatAttributes,
          jumperAttributes,
          trouserAttributes,
        },
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
      _player.Update(gameTime);

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

      _player.Draw(gameTime, spriteBatch);

      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
