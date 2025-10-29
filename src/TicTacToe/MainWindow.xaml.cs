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

namespace TicTacToe
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public Piece[,] Pieces = new Piece[3,3];
        private void ClearPieces()
        {
            var buttons = GetButtons();
            foreach(var btn in buttons)
            {
                btn.Piece = Piece.None;
            }
        }
        private List<GameButton> GetButtons()
        {
            var child = BtnGird.Children.Cast<UIElement>().Where(x => x.GetType() == typeof(GameButton)).Select(x => x as GameButton).ToList();
            return child;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(e.Source is GameButton btn)
            {
                //btn.Style = (Style)FindResource("CircleButton");
                btn.Piece = Piece.Times;
            }
            var restBtns = GetButtons().Where(x => x.Piece == Piece.None).ToArray();
            if(restBtns.Count() > 0)
            {
                int index = new Random().Next(0, restBtns.Count());
                restBtns[index].Piece = Piece.Circle;
            }
            Piece winPiece = (Piece)Tic_Tac_Toe();
            switch (winPiece)
            {
                case Piece.Times:MessageBtn.Text = "你赢了!";break;
                case Piece.Circle:MessageBtn.Text = "你输了!";break;
            }
            if(restBtns.Count() == 0 && winPiece == Piece.None)
            {
                MessageBtn.Text = "平局";
            }
        }

        private void ReSet_Click(object sender, RoutedEventArgs e)
        {
            MessageBtn.Text = "";
            ClearPieces();
        }
        public void Async(Action action)
        {

        }
        public int Tic_Tac_Toe()
        {
            var matrix = new int[3, 3];
            foreach(var btn in GetButtons())
            {
                int tag = Convert.ToInt32(btn.Tag);
                matrix[tag / 3, tag % 3] = (int)btn.Piece;
            }
            int result = 0;
            for (int i = 0; i < 3; i++)
            {
                result = WinNumber(matrix[i, 0], matrix[i, 1], matrix[i, 2]);
                if (result != 0) return result;
            }
            for (int j = 0; j < 3; j++)
            {
                result = WinNumber(matrix[0, j], matrix[1, j], matrix[2, j]);
                if (result != 0) return result;
            }
            result = WinNumber(matrix[0, 0], matrix[1, 1], matrix[2, 2]);
            if (result != 0) return result;
            result = WinNumber(matrix[2, 0], matrix[1, 1], matrix[0, 2]);
            if (result != 0) return result;
            return result;
        }
        public static int WinNumber(int a, int b, int c)
        {
            if (a == b && b == c)
            {
                return a;
            }
            else
            {
                return 0;
            }
        }
    }
}
