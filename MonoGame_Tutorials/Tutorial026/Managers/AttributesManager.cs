using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial026.Models;

namespace Tutorial026.Managers
{
  class AttributesManager
  {
    private List<Attributes> _attributeModifiers;

    public AttributesManager(List<Attributes> attributeModifiers)
    {
      _attributeModifiers = attributeModifiers;
    }

    public void Update(GameTime gameTime)
    {
      for (int i = 0; i < _attributeModifiers.Count; i++)
      {
        _attributeModifiers[i].Timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_attributeModifiers[i].Timer <= 0)
        {
          _attributeModifiers.RemoveAt(i);
          i--;
        }
      }
    }
  }
}
