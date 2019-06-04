using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial030.Emitters;
using Tutorial030.Misc;
using Tutorial030.Sprites;

namespace Tutorial030.Models
{
  public class LevelModel
  {
    public Emitter Emitter;

    public readonly Player Player;

    public List<ScrollingBackground> ScrollingBackgrounds;

    public LevelModel(Player player)
    {
      Player = player;

      Emitter = null;

      ScrollingBackgrounds = new List<ScrollingBackground>();
    }
  }
}
