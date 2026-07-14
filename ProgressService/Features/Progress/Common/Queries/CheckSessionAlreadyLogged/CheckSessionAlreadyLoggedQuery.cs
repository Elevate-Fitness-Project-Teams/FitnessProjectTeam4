using MediatR;

namespace ProgressService.Features.Progress.Common.Queries.CheckSessionAlreadyLogged
{
    public record CheckSessionAlreadyLoggedQuery(Guid sessionId) : IRequest<bool>;

}
