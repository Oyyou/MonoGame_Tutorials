using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tutorial029.Misc;
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

    public string Name { get; set; }

    public LevelSelector(ContentManager content, string name)
    {
      Name = name;

      _player = new Player(content.Load<Texture2D>("Player/boy"))
      {
        Layer = 1.0f,
        Position = new Vector2(50, 500),
      };

      _scrollingBackgrounds = new List<ScrollingBackground>()
      {
        new ScrollingBackground(content.Load<Texture2D>("ScrollingBackgrounds/Trees"), _player, 60f)
        {
          Layer = 0.99f,
        },
        new ScrollingBackground(content.Load<Texture2D>("ScrollingBackgrounds/Floor"), _player, 60f)
        {
          Layer = 0.9f,
        },
        new ScrollingBackground(content.Load<Texture2D>("ScrollingBackgrounds/Hills_Front"), _player, 40f)
        {
          Layer = 0.8f,
        },
        new ScrollingBackground(content.Load<Texture2D>("ScrollingBackgrounds/Hills_Middle"), _player, 30f)
        {
          Layer = 0.79f,
        },
        new ScrollingBackground(content.Load<Texture2D>("ScrollingBackgrounds/Clouds_Fast"), _player, 25f, true)
        {
          Layer = 0.78f,
        },
        new ScrollingBackground(content.Load<Texture2D>("ScrollingBackgrounds/Hills_Back"), _player, 0f)
        {
          Layer = 0.77f,
        },
        new ScrollingBackground(content.Load<Texture2D>("ScrollingBackgrounds/Clouds_Slow"), _player, 10f, true)
        {
          Layer = 0.7f,
        },
        new ScrollingBackground(content.Load<Texture2D>("ScrollingBackgrounds/Sky"), _player, 0f)
        {
          Layer = 0.1f,
        },
      };
    }

    public void LoadContent(ContentManager content)
    {
    }

    public override void Update(GameTime gameTime)
    {
      if (!IsMouseHovering)
        return;

      _player.Update(gameTime);

      foreach (var sb in _scrollingBackgrounds)
        sb.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      _player.Draw(gameTime, spriteBatch);

      foreach (var sb in _scrollingBackgrounds)
        sb.Draw(gameTime, spriteBatch);
    }
  }
}
