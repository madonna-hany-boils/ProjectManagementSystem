using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.Features.Tasks.Commands;
using ProjectManagementSystem.Application.Features.Tasks.DTOs;
using ProjectManagementSystem.Application.Features.Tasks.Queries;

namespace ProjectManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        
            private readonly IMediator _mediator;

            public TasksController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] CreateTaskDTO dto)
            {
                var command = new CreateTaskCommand { CreateTaskDTO = dto };
                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllTasksQuery();
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteTaskCommand(id);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProjectId(Guid projectId)
        {
            var query = new GetTasksByProjectIdQuery(projectId);
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateTaskStatusDTO dto)
        {
            var command = new UpdateTaskStatusCommand { UpdateTaskStatusDTO = dto };
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
    }
