using System.Net.Http;
using System.Threading.Tasks;

public class CurrencyHelper
{
    public static async Task<string> GetExchangeRates()
    {
        using (HttpClient client = new HttpClient())
        {
            var response = await client.GetStringAsync("http://any.kur.com/kurlar");
            return response;
        }
    }
}
