using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarsRover.Domain.Validators
{
    public static class InitialPositionInputValidator
    {
        public static bool Validate(string initialPosition)
        {
            if (String.IsNullOrEmpty(initialPosition))
            {
                    throw new ArgumentNullException(
                        nameof(initialPosition),
                        "Please provide initial position to control the rover.");                  
            }

            string initialPositionPattern = @"^\d+\s\d+\s(N|n|E|e|W|w|S|s){1}$";

            if (!Regex.IsMatch(initialPosition, initialPositionPattern))
                throw new ArgumentException(
                    nameof(initialPosition),
                    "Insufficient arguments provided. " +
                    "Syntax is x<whitespace>y<whitespace>direction and only possitive numbers like '3 2 N' or '4 6 W'");

            return true;
        }
    }
}
