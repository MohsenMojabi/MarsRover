using MarsRover.Domain.Handlers;
using MarsRover.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovementsController : ControllerBase
    {
        private readonly IMediator _mediat;

        public MovementsController(IMediator mediat)
        {
            _mediat = mediat;
        }

        [HttpPost]
        public async Task<ActionResult<CommandsResult>> Move(string edges, string obstacles, string initialPosition, string commands)
        {
            var request = new MovementHandlerRequest() { 
                Edges = edges,
                Obstacles = obstacles ?? "",
                InitialPosition = initialPosition,
                Commands = commands
            };
            try
            {
                var (x, y) = await _mediat.Send(request);
                return Ok(new CommandsResult() { 
                    X = x,
                    Y = y,
                    Message = "Rover touched the destination successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
