using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Adapter_HTML
{
    public class HtmlToJsonConverter
    {
        private readonly HtmlAdapterSettings settings;
        private readonly HttpClient client;
        // IOptions<HtmlAdapterSettings> ayarları otomatik entegre etmek için kullanılıyor. Singleton yapı sayesinde aynı sınıf tekrar tekrar oluşmayacak.
        public HtmlToJsonConverter(IOptions<HtmlAdapterSettings> options, IHttpClientFactory factory)
        {
            settings = options.Value;
            client = factory.CreateClient();
        }

        public async Task<IEnumerable<TagModel>> ConvertAsync()
        {
            // Burada HTML içeriğini JSON'a dönüştürme işlemi yapılacak.
            // Örnek olarak, BaseUrl ve Endpoint kullanılarak bir HTTP isteği yapabiliriz.
            // Ardından gelen HTML içeriğini JSON formatına dönüştürebiliriz.
            // Bu örnekte basit bir dönüşüm yapıyoruz.
            string url = $"{settings.BaseUrl}{settings.Endpoint ?? ""}";
            string html = await client.GetStringAsync(url);

            // Basit bir HTML içeriğini parçalama işlemi
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            IEnumerable<HtmlNode> tags;
            if (!string.IsNullOrEmpty(settings.XPath))
            {
                tags = doc.DocumentNode.SelectNodes(settings.XPath) ?? Enumerable.Empty<HtmlNode>();
            }
            else
            {
                tags = doc.DocumentNode.Descendants();
            }

            var jsonElements = tags.Where(x => !x.Name.StartsWith("#")).Select(tag => new TagModel
            {
                TagName = tag.Name,
                InnerText = tag.InnerText.Trim(),
                Attributes = tag.Attributes.Select(attr => new AttributeModel
                {
                    Name = attr.Name,
                    Value = attr.Value
                })
            });

            return jsonElements;
        }
    }
}
