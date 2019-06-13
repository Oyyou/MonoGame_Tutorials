using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tutorial026.Managers;
using Tutorial026.Models;

namespace Tutorial026.Sprites
{
  public class Player : Sprite
  {
    private AttributesManager _attributeManager;

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

      _attributeManager = new AttributesManager(AttributeModifiers);
    }

    public override void Update(GameTime gameTime)
    {
      _attributeManager.Update(gameTime);
    }

    public override void OnCollide(Sprite sprite)
    {
      switch (sprite)
      {
        case PowerUp powerUp:

          PowerUpCollected(powerUp);

          break;

        default:
          throw new Exception("Unexpected sprite type: " + sprite.ToString());
      }
    }

    private void PowerUpCollected(PowerUp powerUp)
    {
      powerUp.IsRemoved = true;
      AttributeModifiers.Add(powerUp.Attributes);
    }
  }
}
