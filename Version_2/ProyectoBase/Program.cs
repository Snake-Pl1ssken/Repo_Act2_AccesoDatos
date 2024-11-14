using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace BasilioMixer
{
    internal class Program
    {
        // Common objects

        static Configuration configuration;
        static Random random;

        // Background

        static Sprite backgroundSprite;


        static void Main(string[] args)
        {
            // Init common objects

            random = new Random(DateTime.Now.Millisecond);

            ///////////////////////////////////////////////////////////////////
            configuration = new Configuration(); //AQUI HAY QUE CARGAR EL JSON
            ///////////////////////////////////////////////////////////////////
            ///
            //2
            // Si existe el fichero "configuration.json"
            // abres el fichero y metes su contenido en un string
            // llamas al jsonserializer para que deserialize el string en un objeto configuration
            // que asignas a la variable configuration

            // Init window

            var mode = new VideoMode((uint)configuration.windowWidth, (uint)configuration.windowHeight);
            var window = new RenderWindow(mode, configuration.windowTitle);

            window.MouseButtonPressed += OnMouseButtonPressed;
            window.KeyPressed += OnKeyPressed;

            // Init background

            backgroundSprite = new Sprite();
            backgroundSprite.Texture = new Texture(configuration.backgroundTexturePath);

            // Init sound pool

            SoundPool.Init();

            // Init board

            Board.Init();

            // Init basilio

            Basilio.Init();

            // Start the game loop
            while (window.IsOpen)
            {
                // Process events
                window.DispatchEvents();

                // Update everybody

                SoundPool.Update();

                Board.Update();

                Basilio.Update();

                // Draw everybody

                window.Draw(backgroundSprite);

                Board.Draw(window);

                Basilio.Draw(window);

                // Show drawing results

                window.Display();

            }
            //1
            //AQUI HAY QUE GUARDAR EL FICHERO JSON
            // usar el json serialized para convertir el objeto configuration en un string
            // posiblemente la primera vez te genere un string vacío, investiga jsonserializeroptions
            // el string lo guardas en un fichero de texto "configuration.json"


            //estructura json serialize options
            string jsonString = JsonSerializer.Serialize(configuration);
            Console.WriteLine(jsonString);

        }

        public static Random GetRandom()
        {
            return random;
        }

        public static Configuration GetConfig()
        {
            return configuration;
        }

        static void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            var window = (RenderWindow)sender;

            Board.OnBoardClick(window, Mouse.GetPosition(window), e.Button);

        }

        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            var window = (RenderWindow)sender;

            if(e.Code == Keyboard.Key.Escape)
            {
                window.Close();
            }
        }

    }

}

