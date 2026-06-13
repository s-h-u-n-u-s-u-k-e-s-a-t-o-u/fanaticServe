using Microsoft.AspNetCore.Razor.TagHelpers;

namespace fanaticServe.TagHelpers;

// You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
[HtmlTargetElement("TargetBadge", TagStructure = TagStructure.WithoutEndTag)]
abstract public class AbstractBadgeHelper : TagHelper
{
    abstract public string target();
    abstract public string style();

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        // 右のタグを出力する       < span class="badge rounded-pill bg-light  text-dark  border  border-danger">彩香</span>
        output.TagName = "span";
        output.Attributes.SetAttribute("class", $"badge rounded-pill bg-light  border {style()} ");
        output.Content.SetContent(target());
        output.TagMode = TagMode.StartTagAndEndTag;
    }
}
