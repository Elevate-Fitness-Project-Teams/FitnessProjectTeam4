using ProgressService.Common.Exceptions;
using ProgressService.Common.Responses;

namespace ProgressService.Domian.Aggregates.WeightHistories
{
    public class WeightHistory
    {
        public Guid Id { get; private set; }
        public string UserId { get; private set; } 
        public double Weight { get; private set; }
        public DateTime Date { get; private set; }
        public string? Notes { get; private set; }

        private WeightHistory() { }

        public WeightHistory(Guid id, string userId, double weight, DateTime date, string? notes)
        {
            if (weight < 40 || weight > 200)
                throw new DomainException(ErrorCode.InvalidWeightValue);

            Id = id;
            UserId = userId;
            Weight = weight;
            Date = date.Date; 
            Notes = notes;
        }
    }
}
