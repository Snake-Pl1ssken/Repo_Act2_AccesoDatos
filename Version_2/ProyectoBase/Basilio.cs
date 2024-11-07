using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasilioMixer
{
    internal class Basilio
    {

        static Sprite basilioSprite;
        static Sprite gafasSprite;
        static Sprite messageListen;
        static Sprite messageWellDone;

        public static void Init()
        {
            Configuration config = Program.GetConfig();

            basilioSprite = new Sprite();
            basilioSprite.Texture = new Texture(config.basilioTexturePath);
            basilioSprite.Origin = new Vector2f(basilioSprite.Texture.Size.X / 2.0f, basilioSprite.Texture.Size.Y);

            gafasSprite = new Sprite();
            gafasSprite.Texture = new Texture(config.basilioGlassesTexturePath);

            messageListen = new Sprite();
            messageListen.Texture = new Texture(config.basilioMessageListenTexturePath);
            messageListen.Position = config.basilioMessagePosition;

            messageWellDone = new Sprite();
            messageWellDone.Texture = new Texture(config.basilioMessageWellDoneTexturePath);
            messageWellDone.Position = config.basilioMessagePosition;
        }

        public static void Update()
        {
            Configuration config = Program.GetConfig();

            float solvedProgress = Board.GetSolvedProgress();
            float patternProgress = Board.GetPatternProgress();

            basilioSprite.Position = config.basilioPosition + solvedProgress *
                                    Utils.Oscillator(patternProgress * config.basilioOscilator1Multiplier, config.basilioOscilator1X, config.basilioOscilator1Y) +
                                    solvedProgress * Utils.Oscillator(patternProgress * config.basilioOscilator2Multiplier, config.basilioOscilator2X, config.basilioOscilator2Y);
            gafasSprite.Position = basilioSprite.Position + config.basilioGlassesOffset +
                                    solvedProgress * Utils.Oscillator(patternProgress * config.basilioOscilatorGlassesMultiplier, config.basilioOscilatorGlassesX, config.basilioOscilatorGlassesY);

        }

        public static void Draw(RenderWindow window)
        {
            Configuration config = Program.GetConfig();

            window.Draw(basilioSprite);

            float solvedProgress = Board.GetSolvedProgress();

            if (solvedProgress >= config.basilioShowGlassesProgress && (!Board.IsShowingPattern() || Board.IsSolved()))
            {
                window.Draw(gafasSprite);
            }

            if (Board.IsSolved())
            {
                window.Draw(messageWellDone);
            }
            else if (Board.IsShowingPattern())
            {
                window.Draw(messageListen);
            }


        }

    }
}
