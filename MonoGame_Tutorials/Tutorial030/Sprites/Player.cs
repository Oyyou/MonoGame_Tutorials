using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tutorial030.Models;

namespace Tutorial030.Sprites
{
  public class Player : Sprite
  {
    public Vector2 Velocity;
    /// <summary>
    /// These are the types of attributes to only change on level-up
    /// </summary>
    public Attributes BaseAttributes { get; set; }

    /// <summary>
    /// These are extra attributes that can be gained from different sources (equipment, power-ups, spells etc)
    /// </summary>
    public List<Attributes> AttributeModifiers { get; set; }

    public Attributes TotalAttributes
    {
      get
      {
        return BaseAttributes + AttributeModifiers.Sum();
      }
    }

    public Player(Dictionary<string, Animation> animations) : base(animations)
    {
      BaseAttributes = new Attributes();

      AttributeModifiers = new List<Attributes>();
    }

    public override void Update(GameTime gameTime)
    {
      Velocity.X = TotalAttributes.Speed;

      SetAnimation();

      _animationManager.Update(gameTime);
    }

    private void SetAnimation()
    {
      if (Velocity.Y < 0)
      {
        _animationManager.Play(_animations["Jumping"]);
      }
      else if (Velocity.Y > 0)
      {
        _animationManager.Play(_animations["Falling"]);
      }
      else
      {
        _animationManager.Play(_animations["Running"]);
      }
    }
  }
}
