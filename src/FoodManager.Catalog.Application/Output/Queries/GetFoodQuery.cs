using Mattioli.Configurations.Models;
using FoodManager.Catalog.Domain.Filters;
using LiteBus.Queries.Abstractions;
using Mattioli.Configurations.Http;
using FoodManager.Internal.Shared.Http.Catalog.Requests;
using FoodManager.Internal.Shared.Http.Catalog.Responses;

namespace FoodManager.Catalog.Application.Output.Queries;

public sealed record GetFoodQuery(GetFoodRequest Foodequest) : IQuery<Result<PagedResult<GetFoodResponse>>>;