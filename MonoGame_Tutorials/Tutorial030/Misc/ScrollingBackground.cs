using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial030.Sprites;

namespace Tutorial030.Misc
{
  public class ScrollingBackground : Component
  {
    private bool _constantSpeed;

    private float _layer;

    private float _scrollingSpeed;

    private List<Sprite> _sprites;

    private readonly Player _player;

    private float _speed;

    public float Layer
    {
      get { return _layer; }
      set
      {
        _layer = value;

        foreach (var sprite in _sprites)
          sprite.Layer = _layer;
      }
    }

    public ScrollingBackground(Texture2D texture, Player player, float scrollingSpeed, bool constantSpeed = false)
      : this(new List<Texture2D>() { texture, texture }, player, scrollingSpeed, constantSpeed)
    {

    }

    public ScrollingBackground(List<Texture2D> textures, Player player, float scrollingSpeed, bool constantSpeed = false)
    {
      _player = player;

      _sprites = new List<Sprite>();

      for (int i = 0; i < textures.Count; i++)
      {
        var texture = textures[i];

        _sprites.Add(new Sprite(texture)
        {
          Position = new Vector2(i * texture.Width - Math.Min(i, i + 1), Game1.ScreenHeight - texture.Height),
        });
      }

      _scrollingSpeed = scrollingSpeed;

      _constantSpeed = constantSpeed;
    }

    public override void Update(GameTime gameTime)
    {
      ApplySpeed(gameTime);

      CheckPosition();
    }

    private void ApplySpeed(GameTime gameTime)
    {
      _speed = (float)(_scrollingSpeed * gameTime.ElapsedGameTime.TotalSeconds);

      if (!_constantSpeed || _player.Velocity.X > 0)
        _speed *= _player.Velocity.X;

      foreach (var sprite in _sprites)
      {
        sprite.X -= _speed;
      }
    }

    private void CheckPosition()
    {
      for (int i = 0; i < _sprites.Count; i++)
      {
        var sprite = _sprites[i];

        if (sprite.Rectangle.Right <= 0)
        {
          var index = i - 1;

          if (index < 0)
            index = _sprites.Count - 1;

          sprite.X = _sprites[index].Rectangle.Right - (_speed * 2);
        }
      }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      foreach (var sprite in _sprites)
        sprite.Draw(gameTime, spriteBatch);
    }
  }
}
