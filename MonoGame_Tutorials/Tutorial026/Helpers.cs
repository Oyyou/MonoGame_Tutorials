using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial026.Models;

namespace Tutorial026
{
  public static class Helpers
  {
    public static Attributes Sum(this IEnumerable<Attributes> attributes)
    {
      var finalAttributes = new Attributes();

      foreach (var attribute in attributes)      
        finalAttributes += attribute;

      return finalAttributes;
    }
  }
}
