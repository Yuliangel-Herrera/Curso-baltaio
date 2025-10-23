namespace Fina.Core.Requests.Transactions
{
    public class GetTransactionsByPeriodRequest : PagedRequest
    {
        public DateTime? StartDate { get; set; } //primeiro dia do mês
        public DateTime? EndDate { get; set; } //ultimo dia do mês
    }
}
