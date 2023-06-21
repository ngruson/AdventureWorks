using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace AW.UI.Web.Admin.Mvc.Extensions;

public static class UIExtensions
{
    public static List<SelectListItem> ToSelectList<T>(this List<T> list, Expression<Func<T, string?>> valueExpr, Expression<Func<T, string?>> textExpr, bool addSelectItem = true)
    {
        var items = list
        .Select(item => new SelectListItem()
        {
            Value = valueExpr.Compile().Invoke(item),
            Text = textExpr.Compile().Invoke(item)
        })
        .ToList();

        if (addSelectItem)
        {
            var selectItem = new SelectListItem() { Value = "", Text = "--Select--", Selected = true };
            items.Insert(0, selectItem);
        }

        return items;
    }
}
