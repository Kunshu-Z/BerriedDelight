using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BerriedDelight.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        //Fields
        public string? Address { get; set; }
        public string? Content { get; set; }

        //To generate correct link and pass address
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            output.Attributes.SetAttribute("href", "mailto:" + Address);
            output.Content.SetContent(Content);
        }
    }
}
