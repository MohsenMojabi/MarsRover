using MarsRover.Domain.Handlers;
using MarsRover.Domain.Repository;
using MarsRover.Models.Enums;
using MarsRover.WebApi.Controllers;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MarsRover.WebApi.Test
{
    public class MovementsControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;

        private readonly MovementRepository _movementRepository;

        public MovementsControllerTest()
        {
            _movementRepository = new MovementRepository();
            _movementRepository.SetCurrentPosition((-4, 6));
            _movementRepository.SetCurrentDirection(DirectionEnum.North);
            _mockMediator = new Mock<IMediator>();
            //_mockMediator.Setup(p =>p.Send);
            var request = new MovementHandlerRequest()
            {
                Edges = "10 10",
                Obstacles =  "(7 6)",
                InitialPosition = "7 5 n",
                Commands = "flbBRFfL"
            };
            _mockMediator.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).Returns(GetPosition);
            
        }

        [Fact]
        public void POST_Should_Return_Correct_Response_With_Correct_Parameters()
        {
            //[InlineData(, , , 9, 8)]
            var controller = new MovementsController(_mockMediator.Object);
            //var request = new MovementHandlerRequest()
            //{
            //    Edges = "10 10",
            //    Obstacles = "(7 6)",
            //    InitialPosition = "7 5 n",
            //    Commands = "flbBRFfL"
            //};
            //var s = (4, 5);
            //_mockMediator.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).Returns(GetPosition);
            var response = controller.Move("10 10", "(7 6)", "7 5 n", "flbBRFfL").Result;
            //_mockMediator.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).Returns(GetPosition);
            Assert.Equal("", response.Result.ToString());

        }

        public Task<(int x, int y)> GetPosition()
        {
            //return await Task.FromResult((5, 4));
            var request = new MovementHandlerRequest()
            {
                Edges = "10 10",
                Obstacles = "(7 6)",
                InitialPosition = "7 5 n",
                Commands = "flbBRFfL"
            };

            var movementHandler = new MovementHandler(_movementRepository);

            return movementHandler.Handle(request, new CancellationToken());
        }
    }
}
