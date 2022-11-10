using MediatR;
using Printing.Domain.Models;
using Printing.Domain.Models.Dto;

namespace Payment.Command
{
    public class MakePrintingCommand : IRequest<PrintingResponse>
    {
        public PrintingDto Payment { get; set; }
        public MakePrintingCommand(PrintingDto payment)
        {
            Payment = payment;
        }
    }
}