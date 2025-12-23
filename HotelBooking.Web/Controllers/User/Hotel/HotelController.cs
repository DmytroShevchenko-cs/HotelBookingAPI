namespace HotelBooking.Web.Controllers.User.Hotel;

using Shared.Common.Constants;
using Base;
using BLL.Services.HotelsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = AuthorizationConsts.Policies.User)]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.User)]
public sealed class HotelController(IHotelsService hotelsService) : BaseApiController
{

}