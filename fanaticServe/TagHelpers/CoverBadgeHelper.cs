using Microsoft.AspNetCore.Razor.TagHelpers;

namespace fanaticServe.TagHelpers;

[HtmlTargetElement("CoverBadge", TagStructure = TagStructure.WithoutEndTag)]
public class CoverBadgeHelper : AbstractBadgeHelper
{
    public override string style()
    {
        return "badge-cover";
    }

    public override string target()
    {
        return "カバー";
    }
}
