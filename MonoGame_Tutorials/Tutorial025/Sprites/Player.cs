using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Tutorial025.Models;

namespace Tutorial025.Sprites
{
  public class Player : Sprite
  {
    public Attributes BaseAttributes { get; set; }

    public List<Attributes> Attributes { get; set; }

    //public Attributes Test
    //{
    //  get
    //  {
    //    return BaseAttributes + Attributes.Sum(c => c);
    //  }
    //}

    public Player(Texture2D texture) 
      : base(texture)
    {
    }
  }
}
