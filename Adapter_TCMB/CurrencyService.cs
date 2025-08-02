using System.Globalization;
using System.Xml;

namespace Adapter_TCMB
{
    public class CurrencyService
    {
        private readonly HttpClient client;
        public CurrencyService(IHttpClientFactory factory)
        {
            client = factory.CreateClient();
        }

        public async Task<IEnumerable<CurrencyModel>> GetCurrenciesAsync()
        {
            var xmlContent = await client.GetStringAsync("https://www.tcmb.gov.tr/kurlar/today.xml");

            var xmlDoc = new XmlDocument();

            // Parse the XML content to extract currency data
            xmlDoc.LoadXml(xmlContent);

            // Select the Currency nodes from the XML document
            var currencyNodes = xmlDoc.SelectNodes("//Currency");

            var currencies = new List<CurrencyModel>();
            if (currencyNodes != null)
            {
                foreach (XmlNode currencyNode in currencyNodes)
                {
                    currencies.Add(new CurrencyModel
                    {
                        Code = currencyNode.Attributes?["CurrencyCode"]?.Value ?? string.Empty,
                        Name = currencyNode.SelectSingleNode("CurrencyName")?.InnerText.Trim() ?? string.Empty,
                        ForexBuying = toDecimal(currencyNode.SelectSingleNode("ForexBuying")?.InnerText),
                        ForexSelling = toDecimal(currencyNode.SelectSingleNode("ForexSelling")?.InnerText),
                        BanknoteBuying = toDecimal(currencyNode.SelectSingleNode("BanknoteBuying")?.InnerText),
                        BanknoteSelling = toDecimal(currencyNode.SelectSingleNode("BanknoteSelling")?.InnerText),
                        CrossRateUSD = toDecimal(currencyNode.SelectSingleNode("CrossRateUSD")?.InnerText),
                        CrossRateOther = toDecimal(currencyNode.SelectSingleNode("CrossRateOther")?.InnerText)
                    });
                }
            }

            return currencies;
        }

        private decimal? toDecimal(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            if (decimal.TryParse(value, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }
            return null;
        }
    }
}
