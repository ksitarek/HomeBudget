using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HomeBudget.Application.Commands.Categories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCaching.Internal;

using HomeBudget.Application.Queries.Categories;
using HomeBudget.Api.Requests.Categories;
using AutoMapper;

namespace HomeBudget.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateCategoryCommand(request.Label), cancellationToken);

            if (result.IsSuccess == false)
            {
                return StatusCode(500, result.Exception);
            }

            return CreatedAtAction(nameof(Get), new CategoryQuery { Id = result.ObjectId }, result.ObjectId);
        }

        [HttpPost("{id}/rename")]
        public async Task<IActionResult> Rename([FromRoute]Guid id, [FromBody] RenameCategoryRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RenameCategoryCommand(id, request.NewLabel), cancellationToken);

            if (result.IsSuccess == false)
            {
                return StatusCode(500, result.Exception);
            }

            return AcceptedAtAction(nameof(Get), new CategoryQuery { Id = result.ObjectId }, result.ObjectId);
        }

        [HttpPost("{id}/archive")]
        public async Task<IActionResult> Archive([FromQuery]Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ArchiveCategoryCommand(id), cancellationToken);

            if (result.IsSuccess == false)
            {
                return StatusCode(500, result.Exception);
            }

            return AcceptedAtAction(nameof(Get), new CategoryQuery { Id = result.ObjectId }, result.ObjectId);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] CategoryListQuery request, CancellationToken cancellationToken)

        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromQuery] CategoryQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}