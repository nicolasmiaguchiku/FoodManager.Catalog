using FoodManager.Catalog.Application.Input.Requests;
using FoodManager.Catalog.Application.Output.Response;
using FoodManager.Catalog.Catalog.Domain.Results;
using FoodManager.Catalog.Domain.Filters;
using LiteBus.Queries.Abstractions;

namespace FoodManager.Catalog.Application.Output.Queries;

public sealed record GetFoodQuery(GetFoodRequest Foodequest) : IQuery<Result<PagedResult<GetFoodResponse>>>;