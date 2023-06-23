using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _5The15FormWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Button> tiles = new List<Button>();
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            InitializeTilesList();
            ShuffleTiles();
        }

        private void InitializeTilesList()
        {
            tiles.Add(tile1);
            tiles.Add(tile2);
            tiles.Add(tile3);
            tiles.Add(tile4);
            tiles.Add(tile5);
            tiles.Add(tile6);
            tiles.Add(tile7);
            tiles.Add(tile8);
            tiles.Add(tile9);
            tiles.Add(tile10);
            tiles.Add(tile11);
            tiles.Add(tile12);
            tiles.Add(tile13);
            tiles.Add(tile14);
            tiles.Add(tile15);
            tiles.Add(emptyTile);
        }

        private void ShuffleTiles()
        {
            int[] numbers = Enumerable.Range(1, 15).ToArray();
            int index = numbers.Length - 1;

            for (int i = 0; i < numbers.Length; i++)
            {
                int randomIndex = random.Next(i, numbers.Length);
                int temp = numbers[randomIndex];
                numbers[randomIndex] = numbers[i];
                numbers[i] = temp;
            }

            foreach (Button tile in tiles)
            {
                if (tile.Name != "emptyTile")
                {
                    tile.Content = numbers[index].ToString();
                    index--;
                }
            }
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            Button tile = sender as Button;
            int tileNumber = int.Parse(tile.Content.ToString());

            if (IsAdjacentEmpty(tile))
            {
                SwapTiles(tile, emptyTile);
                if (IsPuzzleSolved())
                {
                    MessageBox.Show("You won!");
                    ShuffleTiles();
                }
            }
        }

        private bool IsAdjacentEmpty(Button tile)
        {
            int tileRow = Grid.GetRow(tile);
            int tileColumn = Grid.GetColumn(tile);
            int emptyRow = Grid.GetRow(emptyTile);
            int emptyColumn = Grid.GetColumn(emptyTile);

            if (tileRow == emptyRow && Math.Abs(tileColumn - emptyColumn) == 1)
            {
                return true;
            }
            else if (tileColumn == emptyColumn && Math.Abs(tileRow - emptyRow) == 1)
            {
                return true;
            }

            return false;
        }

        private void SwapTiles(Button tile1, Button tile2)
        {
            int tile1Row = Grid.GetRow(tile1);
            int tile1Column = Grid.GetColumn(tile1);
            int tile2Row = Grid.GetRow(tile2);
            int tile2Column = Grid.GetColumn(tile2);

            Grid.SetRow(tile1, tile2Row);
            Grid.SetColumn(tile1, tile2Column);
            Grid.SetRow(tile2, tile1Row);
            Grid.SetColumn(tile2, tile1Column);
        }

        private bool IsPuzzleSolved()
        {
            int tileNumber = 1;

            foreach (Button tile in tiles)
            {
                if (tile.Name != "emptyTile")
                {
                    int contentNumber = int.Parse(tile.Content.ToString());
                    if (contentNumber != tileNumber)
                    {
                        return false;
                    }
                    tileNumber++;
                }
            }

            return true;
        }

        private void Shuffle_Click(object sender, RoutedEventArgs e)
        {
            ShuffleTiles();
        }
    }

}
