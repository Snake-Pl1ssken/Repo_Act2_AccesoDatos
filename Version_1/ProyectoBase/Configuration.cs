using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasilioMixer
{
    internal class Configuration
    {
        // General

        public int windowWidth = 1280;
        public int windowHeight = 720;
        public string windowTitle = "Basilio's Mix";
        public string backgroundTexturePath = "Assets/Background.png";
        public int soundPoolSize = 25;

        // Board
        
        public float[] boardRowPitches =  new float[] { 1,  1.122324159f,  1.259938838f,  1.335168196f,
                                                1.498470948f,  1.681957187f,  1.888073394f, 2.0f };

        public Vector2f boardPosition = new Vector2f(566, 108);
        public string boardTexturePath = "Assets/Board.png";
        public int boardRows = 8;
        public int boardColumns = 8;

        public string patternMusicPath = "Assets/Loop.wav";
        public float patternMusicVolume = 50.0f;
        public int patternsUntilShow = 4;
        public Color patternCorrectHighlightColor = new Color(0, 255, 0);
        public Color patternShowHighlightColor = new Color(128, 128, 128);
        public Color patternNormalHighlightColor = Color.Yellow;


        public Vector2f piecesPosition = new Vector2f(670, 212);
        public int piecesSize = 39;
        public string[] piecesSoundPaths = new string[] {
                                            "Assets/Effect01.wav",
                                            "Assets/Effect02.wav",
                                            "Assets/Effect03.wav",
                                            };

        public string[] piecesTexturePaths = new string[] {
                                            "Assets/Piece01.png",
                                            "Assets/Piece02.png",
                                            "Assets/Piece03.png",
                                            };

        public string pieceBaseTexturePath = "Assets/Base.png";

        public int piecesCount = 3;

        // Basilio

        public string basilioTexturePath = "Assets/Basilio.png";
        public string basilioGlassesTexturePath = "Assets/Gafas.png";
        public string basilioMessageListenTexturePath = "Assets/MessageListen.png";
        public string basilioMessageWellDoneTexturePath = "Assets/MessageWellDone.png";
        public Vector2f basilioMessagePosition = new Vector2f(110, 40);
        public Vector2f basilioPosition = new Vector2f(253, 730 + 15);
        public float basilioShowGlassesProgress = 0.75f;
        public Vector2f basilioGlassesOffset = new Vector2f(-45, -418);
        public float basilioOscilator1Multiplier = 2;
        public float basilioOscilator1X = 2;
        public float basilioOscilator1Y = 5;
        public float basilioOscilator2Multiplier = 4;
        public float basilioOscilator2X = 4;
        public float basilioOscilator2Y = 10;
        public float basilioOscilatorGlassesMultiplier = 2;
        public float basilioOscilatorGlassesX = 2;
        public float basilioOscilatorGlassesY = 2;

    }
}
