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

        [HttpPut("FullUpdateUser")]
        public async Task<IActionResult> FullUpdateUser(FullUpdateUser request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(DeleteUser request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
