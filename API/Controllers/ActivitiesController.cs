using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain;
using MediatR;
using Application.Activities;
using System.Threading;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class ActivitiesController : BaseController
    {

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<List<ActivityDto>>> List(CancellationToken ct)
        {
            return await Mediator.Send(new List.Query(), ct);
        }

        // GET: api/Activities/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ActivityDto>> Details(Guid id, CancellationToken ct)
        {
            var activity = await Mediator.Send(new Details.Query { Id = id });

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
            // //when used DataAnotations 
            // if (!ModelState.IsValid)
            //     return BadRequest(ModelState);
            return await Mediator.Send(command);
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Authorize(Policy = "IsActivityHost")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }



        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "IsActivityHost")]
        public async Task<ActionResult<Unit>> DeleteActivity(Guid id)
        {
            return await Mediator.Send(new Delete.Command() { Id = id });
        }

        [HttpPost("{id}/attend")]
        public async Task<ActionResult<Unit>> Attend(Guid id)
        {
            return await Mediator.Send(new Attend.Command() { Id = id });
        }

        [HttpDelete("{id}/attend")]
        public async Task<ActionResult<Unit>> Unattend(Guid id)
        {
            return await Mediator.Send(new Unattend.Command() { Id = id });
        }

        // private bool ActivityExists(Guid id)
        // {
        //     return _context.Activities.Any(e => e.Id == id);
        // }

    }
}
