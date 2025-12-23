namespace HotelBooking.Web.Controllers.Admin.Rooms;

using Base;
using BLL.Services.RoomsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Constants;

[Authorize(Policy = AuthorizationConsts.Policies.Admin)]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.Admin)]
public sealed class RoomController(IRoomsService roomsService) : BaseApiController
{

}