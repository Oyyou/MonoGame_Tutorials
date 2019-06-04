using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Tutorial030.Emitters;
using Tutorial030.Misc;
using Tutorial030.Models;
using Tutorial030.Sprites;
using Tutorial030.States;

namespace Tutorial030
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    public static int ScreenWidth = 1280;
    public static int ScreenHeight = 720;

    public static Random Random;

    private GameModel _gameModel;

    private State _currentState;

    private LevelModel _sunnyLevel;

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
      graphics.PreferredBackBufferWidth = ScreenWidth;
      graphics.PreferredBackBufferHeight = ScreenHeight;
      graphics.ApplyChanges();

      Random = new Random();

      IsMouseVisible = true;

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

      _gameModel = new GameModel()
      {
        ContentManger = Content,
        GraphicsDeviceManager = graphics,
        SpriteBatch = spriteBatch,
      };

      _currentState = new LevelSelectorState(_gameModel);
      _currentState.LoadContent();

      var player = new Player(new Dictionary<string, Animation>()
      {
        { "Running", new Animation(Content.Load<Texture2D>("Player/Running"), 4) },
        { "Jumping", new Animation(Content.Load<Texture2D>("Player/Jumping"), 4) },
        { "Falling", new Animation(Content.Load<Texture2D>("Player/Falling"), 4) },
      })
      {
        BaseAttributes = new Attributes()
        {
          Speed = 3f,
        },
        Position = new Vector2(50, 300),
        Layer = 1f,
      };

      _sunnyLevel = new LevelModel(player)
      {
        //Emitter = new SnowEmitter(new Particle(Content.Load<Texture2D>("Particles/Snow"))),
        ScrollingBackgrounds = new List<ScrollingBackground>()
        {
          new ScrollingBackground(Content.Load<Texture2D>("Levels/Sunny/Trees"), player, 60f)
          {
            Layer = 0.99f,
          },
          new ScrollingBackground(Content.Load<Texture2D>("Levels/Sunny/Floor"), player, 60f)
          {
            Layer = 0.9f,
          },
          new ScrollingBackground(Content.Load<Texture2D>("Levels/Sunny/Hills_Front"), player, 40f)
          {
            Layer = 0.8f,
          },
          new ScrollingBackground(Content.Load<Texture2D>("Levels/Sunny/Hills_Middle"), player, 30f)
          {
            Layer = 0.79f,
          },
          new ScrollingBackground(Content.Load<Texture2D>("Levels/Sunny/Clouds_Fast"), player, 25f, true)
          {
            Layer = 0.78f,
          },
          new ScrollingBackground(Content.Load<Texture2D>("Levels/Sunny/Hills_Back"), player, 0f)
          {
            Layer = 0.77f,
          },
          new ScrollingBackground(Content.Load<Texture2D>("Levels/Sunny/Clouds_Slow"), player, 10f, true)
          {
            Layer = 0.7f,
          },
          new ScrollingBackground(Content.Load<Texture2D>("Levels/Sunny/Sky"), player, 0f)
          {
            Layer = 0.1f,
          },
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
      switch (_currentState)
      {
        case LevelSelectorState levelSelectorState:
          _currentState = new PlayingState(_gameModel, _sunnyLevel);
          break;

        case CustomiseState customiseState:

          break;

        case PlayingState playingState:

          break;

        default:
          throw new Exception("Unknown state: " + _currentState.ToString());
      }

      _currentState.Update(gameTime);

      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      _currentState.Draw(gameTime);

      base.Draw(gameTime);
    }
  }
}
