using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AwesomeCalendar.Contracts.Commands;
using AwesomeCalendar.Infrastructure.Enums;
using AwesomeCalendar.Infrastructure.Interfaces.Busses;
using Microsoft.Ajax.Utilities;

namespace AwesomeCalendar.Web.Controllers
{
    //[Authorize]
    [RoutePrefix("api/ValuesCtrl")]
    public class ValuesController : ApiController
    {
        ICommandBus CommandBus { get; }
        public ValuesController(ICommandBus commandBus)
        {
            CommandBus = commandBus;
        }

        [HttpPost, Route("Send")]
        public void Send()
        {
            CommandBus.Send(new CreateCalendarItemCommand
            {
                Description = "Test",
                Name = "Test",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                UserId = Guid.NewGuid().ToString(),
                Cycles = new List<CalendarItemCycle>
                {
                    new CalendarItemCycle
                    {
                        Type = CalendarItemCycleType.Daily,
                        Interval = 1
                    }
                }
            });
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
