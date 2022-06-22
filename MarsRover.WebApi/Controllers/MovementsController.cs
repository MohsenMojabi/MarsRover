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

        /// <summary>
        /// Translate recieved commands for Mars Roter to move
        /// </summary>
        /// <param name="inputs">
        ///     <param name="edges">Mars Rover wraps at edges. Edges are 2 positive integer numbers like 5 5</param>
        ///     <param name="obstacles">Mars Rover stops at obstacles. Obstacles are list of pair positive integer numbers like (2 3)(6 1)(9 7)</param>
        ///     <param name="initialPosition">Initial position of Mars Rover. Initial position contains 2 positive integer numbers and initial direction (N, n, S, s, W, w, E, e) like 4 8 N</param>
        ///     <param name="commands">Commands could contain F, f, B, b, R, r, L, l characters for moving forward, backward, turning right, turning left like FrlLLRFffBRbbLllrrR</param>
        /// </param>
        /// <returns>The last position that Mars Rover could move to if no exception had been throwed.</returns>
        [HttpPost]
        public async Task<ActionResult> Move(InitialInputs inputs)
        {
            var request = new MovementHandlerRequest() { 
                Edges = inputs.Edges,
                Obstacles = inputs.Obstacles ?? "",
                InitialPosition = inputs.InitialPosition,
                Commands = inputs.Commands
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
