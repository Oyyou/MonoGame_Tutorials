using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tutorial017.Core;

namespace Tutorial017
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    private Camera _camera;

    private Texture2D _playerTexture;

    private Texture2D _backgroundTexture;

    private Vector2 _playerPosition;

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

      _camera = new Camera();

      _playerTexture = Content.Load<Texture2D>("Square");
      _backgroundTexture = Content.Load<Texture2D>("Background");
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
      if (Keyboard.GetState().IsKeyDown(Keys.W))
        _playerPosition.Y -= 3f;
      if (Keyboard.GetState().IsKeyDown(Keys.S))
        _playerPosition.Y += 3f;

      if (Keyboard.GetState().IsKeyDown(Keys.A))
        _playerPosition.X -= 3f;
      if (Keyboard.GetState().IsKeyDown(Keys.D))
        _playerPosition.X += 3f;

      _camera.Follow(_playerPosition);

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

      spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

      spriteBatch.End();

      spriteBatch.Begin(transformMatrix: _camera.Transform);

      spriteBatch.Draw(_playerTexture, _playerPosition, Color.Green);
      spriteBatch.Draw(_playerTexture, new Vector2(0, 0), Color.Red);

      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
