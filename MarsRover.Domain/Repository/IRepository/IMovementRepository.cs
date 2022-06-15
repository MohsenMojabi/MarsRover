using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Domain.Repository.IRepository
{
    public interface IMovementRepository
    {
        (int x, int y) GetCurrentPosition();
        DirectionEnum GetCurrentDirection();
        void SetObstacles(string obstaclesInput);
        void SetEdges((int x, int y) edgesInput);
        void SetCurrentPosition((int x, int y) position);
        void SetCurrentDirection(DirectionEnum direction);
        void MoveForward();
        void MoveBackward();
        void TurnRight();
        void TurnLeft();
    }
}
