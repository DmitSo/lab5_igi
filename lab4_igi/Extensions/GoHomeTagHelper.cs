using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4_igi
{
    public class GoHomeTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            context.AllAttributes.TryGetAttribute("link", out TagHelperAttribute tag);
            output.Attributes.SetAttribute("href", "/Home/Index");
            output.Content.SetContent("HOME");
        }
    }
}
