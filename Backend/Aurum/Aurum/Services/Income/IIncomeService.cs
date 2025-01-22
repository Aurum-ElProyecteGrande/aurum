namespace Aurum.Services.Income
{
    public interface IIncomeService
    {
        (DateTime, DateTime) ValidateDates(DateTime? startDate, DateTime? endDate);
    }
}
