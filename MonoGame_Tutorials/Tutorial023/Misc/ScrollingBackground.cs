using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial023.Sprites;

namespace Tutorial023.Misc
{
  public class ScrollingBackground : Component
  {
    private float _layer;

    private float _scrollingSpeed;

    private List<Sprite> _sprites;

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

    public ScrollingBackground(Texture2D texture, float scrollingSpeed)
      : this(new List<Texture2D>() { texture, texture }, scrollingSpeed)
    {

    }

    public ScrollingBackground(List<Texture2D> textures, float scrollingSpeed)
    {
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
    }

    public override void Update(GameTime gameTime)
    {
      foreach (var sprite in _sprites)
      {
        sprite.Position.X -= (float)(_scrollingSpeed * gameTime.ElapsedGameTime.TotalSeconds);
      }

      for (int i = 0; i < _sprites.Count; i++)
      {
        var sprite = _sprites[i];

        if (sprite.Rectangle.Right <= 0)
        {
          var index = i + 1;
          if (index >= _sprites.Count)
            index = 0;

          sprite.Position.X = _sprites[index].Rectangle.Right - 1;
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
