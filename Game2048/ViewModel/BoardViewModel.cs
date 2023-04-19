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
using Microsoft.Maui.Graphics;


namespace Game2048.ViewModel
{
    public class BoardViewModel : ObservableObject, IDrawable
    {
        private PointF _firstPoint;
        private PointF _lastPoint;

        public void StartInteraction(object sender, TouchEventArgs e)
        {
            _firstPoint = e.Touches.FirstOrDefault();
            // string msg = $"Touch/click at {firstPoint}";

        }
        public bool EndInteraction(object sender, TouchEventArgs e, ICanvas canvas)
        {

            _lastPoint = e.Touches.LastOrDefault();
            return CalculateMovementDirection() != MovementDirection.None;


        }


        //public IAsyncRelayCommand EndInteraction { get; set; }

        public const int Size = 4;

        public const int CellSize = 100;

        public const int Margin = 10;

        public int BoardSize => Size * (int)CellSize;

        public static int[,] Matrix = new int[Size, Size];

        private readonly NumberGenerator _numberGenerator = new NumberGenerator();

        // private TaskCompletionSource _completionSource;

        public BoardViewModel()
        {

            //SlidCommand = new AsyncRelayCommand()

            //CellSize = 40;
        }

        public MovementDirection CalculateMovementDirection()
        {
            MovementDirection direction = MovementDirection.None;
            int diff = CellSize / 4;
            if (_firstPoint.X - _lastPoint.X > diff)
            {
                direction = MovementDirection.Left;
                //MoveLeft();

            }
            else if (_lastPoint.X - _firstPoint.X > diff)
            {
                direction = MovementDirection.Right;
                //MoveToRight();
            }
            else if (_lastPoint.Y - _firstPoint.Y > diff)
            {
                direction = MovementDirection.Bottom;
                //MoveBottom();
            }
            else if (_firstPoint.Y - _lastPoint.Y > diff)
            {
                direction = MovementDirection.Top;
                //MoveTop();
            }

            Move(direction);

            return direction;

        }


        public void Move(MovementDirection direction)
        {
            for (int i = 0; i < Size; i++)
            {
                if (direction == MovementDirection.Left)
                    MoveLeft();
                if (direction == MovementDirection.Right)
                    MoveToRight();
                if (direction == MovementDirection.Top)
                    MoveTop();
                if (direction == MovementDirection.Bottom)
                    MoveBottom();
            }
        }

