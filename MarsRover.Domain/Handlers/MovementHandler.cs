using MarsRover.Domain.Convertors;
using MarsRover.Domain.Repository.IRepository;
using MarsRover.Domain.Validators;
using MarsRover.Models.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarsRover.Domain.Handlers
{

    public class MovementHandlerRequest : IRequest<(int x, int y)>
    {
        public string Obstacles { get; set; }
        public string Edges { get; set; }
        public string InitialPosition { get; set; }
        public string Commands { get; set; }
    }

    public class MovementHandler : IRequestHandler<MovementHandlerRequest, (int x, int y)>
    {
        private readonly IMovementRepository _movementRepository;

        public MovementHandler(IMovementRepository movementRepository)
        {
            _movementRepository = movementRepository;
        }

        public async Task<(int x, int y)> Handle(MovementHandlerRequest request, CancellationToken cancellationToken)
        {
            var edgeValidated = EdgesInputValidator.Validate(request.Edges);
            var obstaclesValidated = ObstaclesInputValidator.Validate(request.Obstacles);
            var positionValidated = InitialPositionInputValidator.Validate(request.InitialPosition);
            var commandsValidated = MovementCommandValidator.Validate(request.Commands);

            (int x, int y) edges = EdgesInputConverter.ConvertToEdges(request.Edges);

            ((int x, int y) position, DirectionEnum direction) currentPosition = InitialPositionInputConverter.ConvertToInitialPosition(request.InitialPosition);

            _movementRepository.SetObstacles(request.Obstacles);
            _movementRepository.SetEdges(edges);
            _movementRepository.SetCurrentPosition(currentPosition.position);
            _movementRepository.SetCurrentDirection(currentPosition.direction);

            var commands = MovementCommandConverter.ConvertCommands(request.Commands);

            foreach (var command in commands)
            {
                switch (command)
                {
                    case MovementEnum.Forward:
                        _movementRepository.MoveForward();
                        break;
                    case MovementEnum.Backward:
                        _movementRepository.MoveBackward();
                        break;
                    case MovementEnum.Right:
                        _movementRepository.TurnRight();
                        break;
                    case MovementEnum.Left:
                        _movementRepository.TurnLeft();
                        break;
                    default:
                        break;
                }
            }

            return (_movementRepository.GetCurrentPosition());
        }

    }
}
