using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Diagnostics;

namespace BasilioMixer
{
    internal static class Board
    {

        static Sprite boardSprite;
        static Sprite pieceBaseSprite;
        static Sprite pieceSprite;

        static Vector2f piecesStartPosition;
        static float pieceSize;

        static SoundBuffer[] pieceSoundEffects;
        static Texture[] pieceTextures;

        static List<int[,]> patternsList;

        static int[,] pattern;

        static int[,] guess;

        static Music patternMusic;
        static float patternDuration;
        static int patternPreviousColumn;
        static int patternColumn;
        static float patternProgress;


        static int patternTotalPieces;
        static int guessedCorrectlyPieces;

        static int patternRepeats;

        public static void Init()
        {
            Random random = Program.GetRandom();
            Configuration config = Program.GetConfig();

            string ruta = Directory.GetCurrentDirectory() + "\\Assets" + "\\Patterns";

            string[] ficheros = Directory.GetFiles(ruta);

            Console.WriteLine(ruta + "\n");
            int RandPattern;
            Random rnd = new();
            RandPattern = rnd.Next(0,ficheros.Length);
            FileStream file = new FileStream(ficheros[RandPattern], FileMode.Open, FileAccess.Read);
            pattern = new int[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ///////////////1/////////////////
                    int tamanyo = sizeof(int);
                    byte[] bytes = new byte[tamanyo];
                    ///////////////1/////////////////

                    ///////////////2///////////////
                    file.Read(bytes, 0, tamanyo);
                    ///////////////2///////////////

                    ///////////////3///////////////
                    int ArrayAInt;
                    ArrayAInt = BitConverter.ToInt32(bytes);
                    ///////////////3///////////////

                    ///////////////4///////////////
                    pattern[i, j] = ArrayAInt;
                    ///////////////4///////////////
                }
            }
            file.Close();

            patternTotalPieces = CountPatternPieces(pattern);


            guessedCorrectlyPieces = 0;

            guess = CreateEmptyPattern();

            patternRepeats = -1;


            piecesStartPosition = config.piecesPosition;
            pieceSize = config.piecesSize;

            

            patternMusic = new Music(config.patternMusicPath);


            pieceSoundEffects = new SoundBuffer[config.piecesSoundPaths.Length];

            for (int i = 0; i < config.piecesSoundPaths.Length; i++)
            {
                pieceSoundEffects[i] = new SoundBuffer(config.piecesSoundPaths[i]);
            }

            pieceTextures = new Texture[config.piecesTexturePaths.Length];

            for(int i = 0; i < config.piecesTexturePaths.Length; i++)
            {
                pieceTextures[i] = new Texture(config.piecesTexturePaths[i]);
            }

            // Init board

            boardSprite = new Sprite();
            boardSprite.Texture = new Texture(config.boardTexturePath);
            boardSprite.Position = config.boardPosition;

            // Init pieceTextures

            pieceBaseSprite = new Sprite();
            pieceBaseSprite.Texture = new Texture(config.pieceBaseTexturePath);

            pieceSprite = new Sprite();
            pieceSprite.Texture = pieceTextures[0];


            patternMusic.Volume = config.patternMusicVolume;
            patternMusic.Loop = true;
            patternMusic.Play();
            patternDuration = patternMusic.Duration.AsSeconds();

            patternPreviousColumn = -1;

        }

        public static float GetPatternProgress()
        {
            return patternProgress;
        }

        public static float GetSolvedProgress()
        {
            return guessedCorrectlyPieces / (float)patternTotalPieces; ;
        }

        public static bool IsShowingPattern()
        {
            return patternRepeats % Program.GetConfig().patternsUntilShow == 0;
        }

        public static bool IsSolved()
        {
            return guessedCorrectlyPieces == patternTotalPieces;
        }

        static int CalculateCorrectGuesses(int[,] pattern, int[,] guesses)
        {
            Configuration config = Program.GetConfig();
            int result = 0;

            for (int i = 0; i < config.boardRows; i++)
            {
                for (int j = 0; j < config.boardColumns; j++)
                {
                    if (pattern[i, j] >= 0 && pattern[i, j] == guess[i, j])
                    {
                        result++;
                        guessedCorrectlyPieces++;
                    }

                    if (guess[i, j] >= 0 && pattern[i, j] != guess[i, j])
                    {
                        guessedCorrectlyPieces--;
                    }

                    if (guessedCorrectlyPieces < 0) { guessedCorrectlyPieces = 0; }
                }
            }

            return result;
        }


        static int CalculateWrongGuesses(int[,] pattern, int[,] guesses)
        {
            Configuration config = Program.GetConfig();
            int result = 0;

            for (int i = 0; i < config.boardRows; i++)
            {
                for (int j = 0; j < config.boardColumns; j++)
                {
                    if (guess[i, j] >= 0 && pattern[i, j] != guess[i, j])
                    {
                        result++;
                    }

                }
            }

            return result;
        }

        static int CountPatternPieces(int[,] p)
        {
            Configuration config = Program.GetConfig();
            int total = 0;
            for (int i = 0; i < config.boardRows; i++)
            {
                for (int j = 0; j < config.boardColumns; j++)
                {
                    if (pattern[i, j] >= 0) { total++; }
                }
            }

            return total;
        }

