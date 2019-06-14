using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tutorial029.Misc;
using Tutorial029.Models;
using Tutorial029.Sprites;

namespace Tutorial029.Controls
{
  public class LevelSelector : Component
  {
    private Player _player;

    private List<ScrollingBackground> _scrollingBackgrounds;

    private Vector2 _position;

    private float _scale = 1f;

    public bool IsMouseHovering { get; set; }

    public Viewport Viewport
    {
      get
      {
        return new Viewport((int)Position.X, (int)Position.Y, Game1.ScreenWidth / 4, Game1.ScreenHeight / 4);
      }
    }

    public float Scale
    {
      get { return _scale; }
      set
      {
        _scale = value;
      }
    }

    public Vector2 Position
    {
      get { return _position; }
      set
      {
        _position = value;
      }
    }

    public string Name
    {
      get
      {
        return LevelModel.Name;
      }
    }

    private readonly LevelModel LevelModel;

    public LevelSelector(Player player, LevelModel levelModel)
    {
      _player = player;

      LevelModel = levelModel;
    }

    public void LoadContent(ContentManager content)
    {
    }

    public override void Update(GameTime gameTime)
    {
      if (!IsMouseHovering)
        return;

      _player.Update(gameTime);

      foreach (var sb in LevelModel.ScrollingBackgrounds)
        sb.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      _player.Draw(gameTime, spriteBatch);

      foreach (var sb in LevelModel.ScrollingBackgrounds)
        sb.Draw(gameTime, spriteBatch);
    }
  }
}
