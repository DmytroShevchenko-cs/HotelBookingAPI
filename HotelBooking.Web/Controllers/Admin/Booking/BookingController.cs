namespace HotelBooking.Web.Controllers.Admin.Booking;

using Shared.Common.Constants;
using Base;
using BLL.Services.BookingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = AuthorizationConsts.Policies.Admin)]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.Admin)]
public sealed class BookingController(IBookingService bookingService) : BaseApiController
{

}