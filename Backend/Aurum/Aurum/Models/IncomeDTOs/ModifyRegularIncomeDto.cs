namespace Aurum.Models.IncomeDTOs
{
    public record ModifyRegularIncomeDto (int AccountId, int CategoryId, string Label, decimal Amount, DateTime StartDate, Regularity Regularity);    
}