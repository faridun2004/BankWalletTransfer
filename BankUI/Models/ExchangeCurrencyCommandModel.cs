using System.ComponentModel.DataAnnotations;

namespace BankUI.Models
{
    public class ExchangeCurrencyCommandModel
    {
        [Required(ErrorMessage = "Укажите ID кошелька отправителя")]
        public string WalletId { get; set; }  // Теперь строка

        [Required(ErrorMessage = "Укажите ID кошелька получателя")]
        public string WalletSentId { get; set; }  // Теперь строка

        [Required(ErrorMessage = "Укажите сумму перевода")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Сумма должна быть больше 0")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Выберите валюту отправителя")]
        public int CurrencyFrom { get; set; }

        [Required(ErrorMessage = "Выберите валюту получателя")]
        public int CurrencyTo { get; set; }
    }

}
