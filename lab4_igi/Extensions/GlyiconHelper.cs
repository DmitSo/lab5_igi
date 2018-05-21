using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4_igi
{
    public static class GlyiconHelper
    {
        public static HtmlString Glyicon(this IHtmlHelper htmlHelper)
        {
            var result = @"<span class='input-group-addon'>
                               <i class='glyphicon glyphicon-search'></i>
                           </span>";
            return new HtmlString(result);
        }
    }
}
