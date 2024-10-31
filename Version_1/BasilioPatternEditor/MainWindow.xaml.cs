using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasilioPatternEditor
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBox[,] textBoxes;

        public MainWindow()
        {
            InitializeComponent();

            // Create the Canvas

            textBoxes = new TextBox[8,8];

            for(int i = 0; i < 8; i ++)
            {
                for (int j = 0; j < 8; j++)
                {
                    TextBox t = new TextBox();
                    t.Text = "-1";

                    t.SetValue(Canvas.LeftProperty, j * 39.0);
                    t.SetValue(Canvas.TopProperty, i * 39.0);
                    t.SetValue(Canvas.WidthProperty, 39.0);
                    t.SetValue(Canvas.HeightProperty, 39.0);

                    Canvas.Children.Add(t);
                    textBoxes[i,j] = t;


                }
            }


        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    TextBox t = new TextBox();
                    t.Text = "-1";

                    t.SetValue(Canvas.LeftProperty, j * 39.0);
                    t.SetValue(Canvas.TopProperty, i * 39.0);
                    t.SetValue(Canvas.WidthProperty, 39.0);
                    t.SetValue(Canvas.HeightProperty, 39.0);

                    Canvas.Children.Add(t);
                    textBoxes[i, j] = t;


                }
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            FileStream fichero = new FileStream("datos.bin", FileMode.Open, FileAccess.Read); 
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // 1 .- reservar un array de bytes del tamaño de un int -DONE

                    // 2.- leer del fichero y dejar lo leido en el array de bytes

                    // 3.- convertir el array de bytes en un entero

                    // 4.- convertir el entero en un texto

                    // 5 .- guardar el texto en el TexBox correspondiente

                    ///////////////1/////////////////
                    int tamanyo = sizeof(int);
                    byte[] bytes = new byte[tamanyo];
                    ///////////////1/////////////////


                    ///////////////2///////////////
                    fichero.Read(bytes, 0, tamanyo);
                    ///////////////2///////////////


                    ///////////////3///////////////
                    int ArrayAInt;
                    ArrayAInt = BitConverter.ToInt32(bytes);
                    ///////////////3///////////////


                    ///////////////4///////////////
                    string TextoAEntero;
                    TextoAEntero = ArrayAInt.ToString();
                    ///////////////4///////////////


                    ///////////////5///////////////
                    //int numguard;
                    textBoxes[i, j].Text = TextoAEntero;
                    ///////////////5///////////////
                }
            }
            fichero.Close();

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            FileStream fichero = new FileStream("datos.bin", FileMode.Create, FileAccess.Write);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                { 
                    int numguard;
                    numguard = Int32.Parse(textBoxes[i,j].Text);
                    byte[] textBoxeGuardado = BitConverter.GetBytes(numguard); 

                    fichero.Write(textBoxeGuardado, 0 , textBoxeGuardado.Length);
                    //Se puede un for multiple? NO Y SI SE HACE, EL FOR
                }
            }
            fichero.Close();
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}