using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Tutorial021.Sprites;

namespace Tutorial021
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
      graphics.PreferredBackBufferWidth = ScreenWidth;
      graphics.PreferredBackBufferHeight = ScreenHeight;
      graphics.ApplyChanges();

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

      var texture = Content.Load<Texture2D>("Square");

      _sprites = new List<Sprite>()
      {
        new Sprite(Content.Load<Texture2D>("Backgrounds/Sky"))
        {
          Position = new Vector2(ScreenWidth / 2, ScreenHeight / 2),
          CollisionType = CollisionTypes.None,
        },
        new Player(Content.Load<Texture2D>("Player/boy"))
        {
          Position = new Vector2(100, 100),
          CollisionType = CollisionTypes.Full,
        },
        new Sprite(texture)
        {
          Position = new Vector2(50, 350),
          Colour = Color.Black,
          CollisionType = CollisionTypes.Full,
        },
        new Sprite(texture)
        {
          Position = new Vector2(50, 400),
          Colour = Color.Black,
          CollisionType = CollisionTypes.Full,
        },
        new Sprite(texture)
        {
          Position = new Vector2(100, 400),
          Colour = Color.Black,
          CollisionType = CollisionTypes.Full,
        },
        new Sprite(texture)
        {
          Position = new Vector2(150, 400),
          Colour = Color.Black,
          CollisionType = CollisionTypes.Full,
        },
        new Sprite(texture)
        {
          Position = new Vector2(200, 400),
          Colour = Color.Black,
          CollisionType = CollisionTypes.Full,
        },
        new Sprite(texture)
        {
          Position = new Vector2(200, 350),
          Colour = Color.Black,
          CollisionType = CollisionTypes.Full,
        },
        new Sprite(texture)
        {
          Position = new Vector2(250, 350),
          Colour = Color.Black,
          CollisionType = CollisionTypes.Full,
        },
        new Sprite(texture)
        {
          Position = new Vector2(300, 350),
          Colour = Color.Black,
          CollisionType = CollisionTypes.Full,
        },
        new Sprite(texture)
        {
          Position = new Vector2(350, 350),
          Colour = Color.Black,
          CollisionType = CollisionTypes.Full,
        },
        new Sprite(texture)
        {
          Position = new Vector2(400, 350),
          Colour = Color.Black,
          CollisionType = CollisionTypes.Full,
        },
        new Sprite(texture)
        {
          Position = new Vector2(450, 350),
          Colour = Color.Black,
          CollisionType = CollisionTypes.Full,
        },
        new Sprite(texture)
        {
          Position = new Vector2(500, 350),
          Colour = Color.Black,
          CollisionType = CollisionTypes.Full,
        },
        new Sprite(texture)
        {
          Position = new Vector2(550, 350),
          Colour = Color.Black,
          CollisionType = CollisionTypes.Full,
        },
        new Sprite(texture)
        {
          Position = new Vector2(600, 350),
          Colour = Color.Black,
          CollisionType = CollisionTypes.Full,
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
        sprite.Update(gameTime);

      CheckCollision(gameTime);

      foreach (var sprite in _sprites)
        sprite.ApplyPhysics(gameTime);

      base.Update(gameTime);
    }

    public void CheckCollision(GameTime gameTime)
    {
      var collidableSprites = _sprites.Where(c => c.CollisionType != CollisionTypes.None);

      foreach (var spriteA in collidableSprites)
      {
        foreach (var spriteB in collidableSprites)
        {
          // Don't do anything if they're the same sprite!
          if (spriteA == spriteB)
            continue;

          if (spriteA.WillIntersect(spriteB))
            //if (spriteA.Rectangle.Intersects(spriteB.Rectangle))
            spriteA.OnCollide(spriteB);
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
