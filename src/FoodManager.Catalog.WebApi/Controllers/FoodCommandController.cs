using FoodManager.Catalog.Application.Input.Handlers.Commands;
using FoodManager.Internal.Shared.Attributes;
using FoodManager.Internal.Shared.Dtos;
using FoodManager.Internal.Shared.Http.Catalog.Requests;
using LiteBus.Commands.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FoodManager.Catalog.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/food")]
    [Authorize]
    public class FoodCommandController(ICommandMediator commandMediator) : ControllerBase
    {
        /// <summary>
        ///     Add a new food to the platform.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequiredRole("AddFood")]
        public async Task<IActionResult> AddFoodAsync([FromBody] AddFoodRequest request, CancellationToken cancellationToken)
        {
            var result = await commandMediator.SendAsync(new AddFoodCommand(request), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Error);
        }

        /// <summary>
        ///     Delete a food based on id previosly informed.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFoodAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await commandMediator.SendAsync(new DeleteFoodCommand(id), cancellationToken);

            if (result.IsSuccess)
            {
                return NoContent();

            }

            return BadRequest(result.Error);
        }

        /// <summary>
        ///     Update a food based on id previosly informed.
        /// </summary>
        /// <returns> a status code related to the operation</returns>
        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateFoodAsync(Guid id, [FromBody] JsonPatchDocument<FoodDto> food, CancellationToken cancellationToken)
        {
            var updateFoodRequest = new UpdateFoodRequest(id)
            {
                FoodPatchDocument = food
            };

            var result = await commandMediator.SendAsync(new UpdateFoodCommand(updateFoodRequest), cancellationToken);

            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result.Error);
        }
    }
}