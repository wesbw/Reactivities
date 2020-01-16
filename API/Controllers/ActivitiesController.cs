using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain;
using MediatR;
using Application.Activities;
using System.Threading;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ActivitiesController(IMediator mediator)
        {
            this._mediator = mediator;

        }

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> List(CancellationToken ct)
        {
            return await _mediator.Send(new List.Query(), ct);
        }

        // GET: api/Activities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> Details(Guid id, CancellationToken ct)
        {
            var activity = await _mediator.Send(new Details.Query { Id = id });

            if (activity == null)
            {
                return NotFound();
            }

            return activity;
        }
        // POST: api/Activities
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }



        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteActivity(Guid id)
        {
            return await _mediator.Send(new Delete.Command() { Id = id });
        }

        // private bool ActivityExists(Guid id)
        // {
        //     return _context.Activities.Any(e => e.Id == id);
        // }
    }
}
