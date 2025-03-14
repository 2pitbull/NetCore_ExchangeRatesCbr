namespace ExchangeRatesCbr.Models
{
  public class CurrencyModel
  {
    public int? CurrencyId { get; set; }
    public string CbrId { get; set; } = null!;
    public string RusName { get; set; } = null!;
    public string EngName { get; set; } = null!;
    public int Nominal { get; set; }
    public string ParentCode { get; set; } = null!;
  }
}
