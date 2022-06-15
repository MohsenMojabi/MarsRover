using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarsRover.Domain.Validators
{
    public static class ObstacleDetectionValidator
    {
        public static bool Validate(string newPosition, string obstacles)
        {
            if (!String.IsNullOrEmpty(obstacles) && obstacles.Contains(newPosition))
            {
                    throw new InvalidOperationException($"Obstacle detected at {newPosition}");                  
            }

            return true;
        }
    }
}
