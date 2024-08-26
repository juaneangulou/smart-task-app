using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTaskApp.Notifications.WebApi.Application.Commands;
using SmartTaskApp.Notifications.WebApi.Application.DTOs;
using SmartTaskApp.Notifications.WebApi.Application.Queries;

namespace SmartTaskApp.Notifications.WebApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationDto>> GetNotificationById(Guid id)
        {
            var query = new GetNotificationByIdQuery { Id = id };
            var notification = await _mediator.Send(query);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetAllNotifications()
        {
            var query = new GetAllNotificationsQuery();
            var notifications = await _mediator.Send(query);
            return Ok(notifications);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Guid>> CreateNotification(CreateNotificationCommand command)
        {
            var notificationId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetNotificationById), new { id = notificationId }, notificationId);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> UpdateNotification(Guid id, UpdateNotificationCommand command)
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
        public async Task<ActionResult> DeleteNotification(Guid id)
        {
            var command = new DeleteNotificationCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
