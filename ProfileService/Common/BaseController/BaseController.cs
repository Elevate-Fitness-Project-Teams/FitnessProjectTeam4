using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;

namespace ProfileService.Common.BaseEndPoint
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
