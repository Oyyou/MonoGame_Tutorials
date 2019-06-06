using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tutorial030.Managers;
using Tutorial030.Models;

namespace Tutorial030.Sprites
{
  public class Sprite : Component
  {
    protected AnimationManager _animationManager;

    protected Dictionary<string, Animation> _animations;

    protected Texture2D _texture;

    protected Vector2 _position;

    protected float _layer { get; set; }


    public Color Colour { get; set; }

    public float Opacity { get; set; }

    public Vector2 Origin { get; set; }

    public float Rotation { get; set; }

    public float Scale { get; set; }

    public Vector2 Position
    {
      get { return _position; }
      set
      {
        _position = value;

        if (_animationManager != null)
          _animationManager.Position = _position;
      }
    }

    public float X
    {
      get { return Position.X; }
      set
      {
        Position = new Vector2(value, Position.Y);
      }
    }

    public float Y
    {
      get { return Position.Y; }
      set
      {
        Position = new Vector2(Position.X, value);
      }
    }

    public float Layer
    {
      get { return _layer; }
      set
      {
        _layer = value;

        if (_animationManager != null)
          _animationManager.Layer = _layer;
      }
    }

    public Rectangle Rectangle
    {
      get
      {
        int x = 0;
        int y = 0;
        int width = 0;
        int height = 0;

        if (_texture != null)
        {
          width = _texture.Width;
          height = _texture.Height;
        }
        else if (_animationManager != null)
        {
          width = _animationManager.FrameWidth;
          height = _animationManager.FrameHeight;
        }

        return new Rectangle((int)(Position.X - Origin.X), (int)(Position.Y - Origin.Y), (int)(width * Scale), (int)(height * Scale));
      }
    }

    #region

    public Vector2 TopLeft
    {
      get
      {
        return new Vector2(Rectangle.X, Rectangle.Y);
      }
    }

    public Vector2 TopRight
    {
      get
      {
        return new Vector2(Rectangle.X + Rectangle.Width, Rectangle.Y);
      }
    }

    public Vector2 BottomLeft
    {
      get
      {
        return new Vector2(Rectangle.X, Rectangle.Y + Rectangle.Height);
      }
    }

    public Vector2 BottomRight
    {
      get
      {
        return new Vector2(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height);
      }
    }

    public Vector2 Centre
    {
      get
      {
        return new Vector2(Rectangle.X + (Rectangle.Width / 2), Rectangle.Y + (Rectangle.Height / 2));
      }
    }

    public List<Vector2> Dots
    {
      get
      {
        return new List<Vector2>()
        {
          Centre,
          TopRight,
          BottomRight,
          BottomLeft,
          TopLeft,
        };
      }
    }

    public List<Vector2> GetNormals()
    {
      var normals = new List<Vector2>();

      var dots = this.Dots;

      for (int i = 1; i < dots.Count - 1; i++)
      {
        normals.Add(Vector2.Normalize(new Vector2(dots[i + 1].X - dots[i].X, dots[i + 1].Y - dots[i].Y)));
      }

      normals.Add(Vector2.Normalize(new Vector2(dots[1].X - dots[dots.Count - 1].X, dots[1].Y - dots[dots.Count - 1].Y)));

      return normals;
    }

    #endregion

    public bool IsRemoved { get; set; }

    public Sprite(Texture2D texture)
    {
      _texture = texture;

      Opacity = 1f;

      Scale = 1f;

      Origin = new Vector2(0, 0);

      Colour = Color.White;
    }

    public Sprite(Dictionary<string, Animation> animations)
    {
      _animations = animations;
      _animationManager = new AnimationManager(_animations.First().Value);

      Opacity = 1f;

      Scale = 1f;

      Colour = Color.White;
    }

    public override void Update(GameTime gameTime)
    {

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      if (_texture != null)
        spriteBatch.Draw(_texture, Position, null, Colour * Opacity, Rotation, Origin, Scale, SpriteEffects.None, Layer);

      if (_animationManager != null)
        _animationManager.Draw(spriteBatch);

    }

    public virtual void OnCollide(Sprite sprite)
    {

    }

    public bool IsTouching(Sprite sprite)
    {
      return this.Rectangle.Right >= sprite.Rectangle.Left &&
          this.Rectangle.Left <= sprite.Rectangle.Right &&
          this.Rectangle.Bottom >= sprite.Rectangle.Top &&
          this.Rectangle.Top <= sprite.Rectangle.Bottom;
    }

    public bool IsTouchingTopOf(Sprite sprite)
    {
      return this.Rectangle.Right >= sprite.Rectangle.Left &&
          this.Rectangle.Left <= sprite.Rectangle.Right &&
          this.Rectangle.Bottom >= sprite.Rectangle.Top &&
          this.Rectangle.Top < sprite.Rectangle.Top;
    }

    public bool IsTouchingLeftOf(Sprite sprite)
    {
      return this.Rectangle.Bottom >= sprite.Rectangle.Top &&
        this.Rectangle.Top <= sprite.Rectangle.Bottom &&
        this.Rectangle.Right >= sprite.Rectangle.Left &&
        this.Rectangle.Left < sprite.Rectangle.Left;
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }
  }
}
