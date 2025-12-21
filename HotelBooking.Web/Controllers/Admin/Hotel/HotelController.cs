namespace HotelBooking.Web.Controllers.Admin.Hotel;

using Shared.Common.Constants;
using Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = AuthorizationConsts.Policies.Admin)]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.Admin)]
public sealed class HotelController : BaseApiController
{

}