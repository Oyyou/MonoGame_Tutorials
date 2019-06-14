using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial029.Misc;

namespace Tutorial029.Models
{
  public class LevelModel
  {
    public string Name { get; set; }

    public List<ScrollingBackground> ScrollingBackgrounds { get; set; } = new List<ScrollingBackground>();
  }
}
