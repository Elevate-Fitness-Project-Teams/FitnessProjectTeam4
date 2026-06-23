namespace ProfileService.Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        Task ExecuteAsync(Func<Task> action);
    }
}
