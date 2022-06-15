using MarsRover.Domain.Repository.IRepository;
using MarsRover.Domain.Validators;
using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Domain.Repository
{
    public class MovementRepository : IMovementRepository
    {
        private (int x, int y) edges;
        private string obstacles = "";
        private (int x, int y) currentPosition;
        private DirectionEnum currentDirection;

        public DirectionEnum GetCurrentDirection()
        {
            return currentDirection;
        }

        public (int x, int y) GetCurrentPosition()
        {
            return currentPosition;
        }

        public void SetObstacles(string obstaclesInput)
        {
            obstacles = obstaclesInput;
        }

        public void SetEdges((int x, int y) edgesInput)
        {
            edges = edgesInput;
        }

        public void SetCurrentDirection(DirectionEnum direction)
        {
            currentDirection = direction;
        }

        public void SetCurrentPosition((int x, int y) position)
        {
            currentPosition = position;
        }

        public void MoveForward()
        {
            var x = currentPosition.x;
            var y = currentPosition.y;

            switch (currentDirection)
            {
                case DirectionEnum.North:
                    //currentPosition.y = (currentPosition.y + 1) % edges.y;
                    y = (y + 1) % edges.y;
                    break;
                case DirectionEnum.South:
                    //currentPosition.y = currentPosition.y > 1 ? currentPosition.y - 1 : edges.y;
                    y = y > 1 ? y - 1 : edges.y;
                    break;
                case DirectionEnum.West:
                    //currentPosition.x = currentPosition.x > 1 ? currentPosition.x - 1 : edges.x;
                    x = x > 1 ? x - 1 : edges.x;
                    break;
                case DirectionEnum.East:
                    //currentPosition.x = (currentPosition.x + 1) % edges.x;
                    x = (x + 1) % edges.x;
                    break;
                default:
                    break;
            }

            ObstacleDetectionValidator.Validate($"({x} {y})", obstacles);

            currentPosition.x = x;
            currentPosition.y = y;
        }

        public void MoveBackward()
        {
            var x = currentPosition.x;
            var y = currentPosition.y;

            switch (currentDirection)
            {
                case DirectionEnum.North:
                    //currentPosition.y = currentPosition.y > 1 ? currentPosition.y - 1 : edges.y;
                    y = y > 1 ? y - 1 : edges.y;
                    break;
                case DirectionEnum.South:
                    //currentPosition.y = (currentPosition.y + 1) % edges.y;
                    y = (y + 1) % edges.y;
                    break;
                case DirectionEnum.West:
                    //currentPosition.x = (currentPosition.x + 1) % edges.x;
                    x = (x + 1) % edges.x;
                    break;
                case DirectionEnum.East:
                    //currentPosition.x = currentPosition.x > 1 ? currentPosition.x - 1 : edges.x;
                    x = x > 1 ? x - 1 : edges.x;
                    break;
                default:
                    break;
            }

            ObstacleDetectionValidator.Validate($"({x} {y})", obstacles);

            currentPosition.x = x;
            currentPosition.y = y;
        }

        public void TurnLeft()
        {
            switch (currentDirection)
            {
                case DirectionEnum.North:
                    currentDirection = DirectionEnum.West;
                    break;
                case DirectionEnum.South:
                    currentDirection = DirectionEnum.East;
                    break;
                case DirectionEnum.West:
                    currentDirection = DirectionEnum.South;
                    break;
                case DirectionEnum.East:
                    currentDirection = DirectionEnum.North;
                    break;
                default:
                    break;
            }
        }

        public void TurnRight()
        {
            switch (currentDirection)
            {
                case DirectionEnum.North:
                    currentDirection = DirectionEnum.East;
                    break;
                case DirectionEnum.South:
                    currentDirection = DirectionEnum.West;
                    break;
                case DirectionEnum.West:
                    currentDirection = DirectionEnum.North;
                    break;
                case DirectionEnum.East:
                    currentDirection = DirectionEnum.South;
                    break;
                default:
                    break;
            }
        }
    }
}
