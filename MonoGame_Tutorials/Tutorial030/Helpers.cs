using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial030.Models;

namespace Tutorial030
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
