using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Abstractions.Services;
using Domain.Commands;
using Domain.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _taskService.CreateTaskCommandHandler(command);

            return Created($"/api/tasks/{result.Payload.Id}", result);
        }

        [HttpPut("{id}:complete")]
        [ProducesResponseType(typeof(CompleteTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _taskService.CompleteTaskCommandHandler(new CompleteTaskCommand { Id = id });

                return Ok(result);
            }
            catch (NotFoundException<Guid>)
            {
                return NotFound();
            }            
        }

        [HttpPut("{id}:assign")]
        [ProducesResponseType(typeof(AssignTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(Guid id, AssignTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _taskService.AssignTaskCommandHandler(command);

                return Ok(result);
            }
            catch (NotFoundException<Guid>)
            {
                return NotFound();
            }            
        }
    }
}
