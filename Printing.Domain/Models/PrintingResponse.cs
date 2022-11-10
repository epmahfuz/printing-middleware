namespace Printing.Domain.Models.Dto
{
    public class PrintingResponse
    {
        public PrintingResponse()
        {
            IsTransactionSuccessful = false;
        }

        public bool IsTransactionSuccessful { get; set; }
        public string CustomerReceipt { get; set; }
        public string CardType { get; set; }
        public string PaymentId { get; set; }
    }
}