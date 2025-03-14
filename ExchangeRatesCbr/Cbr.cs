using ExchangeRatesCbr.Models;
using System.Globalization;
using System.Xml.Linq;

namespace ExchangeRatesCbr
{
  public class Cbr
  {
    public async Task<IEnumerable<CurrencyModel>> GetCurrencies()
    {
      using (HttpClient httpClient = new HttpClient())
      {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        string response = await httpClient.GetStringAsync($"{Constants.CbrBaseUrl}/XML_val.asp");
        XDocument xml = XDocument.Parse(response);
        List<XElement> currenciesXElements = xml.Descendants("Item").ToList<XElement>();

        return currenciesXElements.Select(x => new CurrencyModel
        {
          CurrencyId = null,
          CbrId = x.Attribute("ID")?.Value ?? "0",
          RusName = x.Element("Name")?.Value ?? "",
          EngName = x.Element("EngName")?.Value ?? "",
          Nominal = Convert.ToInt32(x.Element("Nominal")?.Value ?? "0"),
          ParentCode = x.Element("ParentCode")?.Value ?? "0"
        });
      }
    }

    public async Task<IEnumerable<ExchangeRateModel>> GetExchangeRates(DateTime date)
    {
      using (HttpClient httpClient = new HttpClient())
      {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        string response = await httpClient.GetStringAsync($"{Constants.CbrBaseUrl}/XML_daily.asp?date_req={date.ToString("dd/MM/yyyy")}");
        XDocument xml = XDocument.Parse(response);

        DateTime.TryParseExact(xml.Root?.Attribute("Date")?.Value, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime cbrDate);
        if (cbrDate.Date != date.Date)
          throw new Exception($"Курсы валют не загружены {date:dd.MM.yyyy}");

        List<XElement> ratesXElements = xml.Descendants("Valute").ToList<XElement>();

        return ratesXElements.Select(x => new ExchangeRateModel
        {
          ExchangeRateId = null,
          Date = cbrDate,
          CbrId = x.Attribute("ID")?.Value ?? "0",
          NumCode = x.Element("NumCode")?.Value ?? "0",
          CharCode = x.Element("CharCode")?.Value ?? "0",
          Value = Convert.ToDecimal(x.Element("Value")?.Value ?? "0", CultureInfo.CurrentCulture),
          VunitRate = Convert.ToDecimal(x.Element("VunitRate")?.Value ?? "0", CultureInfo.CurrentCulture)
        });
      }
    }
  }
}
