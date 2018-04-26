using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Tutorial006.Sprites;

namespace Tutorial006
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

      var shipTexture = Content.Load<Texture2D>("Ship");

      _sprites = new List<Sprite>()
      {
        new Ship(shipTexture)
        {
          Position = new Vector2(100, 100),
          Bullet = new Bullet(Content.Load<Texture2D>("Bullet")),
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
      foreach (var sprite in _sprites.ToArray())
        sprite.Update(gameTime, _sprites);

      PostUpdate();

      base.Update(gameTime);
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

      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
