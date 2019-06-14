using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tutorial029.Controls;
using Tutorial029.Misc;
using Tutorial029.Models;
using Tutorial029.Sprites;

namespace Tutorial029.States
{
  public class LevelSelectionState : State
  {
    private List<LevelSelector> _components;

    private SpriteFont _font;

    private Player _player;

    public LevelSelectionState(GameModel gameModel)
      : base(gameModel)
    {
      _font = _content.Load<SpriteFont>("Fonts/Font");

      _player = new Player(_content.Load<Texture2D>("Player/boy"))
      {
        Layer = 1.0f,
        Position = new Vector2(50, 500),
      };

      var levelModel1 = new LevelModel()
      {
        Name = "Mountains",
        ScrollingBackgrounds = new List<ScrollingBackground>()
        {
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Trees"), _player, 60f)
          {
            Layer = 0.99f,
          },
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Floor"), _player, 60f)
          {
            Layer = 0.9f,
          },
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Hills_Front"), _player, 40f)
          {
            Layer = 0.8f,
          },
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Hills_Middle"), _player, 30f)
          {
            Layer = 0.79f,
          },
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Clouds_Fast"), _player, 25f, true)
          {
            Layer = 0.78f,
          },
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Hills_Back"), _player, 0f)
          {
            Layer = 0.77f,
          },
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Clouds_Slow"), _player, 10f, true)
          {
            Layer = 0.7f,
          },
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Sky"), _player, 0f)
          {
            Layer = 0.1f,
          },
        },
      };

      var levelModel2 = new LevelModel()
      {
        Name = "Snowy Mountains",
        ScrollingBackgrounds = new List<ScrollingBackground>()
        {
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Trees"), _player, 60f)
          {
            Layer = 0.99f,
          },
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Floor"), _player, 60f)
          {
            Layer = 0.9f,
          },
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Hills_Front"), _player, 40f)
          {
            Layer = 0.8f,
          },
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Hills_Middle"), _player, 30f)
          {
            Layer = 0.79f,
          },
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Clouds_Fast"), _player, 25f, true)
          {
            Layer = 0.78f,
          },
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Hills_Back"), _player, 0f)
          {
            Layer = 0.77f,
          },
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Clouds_Slow"), _player, 10f, true)
          {
            Layer = 0.7f,
          },
          new ScrollingBackground(_content.Load<Texture2D>("ScrollingBackgrounds/Sky"), _player, 0f)
          {
            Layer = 0.1f,
          },
        },
      };

      _components = new List<LevelSelector>()
      {
        new LevelSelector(_player, levelModel1)
        {
          Scale = 0.25f,
          Position = new Vector2(50, 50),
        },
        new LevelSelector(_player, levelModel2)
        {
          Scale = 0.25f,
          Position = new Vector2(420, 50),
        },
      };
    }

    public override void LoadContent()
    {

    }

    public override void Update(GameTime gameTime)
    {
      foreach (var component in _components)
      {
        component.IsMouseHovering = false;

        var mouseRectangle = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);

        if (mouseRectangle.Intersects(new Rectangle(component.Viewport.X, component.Viewport.Y, component.Viewport.Width, component.Viewport.Height)))
          component.IsMouseHovering = true;

        component.Update(gameTime);
      }
    }

    public override void Draw(GameTime gameTime)
    {
      var originalViewport = _graphics.GraphicsDevice.Viewport;



      foreach (var component in _components)
      {
        _spriteBatch.Begin();

        _spriteBatch.DrawString(_font, component.Name, component.Position - new Vector2(-5, 25), Color.Black);

        _spriteBatch.End();

        _graphics.GraphicsDevice.Viewport = new Viewport((int)component.Position.X, (int)component.Position.Y, 320, 180);

        var scaleX = (float)_graphics.GraphicsDevice.Viewport.Width / Game1.ScreenWidth;
        var scaleY = (float)_graphics.GraphicsDevice.Viewport.Height / Game1.ScreenHeight;
        var scaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: scaleMatrix);

        component.Draw(gameTime, _spriteBatch);

        _spriteBatch.End();

        _graphics.GraphicsDevice.Viewport = originalViewport;
      }

    }
  }
}
