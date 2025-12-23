namespace HotelBooking.Web.Controllers.User.Booking;

using Shared.Common.Constants;
using Base;
using BLL.Services.BookingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = AuthorizationConsts.Policies.User)]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.User)]
public sealed class BookingController(IBookingService bookingService) : BaseApiController
{

}