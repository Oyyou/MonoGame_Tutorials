using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Tutorial011.Models;
using Tutorial011.Sprites;

namespace Tutorial011
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

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

      // NOTE: I no-longer use this reference as it affects different objects if being used multiple times!
      var animations = new Dictionary<string, Animation>()
      {
        { "WalkUp", new Animation(Content.Load<Texture2D>("Player/WalkingUp"), 3) },
        { "WalkDown", new Animation(Content.Load<Texture2D>("Player/WalkingDown"), 3) },
        { "WalkLeft", new Animation(Content.Load<Texture2D>("Player/WalkingLeft"), 3) },
        { "WalkRight", new Animation(Content.Load<Texture2D>("Player/WalkingRight"), 3) },
      };

      _sprites = new List<Sprite>()
      {
        new Sprite(new Dictionary<string, Animation>()
        {
          { "WalkUp", new Animation(Content.Load<Texture2D>("Player/WalkingUp"), 3) },
          { "WalkDown", new Animation(Content.Load<Texture2D>("Player/WalkingDown"), 3) },
          { "WalkLeft", new Animation(Content.Load<Texture2D>("Player/WalkingLeft"), 3) },
          { "WalkRight", new Animation(Content.Load<Texture2D>("Player/WalkingRight"), 3) },
        })
        {
          Position = new Vector2(100, 100),
          Input = new Input()
          {
            Up = Keys.W,
            Down = Keys.S,
            Left = Keys.A,
            Right = Keys.D,
          },
        },
        new Sprite(new Dictionary<string, Animation>()
        {
          { "WalkUp", new Animation(Content.Load<Texture2D>("Player/WalkingUp"), 3) },
          { "WalkDown", new Animation(Content.Load<Texture2D>("Player/WalkingDown"), 3) },
          { "WalkLeft", new Animation(Content.Load<Texture2D>("Player/WalkingLeft"), 3) },
          { "WalkRight", new Animation(Content.Load<Texture2D>("Player/WalkingRight"), 3) },
        })
        {
          Position = new Vector2(150, 100),
          Input = new Input()
          {
            Up = Keys.Up,
            Down = Keys.Down,
            Left = Keys.Left,
            Right = Keys.Right,
          },
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
      foreach (var sprite in _sprites)
        sprite.Update(gameTime, _sprites);

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
