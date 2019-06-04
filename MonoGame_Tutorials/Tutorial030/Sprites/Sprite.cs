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
        return new Rectangle((int)(Position.X - Origin.X), (int)(Position.Y - Origin.Y), _texture.Width, _texture.Height);
      }
    }

    public bool IsRemoved { get; set; }

    public Sprite(Texture2D texture)
    {
      _texture = texture;

      Opacity = 1f;

      Scale = 1f;

      Origin = new Vector2(0, 0);
    }

    public Sprite(Dictionary<string, Animation> animations)
    {
      _animations = animations;
      _animationManager = new AnimationManager(_animations.First().Value);

      Opacity = 1f;

      Scale = 1f;
    }

    public override void Update(GameTime gameTime)
    {

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      if (_texture != null)
        spriteBatch.Draw(_texture, Position, null, Color.White * Opacity, Rotation, Origin, Scale, SpriteEffects.None, Layer);

      if (_animationManager != null)
        _animationManager.Draw(spriteBatch);

    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }
  }
}
