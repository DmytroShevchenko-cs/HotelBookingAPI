namespace HotelBooking.Web.Controllers.User.Booking;

using Shared.Common.Constants;
using Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = AuthorizationConsts.Policies.User)]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.User)]
public sealed class BookingController : BaseApiController
{

}