        public void MoveToRight()
        {
            int value = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Matrix[i, j] != 0)
                    {
                        value = Matrix[i, j];

                        if (i + 1 < Size)
                        {
                            if (Matrix[i + 1, j] == 0)
                            {
                                Matrix[i + 1, j] = value;
                                Matrix[i, j] = 0;

                            }
                            else if (Matrix[i + 1, j] == Matrix[i, j])
                            {
                                Matrix[i + 1, j] = value * 2;
                                Matrix[i, j] = 0;
                            }
                        }
                        else
                        {
                            continue;
                        }



                    }
                }
            }
        }

        public void MoveTop()
        {
            int value = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = Size - 1; j > -1; j--)
                {
                    if (Matrix[i, j] != 0)
                    {
                        value = Matrix[i, j];

                        if (j - 1 > -1)
                        {
                            if (Matrix[i, j - 1] == 0)
                            {
                                Matrix[i, j - 1] = value;
                                Matrix[i, j] = 0;

                            }
                            else if (Matrix[i, j - 1] == Matrix[i, j])
                            {
                                Matrix[i, j - 1] = value * 2;
                                Matrix[i, j] = 0;
                            }
                        }
                        else
                        {
                            continue;
                        }

                    }
                }

            }
        }

        public void MoveBottom()
        {
            int value = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Matrix[i, j] != 0)
                    {
                        value = Matrix[i, j];

                        if (j + 1 < Size)
                        {
                            if (Matrix[i, j + 1] == 0)
                            {
                                Matrix[i, j + 1] = value;
                                Matrix[i, j] = 0;

                            }
                            else if (Matrix[i, j + 1] == Matrix[i, j])
                            {
                                Matrix[i, j + 1] = value * 2;
                                Matrix[i, j] = 0;
                            }
                        }
                        else
                        {
                            continue;
                        }



                    }
                }
            }
        }

        public void MoveLeft()
        {

            int value = 0;
            for (int i = Size - 1; i > -1; i--)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Matrix[i, j] != 0)
                    {
                        value = Matrix[i, j];

                        if (i - 1 > -1)
                        {
                            if (Matrix[i - 1, j] == 0)
                            {
                                Matrix[i - 1, j] = value;
                                Matrix[i, j] = 0;

                            }
                            else if (Matrix[i - 1, j] == Matrix[i, j])
                            {
                                Matrix[i - 1, j] = value * 2;
                                Matrix[i, j] = 0;
                            }
                        }
                        else
                        {
                            continue;
                        }



                    }
                }
            }




        }




        public void Draw(ICanvas canvas, RectF dirtyRect)
        {

            canvas.FontSize = 30;
            DrawEmptyBoard(canvas);

            if (IsEmpty())
                PopulateNewNumber();

            PopulateNewNumber();
            DrawBasedOnMatrix(canvas);
            //   _completionSource.SetResult();
        }

        public bool IsEmpty()
        {
            bool exists = false;
            for (int i = 0; i < Size; i++)
            {
                if (exists)
                {
                    break;
                }
                for (int j = 0; j < Size; j++)
                {
                    if (Matrix[i, j] != 0)
                    {
                        exists = true;
                        break;
                    }
                }
            }
            return !exists;

        }

        public bool IsSpace()
        {
            for (int i = 0; i < Size; i++)
            {

                for (int j = 0; j < Size; j++)
                {
                    if (Matrix[i, j] == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }





        public void PopulateNewNumber()
        {
            int x = 0;
            int y = 0;
            if (!IsSpace())
            {

                return;
            }
            do
            {
                x = _numberGenerator.GetRandomPosition(Size);
                y = _numberGenerator.GetRandomPosition(Size);

            } while (Matrix[x, y] != 0);

            Matrix[x, y] = _numberGenerator.GetRandomValue();

        }

        public void DrawBasedOnPosition(ICanvas canvas, int x, int y, int value)
        {
            SolidPaint solidPaint = new SolidPaint(Colors.Silver);

            if (value == 2)
            {
                solidPaint = new SolidPaint(new Color(204, 193, 181));
            }
            if (value == 4)
            {
                solidPaint = new SolidPaint(new Color(238, 223, 199));
            }
            if (value == 8)
            {
                solidPaint = new SolidPaint(new Color(241, 178, 121));
            }
            if (value == 16)
            {
                solidPaint = new SolidPaint(new Color(246, 150, 98));
            }
            if (value == 32)
            {
                solidPaint = new SolidPaint(new Color(245, 124, 94));
            }
            if (value > 32) 
            {
                solidPaint = new SolidPaint(new Color(238, 208, 113));
            }

            
            RectF solidRectangle = new RectF(x * CellSize + Margin, y * CellSize + Margin, CellSize - Margin, CellSize - Margin);
            canvas.SetFillPaint(solidPaint, solidRectangle);
            // canvas.SetShadow(new SizeF(10, 10), 10, Colors.Grey);
            canvas.FillRoundedRectangle(solidRectangle, 12);
            if (value != 0)
                canvas.DrawString(value.ToString(), x * CellSize + Margin, y * CellSize + Margin, CellSize - Margin, CellSize - Margin, HorizontalAlignment.Center, VerticalAlignment.Center, TextFlow.OverflowBounds);

        }



        public void DrawBasedOnMatrix(ICanvas canvas)
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    DrawBasedOnPosition(canvas, i, j, Matrix[i, j]);

        }

        public void DrawNumberOnBoard(ICanvas canvas, int x, int y, int value)
        {
        }


        public void DrawEmptyBoard(ICanvas canvas)
        {
            //(187,173,161)
            var solidPaint = new SolidPaint(new Color(187, 173, 161));
            RectF solidRectangle = new RectF(0, 0, CellSize * Size +( Margin * 2), CellSize * Size + (Margin* 2));
            canvas.SetFillPaint(solidPaint, solidRectangle);

            canvas.FillRectangle(solidRectangle);
            return;
            int x, y = 0;
            // canvas.StrokeSize = 4;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    x = i * CellSize;
                    y = (j * CellSize);




                }

            }


        }


        public bool Invalidate()
        {
            return true;
        }
    }

    public enum MovementDirection
    {
        None,
        Left,
        Right,
        Top,
        Bottom,
    }
}
