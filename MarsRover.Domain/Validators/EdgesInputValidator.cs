using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarsRover.Domain.Validators
{
    public static class EdgesInputValidator
    {
        public static bool Validate(string edges)
        {
            if (String.IsNullOrEmpty(edges))
            {
                    throw new ArgumentNullException(
                        nameof(edges),
                        "Please provide edges to control the rover.");                  
            }

            string edgesPattern = @"^\d+\s\d+$";

            if (!Regex.IsMatch(edges, edgesPattern))
                throw new ArgumentException(
                    nameof(edges),
                    "Insufficient arguments provided. " +
                    "Syntax is x<whitespace>y<whitespace> and only possitive numbers like '3 2' or '4 6'");

            return true;
        }
    }
}
