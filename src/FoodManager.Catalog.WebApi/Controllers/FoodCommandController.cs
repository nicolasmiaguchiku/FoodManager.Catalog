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
    public class FoodCommandController(ICommandMediator commandMediator) : ControllerBase
    {
        /// <summary>
        ///     Register a new food
        /// </summary>
        /// <returns>The details of the newly created food item or a validation error.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[RequiredRole("AddFood")]
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
        ///     Excludes a specific food item based on the provided identifier.
        /// </summary>
        /// <returns> Returns 204 (No Content) on successful validation or a validation error.</returns>
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
        ///     Partially updates the data for an existing food item.
        /// </summary>
        /// <returns>The request must follow the JSON Patch standard (RFC 6902).</returns>
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