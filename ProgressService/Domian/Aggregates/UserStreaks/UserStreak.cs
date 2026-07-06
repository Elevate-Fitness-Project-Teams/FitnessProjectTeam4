namespace ProgressService.Domian.Aggregates.UserStreaks
{
    public class UserStreak
    {
        public Guid Id { get; private set; }
        public string UserId { get; private set; }
        public int CurrentStreak { get; private set; }
        public int LongestStreak { get; private set; }
        public DateTime? LastWorkoutDate { get; private set; }

        private UserStreak() { }

        public UserStreak(Guid id, string userId)
        {
            Id = id;
            UserId = userId;
            CurrentStreak = 0;
            LongestStreak = 0;
        }

        public bool UpdateStreak(DateTime completionDate)
        {
            var localCompletionDate = completionDate.Date;

            if (LastWorkoutDate == null)
            {
                CurrentStreak = 1;
                LongestStreak = 1;
                LastWorkoutDate = localCompletionDate;
                return true;
            }

            var lastDate = LastWorkoutDate.Value.Date;
            var daysDifference = (localCompletionDate - lastDate).Days;

            if (daysDifference == 1) // تمرن في اليوم التالي مباشرة (تتابع صحيح)
            {
                CurrentStreak++;
                if (CurrentStreak > LongestStreak)
                {
                    LongestStreak = CurrentStreak;
                }
                LastWorkoutDate = localCompletionDate;
                return true;
            }
            else if (daysDifference > 1) // انقطعت السلسلة
            {
                CurrentStreak = 1;
                LastWorkoutDate = localCompletionDate;
                return true;
            }

            // إذا تمرن في نفس اليوم مرة أخرى لا نغير الـ Counter
            return false;
        }
    }
}
