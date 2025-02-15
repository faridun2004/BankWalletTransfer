using Bank.Models;
using MediatR;

namespace Bank.UseCases.Commands
{
    public class TransferByPhoneCommand: IRequest<Guid>
    {
        public string PhoneNumber { get; set; }  
        public Guid SenderWalletId { get; set; } 
        public double Amount { get; set; }       
        public WalletStatus Currency { get; set; } 
    }
}
