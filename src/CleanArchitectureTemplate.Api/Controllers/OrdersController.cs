#if (shared)
using CleanArchitectureTemplate.Shared.Dispatchers;
#endif
using System.Threading.Tasks;
using CleanArchitectureTemplate.Application.Commands;
using CleanArchitectureTemplate.Application.Queries;
#if (!shared)
using CleanArchitectureTemplate.Application.Services;
#endif
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.Api.Controllers
{
    public class OrdersController : BaseController
    {
        public OrdersController(IDispatcher dispatcher) 
            : base(dispatcher) { }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetOrder query) 
            => Select(await Dispatcher.QueryAsync(query));

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] GetOrders query) 
            => Select(await Dispatcher.QueryAsync(query));

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrder command)
        {
            await Dispatcher.SendAsync(command);
        
            return CreatedAtAction(nameof(Get), new { Id = command.Id }, command.Id);
        }
    }
}
