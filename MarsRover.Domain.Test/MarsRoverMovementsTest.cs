using MarsRover.Domain.Convertors;
using MarsRover.Domain.Handlers;
using MarsRover.Domain.Repository;
using MarsRover.Domain.Repository.IRepository;
using MarsRover.Domain.Validators;
using MarsRover.Models.Enums;
using System;
using System.Threading;
using Xunit;

namespace MarsRover.Domain.Test
{
    public class MarsRoverMovementsTest
    {
        private readonly MovementRepository _movementRepository;

        public MarsRoverMovementsTest()
        {
            _movementRepository = new MovementRepository();
            _movementRepository.SetCurrentPosition((-4, 6));
            _movementRepository.SetCurrentDirection(DirectionEnum.North);
        }


        [Fact]
        public void Should_Throw_Exception_When_Initial_Position_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
                InitialPositionInputValidator.Validate(null));
        }

        [Theory]
        [InlineData("1 2")]
        [InlineData("1 2 North")]
        [InlineData("1 2 X")]
        public void Should_Throw_Exception_When_Initial_Position_Is_Not_In_Correct_Format(string initialPosition)
        {
            Assert.Throws<ArgumentException>(() =>
                InitialPositionInputValidator.Validate(initialPosition));
        }

        [Theory]
        [InlineData("1 2 N")]
        [InlineData("1 2 n")]
        public void Should_Return_Initial_Position_For_Correct_Input_Format(string initialPosition)
        {
            ((int x, int y) initPostition, DirectionEnum direction) position = InitialPositionInputConverter.ConvertToInitialPosition(initialPosition);

            Assert.Equal(1, position.initPostition.x);
            Assert.Equal(2, position.initPostition.y);
            Assert.Equal(DirectionEnum.North, position.direction);
        }

        [Fact]
        public void Should_Throw_Exception_When_Movement_Command_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
                MovementCommandValidator.Validate(null));
        }

        [Theory]
        [InlineData("1lR")]
        [InlineData("XCFFF")]
        [InlineData("LBbbY")]
        public void Should_Throw_Exception_When_Movement_Command_Is_Not_In_Correct_Format(string command)
        {
            Assert.Throws<ArgumentException>(() =>
                MovementCommandValidator.Validate(command));
        }

        [Theory]
        [InlineData("lBbFfR")]
        [InlineData("BbFFrRRlb")]
        public void Should_Return_Movements_Commands_For_Correct_Input_Format(string commands)
        {
            var movements = MovementCommandConverter.ConvertCommands(commands);

            Assert.Equal(MovementEnum.Forward, movements[3]);
            Assert.Equal(MovementEnum.Right, movements[5]);
        }

        [Theory]
        [InlineData(5, 5, 2, 3, DirectionEnum.North, 2, 4)]
        [InlineData(5, 5, 2, 4, DirectionEnum.North, 2, 0)]
        [InlineData(5, 5, 2, 3, DirectionEnum.South, 2, 2)]
        [InlineData(5, 5, 2, 1, DirectionEnum.South, 2, 5)]
        [InlineData(5, 5, 2, 3, DirectionEnum.West, 1, 3)]
        [InlineData(5, 5, 1, 3, DirectionEnum.West, 5, 3)]
        [InlineData(5, 5, 2, 3, DirectionEnum.East, 3, 3)]
        [InlineData(5, 5, 4, 3, DirectionEnum.East, 0, 3)]
        public void Should_Be_Able_To_Move_Forward(int width, int height, int x, int y, DirectionEnum direction, int expectedX, int expectedY) 
        {
            _movementRepository.SetEdges((width, height));
            _movementRepository.SetCurrentPosition((x, y));
            _movementRepository.SetCurrentDirection(direction);

            _movementRepository.MoveForward();

            var currentPosition = _movementRepository.GetCurrentPosition();
            var currentDirection = _movementRepository.GetCurrentDirection();

            Assert.Equal(direction, currentDirection);
            Assert.Equal(expectedX, currentPosition.x);
            Assert.Equal(expectedY, currentPosition.y);
        }

        [Theory]
        [InlineData(5, 5, 2, 3, DirectionEnum.North, 2, 2)]
        [InlineData(5, 5, 2, 1, DirectionEnum.North, 2, 5)]
        [InlineData(5, 5, 2, 3, DirectionEnum.South, 2, 4)]
        [InlineData(5, 5, 2, 4, DirectionEnum.South, 2, 0)]
        [InlineData(5, 5, 2, 3, DirectionEnum.West, 3, 3)]
        [InlineData(5, 5, 4, 3, DirectionEnum.West, 0, 3)]
        [InlineData(5, 5, 2, 3, DirectionEnum.East, 1, 3)]
        [InlineData(5, 5, 1, 3, DirectionEnum.East, 5, 3)]
        public void Should_Be_Able_To_Move_Backward(int width, int height, int x, int y, DirectionEnum direction, int expectedX, int expectedY)
        {
            _movementRepository.SetEdges((width, height));
            _movementRepository.SetCurrentPosition((x, y));
            _movementRepository.SetCurrentDirection(direction);

            _movementRepository.MoveBackward();

            var currentPosition = _movementRepository.GetCurrentPosition();
            var currentDirection = _movementRepository.GetCurrentDirection();

            Assert.Equal(direction, currentDirection);
            Assert.Equal(expectedX, currentPosition.x);
            Assert.Equal(expectedY, currentPosition.y);
        }

        [Theory]
        [InlineData(DirectionEnum.North, DirectionEnum.East)]
        [InlineData(DirectionEnum.South, DirectionEnum.West)]
        [InlineData(DirectionEnum.West, DirectionEnum.North)]
        [InlineData(DirectionEnum.East, DirectionEnum.South)]
        public void Should_Be_Able_To_Turn_Right_Correctly(DirectionEnum direction, DirectionEnum expectedDirection)
        {

            _movementRepository.SetCurrentDirection(direction);

            _movementRepository.TurnRight();

            var currentDirection = _movementRepository.GetCurrentDirection();

            Assert.Equal(expectedDirection, currentDirection);
        }

        [Theory]
        [InlineData(DirectionEnum.North, DirectionEnum.West)]
        [InlineData(DirectionEnum.South, DirectionEnum.East)]
        [InlineData(DirectionEnum.West, DirectionEnum.South)]
        [InlineData(DirectionEnum.East, DirectionEnum.North)]
        public void Should_Be_Able_To_Turn_Left(DirectionEnum direction, DirectionEnum expectedDirection)
        {

            _movementRepository.SetCurrentDirection(direction);

            _movementRepository.TurnLeft();

            var currentDirection = _movementRepository.GetCurrentDirection();

            Assert.Equal(expectedDirection, currentDirection);
        }

        [Theory]      
        [InlineData("10 10", "7 5 n", "flbBRFfL", 9, 8)]
        [InlineData("10 10", "7 5 S", "flbrlffFrb", 9, 5)]
        [InlineData("10 10", "7 5 W", "flbrb", 7, 6)]
        [InlineData("10 10", "7 5 e", "fl", 8, 5)]
        [InlineData("10 10", "7 5 e", "ffflf", 0, 6)]
        [InlineData("10 10", "7 5 n", "fflbBRfFfLbBrFff", 1, 3)]
        public void MovementHandler_Should_Move_Rover_To_Correct_Destination(string edges, string initialPosition, string commands, int expectedX, int expectedY)
        {

            var request = new MovementHandlerRequest()
            {
                Edges = edges,
                Obstacles = "",
                InitialPosition = initialPosition,
                Commands = commands
            };

            var movementHandler = new MovementHandler(_movementRepository);

            var result = movementHandler.Handle(request, new CancellationToken()).Result;

            Assert.Equal(expectedX, result.x);
            Assert.Equal(expectedY, result.y);
        }

        [Fact]
        public void Should_Throw_Exception_When_Edges_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
                EdgesInputValidator.Validate(null));
        }

        [Theory]
        [InlineData("1 2 c")]
        [InlineData("1")]
        [InlineData("1 - 2")]
        [InlineData("1 -2")]
        public void Should_Throw_Exception_When_Edges_Is_Not_In_Correct_Format(string edges)
        {
            Assert.Throws<ArgumentException>(() =>
                EdgesInputValidator.Validate(edges));
        }

        [Theory]
        [InlineData("1 2", 1, 2)]
        [InlineData("5 5", 5, 5)]
        [InlineData("5 -5", 5, -5)]
        public void Should_Return_Edges_For_Correct_Input_Format(string edgesInput, int expectedX, int expectedY)
        {
            (int x, int y) edges = EdgesInputConverter.ConvertToEdges(edgesInput);

            Assert.Equal(expectedX, edges.x);
            Assert.Equal(expectedY, edges.y);
        }

        //[Fact]
        //public void Should_Throw_Exception_When_Obstacles_Input_Is_Null()
        //{
        //    Assert.Throws<ArgumentNullException>(() =>
        //        ObstaclesInputValidator.Validate(null));
        //}

        [Theory]
        [InlineData("1 2")]
        [InlineData("(1 2 North)")]
        [InlineData("(1 2) X")]
        [InlineData("(1 2)(2)")]
        [InlineData("(1 2)()")]
        [InlineData("(1 -2)")]
        public void Should_Throw_Exception_When_Obstacles_Input_Is_Not_In_Correct_Format(string obstacles)
        {
            Assert.Throws<ArgumentException>(() =>
                ObstaclesInputValidator.Validate(obstacles));
        }

        [Theory]
        [InlineData(5, 5, "(2 4)(2 5)", 2, 3, DirectionEnum.North)]
        [InlineData(5, 5, "(2 5)", 2, 1, DirectionEnum.South)]
        public void Should_Not_Be_Able_To_Move_Forward_When_Obstacle_Detected(int width, int height, string obstacles, int x, int y, DirectionEnum direction)
        {
            _movementRepository.SetEdges((width, height));
            _movementRepository.SetObstacles(obstacles);
            _movementRepository.SetCurrentPosition((x, y));
            _movementRepository.SetCurrentDirection(direction);

            Assert.Throws<InvalidOperationException>(() =>
               _movementRepository.MoveForward());
        }

        [Theory]
        [InlineData(5, 5, "(2 2)", 2, 3, DirectionEnum.North)]
        [InlineData(5, 5, "(0 3)(4 6)(10 2)", 4, 3, DirectionEnum.West)]
        public void Should_Not_Be_Able_To_Move_Backward_When_Obstacle_Detected(int width, int height, string obstacles, int x, int y, DirectionEnum direction)
        {
            _movementRepository.SetEdges((width, height));
            _movementRepository.SetObstacles(obstacles);
            _movementRepository.SetCurrentPosition((x, y));
            _movementRepository.SetCurrentDirection(direction);

            Assert.Throws<InvalidOperationException>(() =>
               _movementRepository.MoveBackward());
        }

        [Theory]
        [InlineData("10 10", "(7 6)", "7 5 W", "flbrb")]
        [InlineData("10 10", "(0 5)", "7 5 e", "ffflf")]
        public void MovementHandler_Should_Stop_Command_When_Obstacle_Detected(string edges, string obstacles, string initialPosition, string commands)
        {

            var request = new MovementHandlerRequest()
            {
                Edges = edges,
                Obstacles = obstacles,
                InitialPosition = initialPosition,
                Commands = commands
            };

            var movementHandler = new MovementHandler(_movementRepository);

            Assert.Throws<AggregateException>(() =>
               movementHandler.Handle(request, new CancellationToken()).Result).Message.Contains($"{obstacles}");
        }
    }
}
