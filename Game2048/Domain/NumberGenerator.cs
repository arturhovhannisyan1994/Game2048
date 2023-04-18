using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Domain
{
    public class NumberGenerator
    {
        private readonly Random _random = new Random();

        private const int _singleValue = 2;
        public int GetRandomValue()
        {
            int x = _random.Next(1, 3);

            return x * _singleValue;



        }

        public int GetRandomPosition(int max)
        {
            return _random.Next(0,max);
        }
    }



    public class BoardPosition
    {
        public int X { get; set; }

        public int Y { get; set; }
    }
}
