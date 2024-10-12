using Microsoft.AspNetCore.Razor.TagHelpers;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using StudyPomo.Library.Models.Tables.StudyTaskEntities;

namespace StudyPomo.UI.Util.TagHelpers;

public class StudyTaskLinkTagHelper : TagHelper
{
    public StudyTask Task { get; set; }
    public bool Bold { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";

        // Set class attributes based on the Bold property
        string classAttribute = Bold ? "me-2 fw-semibold text-truncate" : "me-2 text-truncate";
        output.Attributes.SetAttribute("class", classAttribute);
        output.Attributes.SetAttribute("href", $"/Manage/Tasks/{Task.Id}");

        // Set popover attributes
        output.Attributes.SetAttribute("data-bs-toggle", "popover");
        output.Attributes.SetAttribute("data-bs-trigger", "hover focus");
        output.Attributes.SetAttribute("data-bs-placement", "top");
        output.Attributes.SetAttribute("data-bs-content", Task.Name);
        output.Attributes.SetAttribute("data-bs-delay", "{ \"show\": 650, \"hide\": 100 }"); // Delayed popover

        // Set the display text (truncated)
        string displayText = Task.Name;
        output.Content.SetContent(displayText);
    }
}
