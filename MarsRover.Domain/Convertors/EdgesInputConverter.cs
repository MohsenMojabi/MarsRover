using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Domain.Convertors
{
    public static class EdgesInputConverter
    {
        public static (int x, int y) ConvertToEdges(string initialPosition)
        {
            var sepratedChars = initialPosition.Split(' ');

            var x = Convert.ToInt32(sepratedChars[0]);

            var y = Convert.ToInt32(sepratedChars[1]);

            return (x, y);
        }
    }
}
