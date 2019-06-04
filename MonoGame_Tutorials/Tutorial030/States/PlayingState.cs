using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tutorial030.Models;
using Tutorial030.Sprites;

namespace Tutorial030.States
{
  public class PlayingState : State
  {
    private LevelModel _level;

    public PlayingState(GameModel gameModel, LevelModel level)
      : base(gameModel)
    {
      _level = level;
    }

    public override void LoadContent()
    {

    }

    public override void Update(GameTime gameTime)
    {
      _level.Player.Update(gameTime);

      foreach (var sb in _level.ScrollingBackgrounds)
        sb.Update(gameTime);

      _level.Emitter?.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      _spriteBatch.Begin(SpriteSortMode.FrontToBack);

      _level.Player.Draw(gameTime, _spriteBatch);

      foreach (var sb in _level.ScrollingBackgrounds)
        sb.Draw(gameTime, _spriteBatch);

      _spriteBatch.End();

      DrawEmitter(gameTime);
    }

    private void DrawEmitter(GameTime gameTime)
    {
      if (_level.Emitter == null)
        return;

      _spriteBatch.Begin();

      _level.Emitter.Draw(gameTime, _spriteBatch);

      _spriteBatch.End();
    }
  }
}
