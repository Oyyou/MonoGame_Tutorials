using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Tutorial019.Sprites;

namespace Tutorial019
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

      var shipTexture = Content.Load<Texture2D>("Player");

      var bulletPrefab = new Bullet(Content.Load<Texture2D>("Bullet"));

      _sprites = new List<Sprite>()
      {
        new Ship(shipTexture)
        {
          Bullet = bulletPrefab,
          Position = new Vector2(100, 100),
          Colour = Color.Green,
        },
        new Sprite(shipTexture)
        {
          Position = new Vector2(200, 200),
          Colour = Color.Red,
        },
        new Sprite(Content.Load<Texture2D>("Enemy_1"))
        {
          Position = new Vector2(300, 100),
          Colour = Color.Red,
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
      foreach (var sprite in _sprites)
        sprite.Update(gameTime);

      PostUpdate();

      base.Update(gameTime);
    }

    private void PostUpdate()
    {
      // 1. Check collision between all current "Sprites"
      // 2. Add "Children" to the list
      // 3. Remove all "IsRemoved" sprites

      foreach (var spriteA in _sprites)
      {
        foreach (var spriteB in _sprites)
        {
          if (spriteA == spriteB)
            continue;

          if (!spriteA.Rectangle.Intersects(spriteB.Rectangle))
            continue;

          if (spriteA.Intersects(spriteB))
          {
            spriteA.OnCollide(spriteB);
          }
        }
      }

      int count = _sprites.Count;
      for (int i = 0; i < count; i++)
      {
        foreach (var child in _sprites[i].Children)
          _sprites.Add(child);

        _sprites[i].Children.Clear();
      }

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
        sprite.Draw(gameTime, spriteBatch);

      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
