using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tutorial026.Models;

namespace Tutorial026.Sprites
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
  }
}
