using ExchangeRatesCbr.Models;

namespace ExchangeRatesCbr
{
  class Program
  {
    static async Task Main(string[] args)
    {
      await Task.Delay(TimeSpan.FromSeconds(10));

      await FetchAndSaveCurrencies();
      await FetchAndSaveExchangeRatesFromDays(30);

      while (true)
      {
        await FetchAndSaveCurrencies();
        await FetchAndSaveExchangeRatesFromDays(5);

        await Task.Delay(TimeSpan.FromDays(1));
      }
    }

    static async Task FetchAndSaveExchangeRates(DateTime date)
    {
      try
      {
        List<ExchangeRateModel> rates = (await new Cbr().GetExchangeRates(date)).ToList<ExchangeRateModel>();
        await new Db().SaveExchangeRates(rates, date);

        Console.WriteLine($"Курсы валют сохранены {date:dd.MM.yyyy}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка {ex.Message}");
      }
    }

    static async Task FetchAndSaveExchangeRatesFromDays(int days)
    {
      Console.WriteLine($"Загрузка курсов валют за последние {days} дней");

      List<DateTime> dates = (await new Db().GetExchangeRatesFromDays(days)).ToList<DateTime>();

      DateTime today = DateTime.Today;
      for (int i = 0; i < days; i++)
      {
        DateTime date = today.AddDays(-i);

        if (dates.Contains(date))
        {
          Console.WriteLine($"Курсы валют за {date:dd.MM.yyyy} уже есть в базе");

          continue;
        }

        await FetchAndSaveExchangeRates(date);
      }

      Console.WriteLine($"Курсы валют за последние {days} дней загружены");
    }

    static async Task FetchAndSaveCurrencies()
    {
      Db db = new Db();

      try
      {
        List<CurrencyModel> cbrCurrencies = (await new Cbr().GetCurrencies()).ToList<CurrencyModel>();

        if (cbrCurrencies.Count > 0)
        {
          List<string> dbCurrencies = (await db.GetCurrecies()).ToList<string>();
          await db.SaveCurrencies(cbrCurrencies, dbCurrencies);

          Console.WriteLine("\nСписок валют сохранен\n");
        }
        else
        {
          Console.WriteLine("\nСписок валют не загружен\n");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"\nОшибка {ex.Message}\n");
      }
    }
  }
}
