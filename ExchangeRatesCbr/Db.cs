using Dapper;
using ExchangeRatesCbr.Models;
using Npgsql;

namespace ExchangeRatesCbr
{
  public class Db
  {
    public async Task SaveCurrencies(IEnumerable<CurrencyModel> cbrCurrencies, IEnumerable<string> dbCurrencies)
    {
      using (NpgsqlConnection connection = new NpgsqlConnection(Constants.connectionString))
      {
        connection.Open();

        string sql = @"
          insert into Currency
          (CbrId, RusName, EngName, Nominal, ParentCode)
          values
          (@CbrId, @RusName, @EngName, @Nominal, @ParentCode)";

        foreach (CurrencyModel currency in cbrCurrencies)
        {
          if (dbCurrencies.Contains(currency.CbrId))
            continue;

          await connection.ExecuteAsync(sql, new
          {
            currency.CbrId,
            currency.RusName,
            currency.EngName,
            currency.Nominal,
            currency.ParentCode
          });
        }
      }
    }

    public async Task<IEnumerable<string>> GetCurrecies()
    {
      using (NpgsqlConnection connection = new NpgsqlConnection(Constants.connectionString))
      {
        connection.Open();

        string sql = @"
          select CbrId
          from Currency";

        return await connection.QueryAsync<string>(sql);
      }
    }

    public async Task SaveExchangeRates(IEnumerable<ExchangeRateModel> rates, DateTime date)
    {
      using (NpgsqlConnection connection = new NpgsqlConnection(Constants.connectionString))
      {
        connection.Open();

        string sql = @"
          insert into ExchangeRate
          (Date, CbrId, NumCode, CharCode, Value, VunitRate)
          values
          (@Date, @CbrId, @NumCode, @CharCode, @Value, @VunitRate)";

        foreach (ExchangeRateModel rate in rates)
        {
          await connection.ExecuteAsync(sql, new
          {
            Date = date,
            rate.CbrId,
            rate.NumCode,
            rate.CharCode,
            rate.Value,
            rate.VunitRate
          });
        }
      }
    }

    public async Task<IEnumerable<DateTime>> GetExchangeRatesFromDays(int days)
    {
      using (NpgsqlConnection connection = new NpgsqlConnection(Constants.connectionString))
      {
        connection.Open();

        string sql = @"
          select distinct date
          from ExchangeRate
          order by date desc
          limit @days";

        return await connection.QueryAsync<DateTime>(sql, new { days });
      }
    }
  }
}
