using MediatR;
using ProgressService.Domian.Aggregates.UserStreaks;

namespace ProgressService.Features.Progress.Commands.CreateUserStreak
{
    public record CreateUserStreakCommand(UserStreak Streak) : IRequest;

}
