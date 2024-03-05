using AutoMapper;
using DotnetAutoMapEFCoreAPI.Core.Context;
using DotnetAutoMapEFCoreAPI.Core.DTO;
using DotnetAutoMapEFCoreAPI.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetAutoMapEFCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public TicketsController(IConfiguration configuration, ApplicationDbContext dbContext, IMapper mapper)
        {
            this.configuration = configuration;
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto createTicketDto)
        {
            //This is not ideal way,since we have to manually map all the values,instead we can use automapper
            //var newTicket = new Ticket()
            //{
            //    From = createTicketDto.From,
            //    To= createTicketDto.To,
            //    PassengerName= createTicketDto.PassengerName
            //};

            var newTicket = new Ticket();

            //use automapper
            _mapper.Map(createTicketDto, newTicket);

            await _dbContext.Tickets.AddAsync(newTicket);
            await _dbContext.SaveChangesAsync();

            return Ok("Ticket Created Successfully");
        }

        [HttpGet]
        [Route("GetAllTickets")]

        public async Task<ActionResult<IEnumerable<GetTicketDto>>> GetAllTickets()
        {
            //Get Tickets from the context
            var tickets = await _dbContext.Tickets.ToListAsync();
            //using auto mapper we are only fetching the required details
            var requiredTicketDetails = _mapper.Map<IEnumerable<GetTicketDto>>(tickets);
            return Ok(requiredTicketDetails);
        }
        [HttpGet]
        [Route("GetTicketById/{id}")]

        public async Task<ActionResult<GetTicketDto>> GetTicketById([FromRoute] long id)
        {
            //Get Tickets from the context and check if exists
            var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(t=>t.Id==id);
            if(ticket is null)
            {
                return NotFound("Ticket Not Found");
            }

            var requiredTicket = _mapper.Map<GetTicketDto>(ticket);
            return Ok(requiredTicket);
           
        }
        [HttpPut]
        [Route("EditTicket/{id}")]
        public async Task<IActionResult> EditTicket([FromRoute] long id,[FromBody] UpdateTicketDto updateTicketDto)
        {
            //Get Tickets from the context and check if exists
            var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if (ticket is null)
            {
                return NotFound("Ticket Not Found");
            }

            _mapper.Map(updateTicketDto, ticket);
            ticket.UpdatedAt= DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return Ok("Ticket Updated Successfully");
           
        }

        [HttpDelete]
        [Route("DeleteTicket/{id}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] long id)
        {
            //Get Tickets from the context and check if exists
            var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if (ticket is null)
            {
                return NotFound("Ticket Not Found");
            }

            _dbContext.Tickets.Remove(ticket);
            await _dbContext.SaveChangesAsync();
            return Ok("Ticket Deleted Successfully");           

        }

    }
}
