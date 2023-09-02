using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sewa_Application.Features.Category.Queries.GetAllCategories;
using System.Net;

namespace Sewa.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProviderController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ServiceProviderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet(Name = "GetCategories")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategories()
        {
            var categories = await _mediator.Send(new GetCategoryListRequest() { });
            return Ok(categories);
        }

    }
}
