using System.Web.Mvc;
using System.Web.Routing;

namespace Micro.Wanter.Common.Extension
{
    public static class HtmlExtension
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string alt, string title, object htmlAttributes, string defaultClass = "btn btn-default")
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", alt);
            builder.MergeAttribute("title", title);
            builder.MergeAttribute("class", defaultClass);
            builder.MergeAttributes<string, object>(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }


    }
}
