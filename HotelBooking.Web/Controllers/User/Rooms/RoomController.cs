namespace HotelBooking.Web.Controllers.User.Rooms;

using Shared.Common.Constants;
using Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = AuthorizationConsts.Policies.User)]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.User)]
public sealed class RoomController : BaseApiController
{

}