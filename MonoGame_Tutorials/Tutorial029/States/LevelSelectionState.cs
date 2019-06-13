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
using Tutorial029.Models;

namespace Tutorial029.States
{
  public class LevelSelectionState : State
  {
    private List<LevelSelector> _components;

    private SpriteFont _font;

    public LevelSelectionState(GameModel gameModel)
      : base(gameModel)
    {
      _font = _content.Load<SpriteFont>("Fonts/Font");

      _components = new List<LevelSelector>()
      {
        new LevelSelector(_content, "Level 001")
        {
          Scale = 0.25f,
          Position = new Vector2(50, 50),
        },
        new LevelSelector(_content, "Level 002")
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
