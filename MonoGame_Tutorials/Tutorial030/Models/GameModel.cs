using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial030.Models
{
  public class GameModel
  {
    public ContentManager ContentManger { get; set; }

    public GraphicsDeviceManager GraphicsDeviceManager { get; set; }

    public SpriteBatch SpriteBatch { get; set; }
  }
}
