using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTaskApp.ProjectManagement.WebApi.Application.Commands;
using SmartTaskApp.ProjectManagement.WebApi.Application.DTOs;
using SmartTaskApp.ProjectManagement.WebApi.Application.Queries;

namespace SmartTaskApp.ProjectManagement.WebApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProjectById(Guid id)
        {
            var query = new GetProjectByIdQuery { Id = id };
            var project = await _mediator.Send(query);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects()
        {
            var query = new GetAllProjectsQuery();
            var projects = await _mediator.Send(query);
            return Ok(projects);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Guid>> CreateProject(CreateProjectCommand command)
        {
            var projectId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProjectById), new { id = projectId }, projectId);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> UpdateProject(Guid id, UpdateProjectCommand command)
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
        public async Task<ActionResult> DeleteProject(Guid id)
        {
            var command = new DeleteProjectCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
