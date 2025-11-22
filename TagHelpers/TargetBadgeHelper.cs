using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace fanaticServe.TagHelpers;

// You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
[HtmlTargetElement("TargetBadge",TagStructure =TagStructure.WithoutEndTag)]
public class TargetBadgeHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        // 右のタグを出力する       < span class="badge rounded-pill bg-light  text-dark  border  border-danger">彩香</span>
        output.TagName = "span";
        output.Attributes.SetAttribute("class", "badge rounded-pill bg-light  text-image-color  border border-image-color ");
        output.Content.SetContent("彩香");
        output.TagMode = TagMode.StartTagAndEndTag;
    }
}
