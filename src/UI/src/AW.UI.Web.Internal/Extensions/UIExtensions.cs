using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AW.UI.Web.Internal.Extensions
{
    public static class UIExtensions
    {
        public static List<SelectListItem> ToSelectList<T>(this List<T> list, Expression<Func<T, string>> valueExpr, Expression<Func<T, string>> textExpr)
        {
            var items = list
            .Select(item => new SelectListItem() 
            { 
                Value = valueExpr.Compile().Invoke(item), 
                Text = textExpr.Compile().Invoke(item) 
            })
            .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }
    }
}