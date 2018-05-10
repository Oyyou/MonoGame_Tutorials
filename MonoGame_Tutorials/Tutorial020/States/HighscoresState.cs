using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tutorial020.Managers;

namespace Tutorial020.States
{
  public class HighscoresState : State
  {
    private SpriteFont _font;

    private ScoreManager _scoreManager;

    public HighscoresState(Game1 game, ContentManager content) 
      : base(game, content)
    {
    }

    public override void LoadContent()
    {
      _font = _content.Load<SpriteFont>("Font");

      _scoreManager = ScoreManager.Load();
    }

    public override void Update(GameTime gameTime)
    {
      if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        _game.ChangeState(new MenuState(_game, _content));
    }

    public override void PostUpdate(GameTime gameTime)
    {

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      spriteBatch.Begin(SpriteSortMode.FrontToBack);

      spriteBatch.DrawString(_font, "Highscores:\n" + string.Join("\n", _scoreManager.HighScores.Select(c => c.PlayerName + ": " + c.Value)), new Vector2(400, 100), Color.Red);

      spriteBatch.End();
    }
  }
}
