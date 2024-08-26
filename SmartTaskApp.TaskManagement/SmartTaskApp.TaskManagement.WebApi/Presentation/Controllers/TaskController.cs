using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTaskApp.TaskManagement.WebApi.Application.Commands;
using SmartTaskApp.TaskManagement.WebApi.Application.DTOs;
using SmartTaskApp.TaskManagement.WebApi.Application.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartTaskApp.TaskManagement.WebApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetTaskById(Guid id)
        {
            var query = new GetTaskByIdQuery { Id = id };
            var taskItem = await _mediator.Send(query);

            if (taskItem == null)
            {
                return NotFound();
            }

            return Ok(taskItem);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetAllTasks()
        {
            var query = new GetAllTasksQuery();
            var taskItems = await _mediator.Send(query);
            return Ok(taskItems);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Guid>> CreateTask(CreateTaskCommand command)
        {
            var taskId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTaskById), new { id = taskId }, taskId);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> UpdateTask(Guid id, UpdateTaskCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteTask(Guid id)
        {
            var command = new DeleteTaskCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
