using Healthcare_Patient_Portal.Features.User.Operations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_Patient_Portal.Features.User
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("SearchUser/{UserId}")]
        public async Task<IActionResult> SearchUser(SearchUser request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPatch("UpdateUser/{UserId}")]
        public async Task<IActionResult> UpdateUser(UpdateUser request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        
        [HttpDelete("DeleteUser/{UserId}")]
        public async Task<IActionResult> DeleteUser(DeleteUser request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
