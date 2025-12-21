namespace HotelBooking.Web.Controllers.Admin.Booking;

using Shared.Common.Constants;
using Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = AuthorizationConsts.Policies.Admin)]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.Admin)]
public sealed class BookingController : BaseApiController
{

}