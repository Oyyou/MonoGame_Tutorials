using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial010.Models;

namespace Tutorial010.Sprites
{
  public class Sprite
  {
    protected Texture2D _texture;

    public Vector2 Position;
    public Vector2 Velocity;
    public float Speed;
    public Input Input;

    public Rectangle Rectangle
    {
      get
      {
        return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
      }
    }

    public Sprite(Texture2D texture)
    {
      _texture = texture;
    }

    public virtual void Update(GameTime gameTime, List<Sprite> sprites)
    {

    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(_texture, Position, Color.White);
    }

    #region Colloision
    protected bool IsTouchingLeft(Sprite sprite)
    {
      return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
        this.Rectangle.Left < sprite.Rectangle.Left &&
        this.Rectangle.Bottom > sprite.Rectangle.Top &&
        this.Rectangle.Top < sprite.Rectangle.Bottom;
    }

    protected bool IsTouchingRight(Sprite sprite)
    {
      return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
        this.Rectangle.Right > sprite.Rectangle.Right &&
        this.Rectangle.Bottom > sprite.Rectangle.Top &&
        this.Rectangle.Top < sprite.Rectangle.Bottom;
    }

    protected bool IsTouchingTop(Sprite sprite)
    {
      return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
        this.Rectangle.Top < sprite.Rectangle.Top &&
        this.Rectangle.Right > sprite.Rectangle.Left &&
        this.Rectangle.Left < sprite.Rectangle.Right;
    }

    protected bool IsTouchingBottom(Sprite sprite)
    {
      return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
        this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
        this.Rectangle.Right > sprite.Rectangle.Left &&
        this.Rectangle.Left < sprite.Rectangle.Right;
    }
    #endregion
  }
}
