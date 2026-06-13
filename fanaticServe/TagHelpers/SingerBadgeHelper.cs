using Microsoft.AspNetCore.Razor.TagHelpers;

namespace fanaticServe.TagHelpers;

[HtmlTargetElement("SingerBadge", TagStructure = TagStructure.WithoutEndTag)]
public class SingerBadgeHelper : AbstractBadgeHelper
{
    public override string style()
    {
        return "badge-singer";
    }

    public override string target()
    {
        return "彩香";
    }
}
