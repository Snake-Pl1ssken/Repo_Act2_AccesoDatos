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
            int b;

            FileStream fichero = new FileStream("datos.bin", FileMode.Open, FileAccess.Read);
            b = (int)fichero.ReadByte();
            fichero.Close();

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            byte textBoxes;

            FileStream fichero;

            fichero = new FileStream("datos.bin", FileMode.Create, FileAccess.Write);

            fichero.WriteByte(textBoxes);

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