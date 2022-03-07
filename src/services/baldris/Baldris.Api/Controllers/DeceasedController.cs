using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Baldris.Application.Common.Exceptions;
using Baldris.Application.Common.Models;
using Baldris.Application.Deceased.Deceased.Queries.GetByPersonId;
using Baldris.Application.Deceased.Models;
using Baldris.Application.Deceased.Deceased.Commands.Create;
using Baldris.Application.Deceased.Deceased.Commands.Delete;
using Baldris.Application.Deceased.Deceased.Commands.Update;
using Baldris.Application.Deceased.Deceased.Queries.GetAll;
using Baldris.Application.Deceased.Deceased.Queries.GetById;
using Baldris.Application.Deceased.Deceased.Queries.GetByIds;
using Baldris.Application.Deceased.Deceased.Queries.GetPaginated;
using Baldris.Application.Deceased.Deceased.Queries.GetByWorkOrderId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DeceasedDto = Baldris.Application.Deceased.Models.DeceasedDto;

namespace Baldris.Api.Controllers {
    [Authorize("read:workOrders")]
    public class DeceasedController : BaldrisController {
        // GET: api/Deceased
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DeceasedDto>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetDeceased([FromQuery] Guid[] ids = null) {
            var guids = (ids ?? Array.Empty<Guid>()).ToList();
            if (guids.Any())
            {
                return Ok(await Mediator.Send(new GetDeceasedByIdsQuery(guids)));
            }
            return Ok(await Mediator.Send(new GetAllDeceasedQuery()));
        }
        
        // GET: api/Deceased/Items[?pageSize=50&pageIndex=10]
        [HttpGet]
        [Route("Items", Name = "GetDeceasedItems")]
        [ProducesResponseType(typeof(PaginatedItems<DeceasedDto>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetDeceasedItems([FromQuery] int pageSize = 50, [FromQuery] int pageIndex = 0) {
            return Ok(await Mediator.Send(new GetPaginatedDeceasedQuery(pageSize, pageIndex, null)));
        }

        // GET: api/Deceased/Search
        [HttpGet]
        [Route("Search", Name = "FindDeceased")]
        [ProducesResponseType(typeof(PaginatedItems<DeceasedDto>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> FindDeceased([FromQuery] int pageSize = 50, [FromQuery] int pageIndex = 0, [FromQuery] string searchString = null) {
            return Ok(await Mediator.Send(new GetPaginatedDeceasedQuery(pageSize, pageIndex, searchString)));
        }

        // GET: api/Deceased/ByWorkOrderId/abaa5237-6927-404e-b3ab-001cb2b9600b
        [HttpGet]
        [Route("ByWorkOrderId/{workOrderId}")]
        [ProducesResponseType(typeof(IEnumerable<DeceasedDto>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetDeceasedByWorkOrderId([FromRoute] Guid workOrderId)
        {
            return Ok(await Mediator.Send(new GetDeceasedByWorkOrderIdQuery(workOrderId)));
        }

        // GET: api/Deceased/ByPersonId/abaa5237-6927-404e-b3ab-001cb2b9600b
        [HttpGet]
        [Route("ByPersonId/{personId}")]
        [ProducesResponseType(typeof(IEnumerable<DeceasedDto>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetDeceasedByPersonId([FromRoute] Guid personId) {
            return Ok(await Mediator.Send(new GetDeceasedByPersonIdQuery(personId)));
        }

        // GET: api/Deceased/abaa5237-6927-404e-b3ab-001cb2b9600b
        [HttpGet]
        [Route("{id}", Name = "GetDeceased")]
        [ProducesResponseType(typeof(DeceasedDto), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetDeceased([FromRoute] Guid id, [FromQuery] bool asDto = false) {
            try
            {
                return Ok(await Mediator.Send(new GetDeceasedByIdQuery(id)));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Deceased/abaa5237-6927-404e-b3ab-001cb2b9600b
        [HttpGet]
        [Route("{id}", Name = "GetDeceased")]
        [ProducesResponseType(typeof(DeceasedDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDeceased([FromRoute] Guid id) {
            try
            {
                return Ok(await Mediator.Send(new GetDeceasedByIdQuery(id)));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/Deceased/abaa5237-6927-404e-b3ab-001cb2b9600b
        [HttpPut]
        [Route("{id}", Name = "PutDeceased")]
        [Authorize("write:workOrders")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> PutDeceased([FromRoute] Guid id, [FromBody] UpdateDeceasedCommand updateDeceasedCommand) {
            try
            {
                await Mediator.Send(updateDeceasedCommand);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Deceased
        [HttpPost]
        [Authorize("write:workOrders")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(DeceasedDto), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> PostDeceased([FromBody] CreateDeceasedCommand createDeceasedCommand)
        {
            var deceased = await Mediator.Send(createDeceasedCommand);
            return CreatedAtRoute(nameof(GetDeceased), new { id = deceased.Id }, deceased);
        }

        // DELETE: api/Deceased/abaa5237-6927-404e-b3ab-001cb2b9600b
        [HttpDelete]
        [Route("{id}", Name = "DeleteDeceased")]
        [Authorize("delete:workOrders")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteDeceased([FromRoute] Guid id) {
            try
            {
                await Mediator.Send(new DeleteDeceasedCommand(id));
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}