using Microsoft.AspNetCore.Razor.TagHelpers;

namespace fanaticServe.TagHelpers;

[HtmlTargetElement("MedleyBadge", TagStructure = TagStructure.WithoutEndTag)]
public class MedleyBadgeHelper : AbstractBadgeHelper
{
    public override string style()
    {
        return "badge-medley";
    }

    public override string target()
    {
        return "メドレー";
    }
}
