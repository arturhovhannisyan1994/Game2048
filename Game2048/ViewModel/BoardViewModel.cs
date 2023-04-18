using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using Game2048.Domain;
using CommunityToolkit.Mvvm.Input;

namespace Game2048.ViewModel
{
    public class BoardViewModel : ObservableObject, IDrawable
    {
        public IAsyncRelayCommand SlidCommand { get; set; }

        public const int Size = 4;

        public const int CellSize = 100;

        public int BoardSize => Size * (int)CellSize;

        public int[,] Matrix = new int[Size, Size];

        private readonly NumberGenerator _numberGenerator = new NumberGenerator();

        private TaskCompletionSource _completionSource;

        public BoardViewModel()
        {
           
            //SlidCommand = new AsyncRelayCommand()

            //CellSize = 40;
        }


        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FontSize = 30;
            DrawEmptyBoard(canvas);
            PopulateNewNumber();
            PopulateNewNumber();
            DrawBasedOnMatrix(canvas);
            _completionSource.SetResult();
        }





        public void PopulateNewNumber()
        {
            int x = 0;
            int y = 0;

            do
            {
                x = _numberGenerator.GetRandomPosition(Size);
                y = _numberGenerator.GetRandomPosition(Size);

            } while (Matrix[x, y] != 0);

            Matrix[x, y] = _numberGenerator.GetRandomValue();

        }

        public void DrawBasedOnPosition(ICanvas canvas, int x, int y, int value)
        {
            canvas.DrawString(value.ToString(), x * CellSize, y * CellSize, CellSize, CellSize, HorizontalAlignment.Center, VerticalAlignment.Center, TextFlow.OverflowBounds);

        }



        public void DrawBasedOnMatrix(ICanvas canvas)
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    if (Matrix[i, j] != 0)
                        DrawBasedOnPosition(canvas, i, j, Matrix[i, j]);

        }

        public void DrawNumberOnBoard(ICanvas canvas, int x, int y, int value)
        {
        }


        public void DrawEmptyBoard(ICanvas canvas)
        {

            int x, y = 0;
            // canvas.StrokeSize = 4;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    x = i * CellSize;
                    y = (j * CellSize);
                    Debug.WriteLine($"postion x:{x} y:{y}");

                    canvas.DrawRectangle(x, y, CellSize, CellSize);

                }

            }


        }


        public bool Invalidate()
        {
            return true;
        }
    }
}
