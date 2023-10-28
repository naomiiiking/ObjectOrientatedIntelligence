using System;

namespace Assignment2
{
	public class ElectricFence : Obstacle
    {
        private List<Point> Effects = new List<Point>();
        private char direction;
        private float axis;

        public ElectricFence(char Direction, float Axis)
		{
            direction = Direction;
            axis = Axis;


        }

        private bool checkFence(int x, int y)
        {
            if(direction == 'v')
            {
                if( (x == axis && isEven(y)) || (x == (axis+1) && !isEven(y)) ) { return true; }
            }
            if (direction == 'h')
            {
                if (y == axis && isEven(x) || (y == (axis + 1) && !isEven(x)) ) { return true; }
            }
            return false;
        }

        private bool isEven(int num)
        {
            if (num % 2 == 0){ return true; }
            return false;
        }
        public override List<Point> effects
        {
            get { return Effects; }
            set { }
        }

        public override char symbol
        {
            get
            {
                return 'e';
            }
            set { }
        }

        public override bool obeserve(int x, int y)
        {
            return checkFence(x, y);
        }
    }
}

