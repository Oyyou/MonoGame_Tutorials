using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tutorial025.Models;

namespace Tutorial025.Sprites
{
  public class Player : Sprite
  {
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

    public Player(Texture2D texture) 
      : base(texture)
    {
      BaseAttributes = new Attributes();

      AttributeModifiers = new List<Attributes>();
    }

    public override void Update(GameTime gameTime)
    {
      var speed = TotalAttributes.Speed;

      var velocity = new Vector2();

      if (Keyboard.GetState().IsKeyDown(Keys.D))
        velocity.X = speed;
      else if (Keyboard.GetState().IsKeyDown(Keys.A))
        velocity.X = -speed;

      Position += velocity;
    }
  }
}
