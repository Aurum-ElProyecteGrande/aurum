namespace Aurum.Services.Income
{
    public class IncomeService : IIncomeService
    {
        public (DateTime, DateTime) ValidateDates (DateTime? startDate, DateTime? endDate)
        {
            var validStartDate = new DateTime();
            var validEndDate = new DateTime();

            if (startDate.HasValue) validStartDate = startDate.Value;
            if (endDate.HasValue) validEndDate = endDate.Value;

            if (!startDate.HasValue || !endDate.HasValue) throw new NullReferenceException("Missing date");

            return (validStartDate, validEndDate);
        }

    }
}
