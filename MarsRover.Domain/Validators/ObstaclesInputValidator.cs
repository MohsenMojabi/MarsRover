using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarsRover.Domain.Validators
{
    public static class ObstaclesInputValidator
    {
        public static bool Validate(string obstacles)
        {

            string edgesPattern = @"^(\(\d+\s\d\))+$";

            if (!Regex.IsMatch(obstacles, edgesPattern) && obstacles.Trim().Length > 0)
                throw new ArgumentException(
                    nameof(obstacles),
                    "Insufficient arguments provided. " +
                    "Syntax is (x1<whitespace>y1)(x2<whitespace>y2)... and only possitive numbers like '(3 2)(6 7)' or '(4 6)(7 8)(9 0)'");

            return true;
        }
    }
}
