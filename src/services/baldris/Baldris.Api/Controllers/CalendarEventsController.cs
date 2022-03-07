using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Baldris.Application.Calendar.CalendarEvents.Commands.Create;
using Baldris.Application.Calendar.CalendarEvents.Commands.Delete;
using Baldris.Application.Calendar.CalendarEvents.Commands.ToggleCompleted;
using Baldris.Application.Calendar.CalendarEvents.Commands.Update;
using Baldris.Application.Calendar.CalendarEvents.Queries.CanToggle;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Calendar.CalendarEvents.Queries.GetAll;
using Baldris.Application.Calendar.CalendarEvents.Queries.GetBetweenDates;
using Baldris.Application.Calendar.CalendarEvents.Queries.GetById;
using Baldris.Application.Calendar.CalendarEvents.Queries.GetByIds;
using Baldris.Application.Calendar.CalendarEvents.Queries.GetPaginated;
using Baldris.Application.Calendar.CalendarEvents.Queries.GetByWorkOrderId;
using Baldris.Application.Calendar.CalendarEvents.Queries.GetByCalendarEventTypeId;
using Baldris.Application.Common.Exceptions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Baldris.Api.Controllers
{
    [Authorize("read:calendar")]
    public class CalendarEventsController : BaldrisController
    {
        private readonly ILoggedInUserService _loggedInUserService;

        public CalendarEventsController(ILoggedInUserService loggedInUserService)
        {
            _loggedInUserService = loggedInUserService;
        }
        
        // GET: api/CalendarEvents
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CalendarEventDto>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetCalendarEvents([FromQuery] Guid[] ids = null) {
            var guids = (ids ?? Array.Empty<Guid>()).ToList();
            if (guids.Any())
            {
                return Ok(await Mediator.Send(new GetCalendarEventsByIdsQuery(guids)));
            }
            return Ok(await Mediator.Send(new GetAllCalendarEventsQuery()));
        }
        
        // GET: api/CalendarEvents/Items[?pageSize=50&pageIndex=10]
        [HttpGet]
        [Route("Items", Name = "GetCalendarEventItems")]
        [ProducesResponseType(typeof(PaginatedItems<CalendarEventDto>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetCalendarEventItems([FromQuery] int pageSize = 50, [FromQuery] int pageIndex = 0, [FromQuery] Guid? calendarEventId = null) {
            return Ok(await Mediator.Send(new GetPaginatedCalendarEventsQuery(pageSize, pageIndex, null, calendarEventId)));
        }

        // GET: api/CalendarEvents/BetweenDates
        [HttpGet]
        [Route("BetweenDates")]
        [ProducesResponseType(typeof(PaginatedItems<CalendarEventDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCalendarEventsBetweenDates([FromQuery] DateTime startTime, [FromQuery] DateTime endTime, [FromQuery] int pageSize = 50, [FromQuery] int pageIndex = 0)
        {
            return Ok(await Mediator.Send(new GetCalendarEventsBetweenDatesQuery(pageSize, pageIndex, startTime, endTime)));
        }

        // GET: api/CalendarEvents/ByUserId/5
        [HttpGet]
        [Route("ByUserId/{userId}")]
        [ProducesResponseType(typeof(PaginatedItems<CalendarEventDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCalendarEventsByUserId([FromRoute] string userId, [FromQuery] int pageSize = 50, [FromQuery] int pageIndex = 0, [FromQuery] Guid? calendarEventId = null)
        {
            return Ok(await Mediator.Send(new GetPaginatedCalendarEventsQuery(pageSize, pageIndex, null, calendarEventId, userId)));
        }

        // GET: api/CalendarEvents/ByWorkOrderId/abaa5237-6927-404e-b3ab-001cb2b9600b
        [HttpGet]
        [Route("ByWorkOrderId/{workOrderId}")]
        [ProducesResponseType(typeof(IEnumerable<CalendarEventDto>), (int) HttpStatusCode.OK)]
        [Authorize("read:workOrders")]
        public async Task<IActionResult> GetCalendarEventsByWorkOrderId([FromRoute] Guid workOrderId)
        {
            return Ok(await Mediator.Send(new GetCalendarEventsByWorkOrderIdQuery(workOrderId)));
        }

        // GET: api/CalendarEvents/ByPartyId/5
        [HttpGet]
        [Route("ByPartyId/{partyId}")]
        [ProducesResponseType(typeof(IEnumerable<CalendarEventDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCalendarEventsByPartyId([FromRoute] Guid partyId, [FromQuery] int pageSize = 50, [FromQuery] int pageIndex = 0, [FromQuery] Guid? calendarEventId = null)
        {
            return Ok(await Mediator.Send(new GetPaginatedCalendarEventsQuery(pageSize, pageIndex, null, calendarEventId, null, partyId)));

        }

        // GET: api/CalendarEvents/ByUserId/5/BetweenDates
        [HttpGet]
        [Route("ByUserId/{userId}/BetweenDates")]
        [ProducesResponseType(typeof(IEnumerable<CalendarEventDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCalendarEventsByUserIdBetweenDates([FromRoute] string userId, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime, [FromQuery] int pageSize = 50, [FromQuery] int pageIndex = 0)
        {
            return Ok(await Mediator.Send(new GetCalendarEventsBetweenDatesQuery(pageSize, pageIndex, startTime, endTime, userId)));
        }

        // GET: api/CalendarEvents/Search
        [HttpGet]
        [Route("Search", Name = "FindCalendarEvents")]
        [ProducesResponseType(typeof(PaginatedItems<CalendarEventDto>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> FindCalendarEvents([FromQuery] int pageSize = 50, [FromQuery] int pageIndex = 0, [FromQuery] string searchString = null, [FromQuery] Guid? calendarEventId = null) {
            return Ok(await Mediator.Send(new GetPaginatedCalendarEventsQuery(pageSize, pageIndex, searchString, calendarEventId)));
        }
        
        // GET: api/CalendarEvents/ByCalendarEventTypeId/abaa5237-6927-404e-b3ab-001cb2b9600b
        [HttpGet]
        [Route("ByCalendarEventTypeId/{calendarEventTypeId}")]
        [ProducesResponseType(typeof(PaginatedItems<CalendarEventDto>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetCalendarEventsByCalendarEventTypeId([FromRoute] Guid calendarEventTypeId, [FromQuery] int pageSize = 50, [FromQuery] int pageIndex = 0)
        {
            return Ok(await Mediator.Send(new GetPaginatedCalendarEventsQuery(pageSize, pageIndex, null, calendarEventTypeId)));
        }

        // GET: api/CalendarEvents/Upcoming
        [HttpGet]
        [Route("Upcoming")]
        [ProducesResponseType(typeof(PaginatedItems<CalendarEventDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUpcomingCeremonies([FromQuery] DateTime? startTime = null, [FromQuery] int pageSize = 50, [FromQuery] int pageIndex = 0, [FromQuery] string userId = null)
        {
            return Ok(await Mediator.Send(new GetCalendarEventsBetweenDatesQuery(pageSize, pageIndex, startTime ?? DateTime.Now, DateTime.Now.AddYears(10), userId)));
        }

        // GET: api/CalendarEvents/abaa5237-6927-404e-b3ab-001cb2b9600b
        [HttpGet]
        [Route("{id}", Name = "GetCalendarEvent")]
        [ProducesResponseType(typeof(CalendarEventDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCalendarEvent([FromRoute] Guid id) {
            try
            {
                return Ok(await Mediator.Send(new GetCalendarEventByIdQuery(id)));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/CalendarEvents/abaa5237-6927-404e-b3ab-001cb2b9600b/CanToggle
        [HttpGet]
        [Route("{id}/CanToggle")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> CanToggleCompletedCalendarEvent([FromRoute] Guid id, [FromQuery] string userId = null)
        {
            try
            {
                userId ??= await _loggedInUserService.GetUserIdAsync();
                return Ok(await Mediator.Send(new CanToggleCalendarEventQuery(id, userId, !_loggedInUserService.CurrentUserHasScope("write:workOrders"))));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // Put: api/CalendarEvents/abaa5237-6927-404e-b3ab-001cb2b9600b/Toggle
        [HttpPut]
        [Route("{id}/Toggle")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Authorize("write:calendar")]
        public async Task<IActionResult> ToggleCompletedCalendarEvent([FromRoute] Guid id, [FromQuery] string userId = null)
        {
            try
            {
                userId ??= await _loggedInUserService.GetUserIdAsync();
                await Mediator.Send(new ToggleCalendarEventCompletedCommand(id, userId,
                    !_loggedInUserService.CurrentUserHasScope("write:workOrders")));
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ForbiddenOperationException)
            {
                return Forbid();
            }
            catch (UnauthorizedException)
            {
                return Unauthorized();
            }
        }

        // PUT: api/CalendarEvents/abaa5237-6927-404e-b3ab-001cb2b9600b
        [HttpPut]
        [Route("{id}", Name = "PutCalendarEvent")]
        [Authorize("write:calendar")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> PutCalendarEvent([FromRoute] Guid id, [FromBody] UpdateCalendarEventCommand updateCalendarEventCommand) {
            try
            {
                await Mediator.Send(updateCalendarEventCommand);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/CalendarEvents
        [HttpPost]
        [Authorize("write:calendar")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(CalendarEventDto), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> PostCalendarEvent([FromBody] CreateCalendarEventCommand createCalendarEventCommand)
        {
            var calendarEvent = await Mediator.Send(createCalendarEventCommand);
            return CreatedAtRoute(nameof(GetCalendarEvent), new { id = calendarEvent.Id }, calendarEvent);
        }

        // DELETE: api/CalendarEvents/abaa5237-6927-404e-b3ab-001cb2b9600b
        [HttpDelete]
        [Route("{id}", Name = "DeleteCalendarEvent")]
        [Authorize("delete:calendar")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCalendarEvent([FromRoute] Guid id) {
            try
            {
                await Mediator.Send(new DeleteCalendarEventCommand(id));
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}