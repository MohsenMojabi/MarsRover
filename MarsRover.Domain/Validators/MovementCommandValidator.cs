using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarsRover.Domain.Validators
{
    public static class MovementCommandValidator
    {
        public static bool Validate(string command)
        {
            if (String.IsNullOrEmpty(command))
            {
                    throw new ArgumentNullException(
                        nameof(command),
                        "Please provide commands to control the rover.");                  
            }

            string movementCommandPattern = @"^(L|l|R|r|F|f|B|b)+$";

            if (!Regex.IsMatch(command, movementCommandPattern))
                throw new ArgumentException(
                    nameof(command),
                    "Insufficient arguments provided for movement commands. " +
                    "Syntax is L|l|R|r|F|f|B|b like 'lfffRb' or 'flrrBllFfB'");

            return true;
        }
    }
}
