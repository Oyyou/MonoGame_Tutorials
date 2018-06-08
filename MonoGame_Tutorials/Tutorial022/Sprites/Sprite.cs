using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tutorial022.Sprites
{
  public class Sprite : Component
  {
    protected float _layer { get; set; }

    protected Vector2 _origin { get; set; }

    protected Vector2 _position { get; set; }

    protected float _rotation { get; set; }

    protected Texture2D _texture;

    public Color Colour { get; set; }

    /// <summary>
    /// The sprite that we want to follow
    /// </summary>
    public Sprite FollowTarget { get; set; }

    /// <summary>
    /// How close we want to be to our target
    /// </summary>
    public float FollowDistance { get; set; }

    public bool IsRemoved { get; set; }

    public Vector2 Direction;

    public float RotationVelocity = 3f;

    public float LinearVelocity = 4f;

    public float Layer
    {
      get { return _layer; }
      set
      {
        _layer = value;
      }
    }

    public Vector2 Origin
    {
      get { return _origin; }
      set
      {
        _origin = value;
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

    public Rectangle Rectangle
    {
      get
      {
        return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, _texture.Width, _texture.Height);
      }
    }

    public float Rotation
    {
      get { return _rotation; }
      set
      {
        _rotation = value;
      }
    }

    public Sprite(Texture2D texture)
    {
      _texture = texture;

      Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

      Colour = Color.White;
    }

    public override void Update(GameTime gameTime)
    {
      Follow();
    }

    protected void Follow()
    {
      if (FollowTarget == null)
        return;

      var distance = FollowTarget.Position - this.Position;
      _rotation = (float)Math.Atan2(distance.Y, distance.X);

      Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

      var currentDistance = Vector2.Distance(this.Position, FollowTarget.Position);
      if (currentDistance > FollowDistance)
      {
        var t = MathHelper.Min((float)Math.Abs(currentDistance - FollowDistance), LinearVelocity);
        var velocity = Direction * t;

        Position += velocity;
      }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(_texture, Position, null, Colour, _rotation, Origin, 1f, SpriteEffects.None, Layer);
    }

    /// <summary>
    /// Once the follow target has been set, the sprite will follow the target
    /// </summary>
    /// <param name="followTarget">The sprite we want to follow</param>
    /// <param name="followDistance">how close we'll get to our target</param>
    /// <returns></returns>
    public Sprite SetFollowTarget(Sprite followTarget, float followDistance)
    {
      FollowTarget = followTarget;

      FollowDistance = followDistance;

      return this;
    }
  }
}
