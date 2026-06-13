using Microsoft.AspNetCore.Razor.TagHelpers;

namespace fanaticServe.TagHelpers;

[HtmlTargetElement("EncoreBadge", TagStructure = TagStructure.WithoutEndTag)]
public class EncoreBadgeHelper : AbstractBadgeHelper
{
    public override string style()
    {
        return "badge-encore";
    }

    public override string target()
    {
        return "アンコール";
    }
}
