using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Domain.Convertors
{
    public static class InitialPositionInputConverter
    {
        public static ((int x, int y), DirectionEnum direction) ConvertToInitialPosition(string initialPosition)
        {
            var sepratedChars = initialPosition.Split(' ');

            var x = Convert.ToInt32(sepratedChars[0]);

            var y = Convert.ToInt32(sepratedChars[1]);

            var direction = DirectionEnum.North;
            switch (sepratedChars[2].ToUpper())
            {
                case "N":
                    direction = DirectionEnum.North;
                    break;
                case "S":
                    direction = DirectionEnum.South;
                    break;
                case "W":
                    direction = DirectionEnum.West;
                    break;
                case "E":
                    direction = DirectionEnum.East;
                    break;
                default:
                    break;
            };

            return ((x, y), direction);
        }
    }
}
