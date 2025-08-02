using HtmlAgilityPack;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Adapter_KOERI
{
    public class DepremServisi
    {
        private readonly HttpClient httpClient;

        public DepremServisi(IHttpClientFactory factory)
        {
            httpClient = factory.CreateClient();
        }

        public async Task<IEnumerable<DepremModel>> DepremleriGetirAsync()
        {
            string url = "http://www.koeri.boun.edu.tr/scripts/sondepremler.asp";
            string html = await httpClient.GetStringAsync(url);

            // HTML içeriğini yüklemek için HtmlAgilityPack kullanıyoruz
            var doc = new HtmlDocument();

            // HTML içeriğini yükle
            doc.LoadHtml(html);

            // HTML içeriğinden ilk <pre> etiketini seçiyoruz
            var pre = doc.DocumentNode.SelectSingleNode("//pre");

            // Eğer <pre> etiketi bulunamazsa, hata fırlatıyoruz
            if (pre == null)
            {
                throw new Exception("Deprem verileri bulunamadı.");
            }

            // <pre> etiketinin içeriğini alıyoruz
            string preContent = pre.InnerText;

            var lines = preContent.Trim().Split("\n").Skip(6);

            var depremler = new List<DepremModel>();

            foreach (var line in lines)
            {
                var parts = Regex.Split(line.Trim(), @"\s{2,}"); // İki veya daha fazla boşlukla ayırma
                depremler.Add(new DepremModel
                {
                    TarihSaat = DateTime.Parse(parts[0]),
                    Enlem = double.Parse(parts[1], CultureInfo.InvariantCulture),
                    Boylam = double.Parse(parts[2], CultureInfo.InvariantCulture),
                    Derinlik = double.Parse(parts[3], CultureInfo.InvariantCulture),
                    Siddet = double.Parse(parts[5], CultureInfo.InvariantCulture),
                    Yer = parts[7]
                });
            }

            return depremler;
        }
    }
}
