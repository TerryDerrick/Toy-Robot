using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Toy_Robot
{
    public class ToyRobot
    {
        public ToyRobot(string x, string y, string direction)
        {
            InitialPlaceValidation(x, y, direction);
        }

        public int CurrentXPosition { get; set; }
        public int CurrentYPosition { get; set; }
        public DirectionEnum CurrentDirection { get; set; }

        public enum DirectionEnum
        {
            NORTH = 0,
            EAST = 90,
            SOUTH = 180,
            WEST = 270,
        }

        private void InitialPlaceValidation(string x, string y, string direction)
        {
            try
            {
                var Xposition = int.Parse(x);
                var Yposition = int.Parse(y);
                DirectionEnum Direction = Enum.Parse<DirectionEnum>(direction);

                if ((Xposition > 5 || Xposition < 0) || (Yposition > 5 || Yposition < 0))
                {
                    throw new Exception("Initial Placement Position is not on the table");
                }
                else
                {
                    CurrentXPosition = Xposition;
                    CurrentYPosition = Yposition;
                    CurrentDirection = Direction;
                }
            }
            catch (Exception)
            {
                throw;
            } 
        }

        public void MoveRobot()
        {
            var newXposition = CurrentXPosition;
            var newYposition = CurrentYPosition;

            switch (CurrentDirection)
            {
                case DirectionEnum.NORTH:
                    newYposition += 1;
                    break;
                case DirectionEnum.EAST:
                    newXposition += 1;
                    break;
                case DirectionEnum.SOUTH:
                    newYposition += -1;
                    break;
                case DirectionEnum.WEST:
                    newXposition += -1;
                    break;

            }

            if ((newXposition > 5 || newXposition < 0) || (newYposition > 5 || newYposition < 0))
            {
                throw new Exception("Invalid MOVE command, Toy Robot would fall of Table and be destroyed");
            }
            else
            {
                CurrentXPosition = newXposition;
                CurrentYPosition = newYposition;
            }

        }

        public void RotateRobot(string rotationDirection)
        {
            var adjustmentValue = 0;
            if (rotationDirection.Equals("LEFT", StringComparison.InvariantCultureIgnoreCase))
            {
                adjustmentValue = -90;
            }
            else if (rotationDirection.Equals("RIGHT", StringComparison.InvariantCultureIgnoreCase))
            {
                adjustmentValue = 90;
            }

            var currentDirectionValue = (int)CurrentDirection;
            var newDirection = currentDirectionValue + adjustmentValue;
            if (newDirection >= 360)
                newDirection -= 360;

            if (newDirection < 0)
                newDirection += 360;

            CurrentDirection = (DirectionEnum)newDirection;
        }

        public string Report()
        {
            return $"Output: {CurrentXPosition},{CurrentYPosition},{Enum.GetName(CurrentDirection)}";
        }


    }
}
