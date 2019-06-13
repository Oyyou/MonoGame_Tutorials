using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial026.Models
{
  public class Attributes
  {
    public float Speed { get; set; }

    /// <summary>
    /// How long the Attribute lasts for
    /// </summary>
    public float Timer { get; set; }

    public Attributes()
    {
      Timer = 5f;
    }

    public static Attributes operator +(Attributes a, Attributes b)
    {
      return new Attributes()
      {
        Speed = a.Speed + b.Speed,
      };
    }

    public static Attributes operator -(Attributes a, Attributes b)
    {
      return new Attributes()
      {
        Speed = a.Speed - b.Speed,
      };
    }
  }
}
