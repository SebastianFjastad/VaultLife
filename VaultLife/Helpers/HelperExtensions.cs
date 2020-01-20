using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Mvc.Ajax;

namespace Vaultlife.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string rawHtml, string action, string controller, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            string holder = Guid.NewGuid().ToString();
            string anchor = ajaxHelper.ActionLink(holder, action, controller, routeValues, ajaxOptions, htmlAttributes).ToString();
            return MvcHtmlString.Create(anchor.Replace(holder, rawHtml));
        }

        public static MvcHtmlString RawActionLink(this HtmlHelper htmlHelper, string rawHtml, string action, string controller, object routeValues, object htmlAttributes)
        {
            string holder = Guid.NewGuid().ToString();
            string anchor = htmlHelper.ActionLink(holder, action, controller, routeValues, htmlAttributes).ToString();
            return MvcHtmlString.Create(anchor.Replace(holder, rawHtml));
        }

        /*
         * Meant to allow for the generation of anchor tags with raw html inside them, as well as passing in fragments
         * */
        public static MvcHtmlString RawActionLink(this HtmlHelper htmlHelper, string rawHtml, string action, string controller, string protocol, string hostname, string fragment, object routeValues, object htmlAttributes)
        {
            string holder = Guid.NewGuid().ToString();
            string anchor = htmlHelper.ActionLink(holder, action, controller, protocol, hostname, fragment, routeValues, htmlAttributes).ToString();
            return MvcHtmlString.Create(anchor.Replace(holder, rawHtml));
        }
    }
}
