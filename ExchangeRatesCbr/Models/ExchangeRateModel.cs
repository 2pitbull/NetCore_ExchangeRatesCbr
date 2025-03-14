namespace ExchangeRatesCbr.Models
{
  public class ExchangeRateModel
  {
    public int? ExchangeRateId { get; set; }
    public DateTime Date { get; set; }
    public string CbrId { get; set; } = null!;
    public string NumCode { get; set; } = null!;
    public string CharCode { get; set; } = null!;
    public decimal Value { get; set; }
    public decimal VunitRate { get; set; }
  }
}
