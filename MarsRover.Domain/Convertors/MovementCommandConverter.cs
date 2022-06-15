using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Domain.Convertors
{
    public static class MovementCommandConverter
    {
        public static List<MovementEnum> ConvertCommands(string command)
        {
            var movements = new List<MovementEnum>();

            foreach (var move in command)
            {
                switch (move.ToString().ToUpper())
                {
                    case "F":
                    case "f":
                        movements.Add(MovementEnum.Forward);
                        break;
                    case "B":
                    case "b":
                        movements.Add(MovementEnum.Backward);
                        break;
                    case "R":
                    case "r":
                        movements.Add(MovementEnum.Right);
                        break;
                    case "L":
                    case "l":
                        movements.Add(MovementEnum.Left);
                        break;
                    default:
                        break;
                }
            }

            return movements;
        }
    }
}
