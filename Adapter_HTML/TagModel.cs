namespace Adapter_HTML
{
    public class TagModel
    {
        public string TagName { get; set; } = null!;
        public string InnerText { get; set; } = null!;
        public IEnumerable<AttributeModel> Attributes { get; set; } = [];

    }

    public class AttributeModel
    {
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}