        static int[,] CreateEmptyPattern()
        {
            Configuration config = Program.GetConfig();
            int[,] result = new int[config.boardRows, config.boardColumns];

            for (int i = 0; i < config.boardRows; i++)
            {
                for (int j = 0; j < config.boardColumns; j++)
                {
                    result[i, j] = -1;
                }
            }

            return result;
        }

        static int[,] CreateRandomPattern()
        {
            Configuration config = Program.GetConfig();
            Random random = Program.GetRandom();

            int[,] result = CreateEmptyPattern();

            int placed = 0;
            int target = 3 + random.Next() % 2;

            while (placed < target)
            {
                int ri = random.Next() % config.boardRows;
                int rj = random.Next() % config.boardColumns;

                if (result[ri, rj] < 0)
                {
                    result[ri, rj] = random.Next() % config.piecesCount;
                    placed++;
                }
            }

            return result;
        }

        public static void Draw(RenderWindow window)
        {
            Configuration config = Program.GetConfig();
            window.Draw(boardSprite);


            for (int i = 0; i < config.boardRows; i++)
            {
                for (int j = 0; j < config.boardColumns; j++)
                {
                    Vector2f p = piecesStartPosition + new Vector2f(j * pieceSize, i * pieceSize);
                    pieceBaseSprite.Position = p;
                    if (patternColumn == j)
                    {
                        if (guessedCorrectlyPieces == patternTotalPieces)
                        { pieceBaseSprite.Color = config.patternCorrectHighlightColor; }
                        else if (patternRepeats % config.patternsUntilShow == 0)
                        { pieceBaseSprite.Color = config.patternShowHighlightColor; }
                        else { pieceBaseSprite.Color = config.patternNormalHighlightColor; }
                    }
                    else { pieceBaseSprite.Color = Color.White; }

                    window.Draw(pieceBaseSprite);

                    int effect = guess[i, j];
                    if (effect >= 0 && (patternRepeats % config.patternsUntilShow != 0 || guessedCorrectlyPieces == patternTotalPieces))
                    {
                        pieceSprite.Position = p;
                        pieceSprite.Texture = pieceTextures[effect];
                        window.Draw(pieceSprite);
                    }

                }
            }


        }

        public static void OnBoardClick(RenderWindow window, Vector2i mp, Mouse.Button button)
        {
            Configuration config = Program.GetConfig();
            if (patternRepeats % config.patternsUntilShow == 0 || guessedCorrectlyPieces == patternTotalPieces) { return; }

            Vector2f bp = (Vector2f)mp - piecesStartPosition;
            int column = (int)(bp.X / pieceSize);
            int row = (int)(bp.Y / pieceSize);

            if (column >= 0 && column < config.boardColumns && row >= 0 && row < config.boardRows)
            {
                if (button == Mouse.Button.Left)
                {
                    guess[row, column]++;

                    if (guess[row, column] >= pieceSoundEffects.Length) { guess[row, column] = 0; }
                }
                else if (button == Mouse.Button.Right)
                {
                    guess[row, column] = -1;
                }
            }

        }


        public static void Update()
        {
            Configuration config = Program.GetConfig();

            patternProgress = patternMusic.PlayingOffset.AsSeconds() / patternDuration;

            patternColumn = (int)(patternProgress * 2 * config.boardColumns);
            if (patternColumn == 2 * config.boardColumns) { patternColumn = 0; }


            patternColumn = patternColumn % config.boardColumns;


            if (patternColumn != patternPreviousColumn)
            {
                if (patternColumn == 0) { patternRepeats++; }

                for (int i = 0; i < config.boardRows; i++)
                {
                    int effect = (patternRepeats % config.patternsUntilShow == 0 ? pattern[i, patternColumn] : guess[i, patternColumn]);
                    if (effect >= 0)
                    {
                        SoundPool.PlaySound(pieceSoundEffects[effect], config.boardRowPitches[config.boardRows - 1 - i]);
                    }
                }

                patternPreviousColumn = patternColumn;
            }


            guessedCorrectlyPieces = CalculateCorrectGuesses(pattern, guess) - CalculateWrongGuesses(pattern, guess);
            if (guessedCorrectlyPieces < 0) { guessedCorrectlyPieces = 0; }



        }

        public static void PrintPattern(int[,] p)
        {
            Configuration config = Program.GetConfig();
            int[,] result = new int[config.boardRows, config.boardColumns];

            Console.Write("   ");
            for (int j = 0; j < config.boardColumns; j++)
            {
                Console.Write($"{j} ");
            }
            Console.WriteLine();

            for (int i = 0; i < config.boardRows; i++)
            {
                Console.Write($" {i}:");
                for (int j = 0; j < config.boardColumns; j++)
                {
                    if(p[i,j] >= 0)
                    {
                        Console.Write($"{p[i,j]} ");
                    }
                    else 
                    {
                        Console.Write(". ");
                    }
                }

                Console.WriteLine();
            }

        }
    }
}