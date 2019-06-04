using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial030.Models;

namespace Tutorial030.States
{
  public abstract class State
  {
    protected GameModel _gameModel;

    protected ContentManager _content
    {
      get
      {
        return _gameModel.ContentManger;
      }
    }

    protected GraphicsDeviceManager _graphics
    {
      get
      {
        return _gameModel.GraphicsDeviceManager;
      }
    }

    protected SpriteBatch _spriteBatch
    {
      get
      {
        return _gameModel.SpriteBatch;
      }
    }

    public State(GameModel gameModel)
    {
      _gameModel = gameModel;
    }

    public abstract void LoadContent();

    public abstract void Update(GameTime gameTime);

    public abstract void Draw(GameTime gameTime);
  }
}
