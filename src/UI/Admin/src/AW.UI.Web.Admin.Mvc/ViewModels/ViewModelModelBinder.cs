using AW.SharedKernel.JsonConverters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace AW.UI.Web.Admin.Mvc.ViewModels
{
    public class ViewModelModelBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var viewModel = BuildViewModel(bindingContext);

            bindingContext.Result = ModelBindingResult.Success(viewModel);

            return Task.CompletedTask;
        }

        protected virtual T? BuildViewModel(ModelBindingContext bindingContext)
        {
            var json = BuildJson(bindingContext.HttpContext.Request.Form);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new NullableDateTimeConverter());
            options.Converters.Add(new DateTimeConverter());

            var viewModel = JsonSerializer.Deserialize<T>(
                json,
                options
            );

            return viewModel;
        }

        private string BuildJson(IFormCollection form)
        {
            var json = new Dictionary<string, object>();

            //Simple properties
            foreach (var item in form.Where(x => !x.Key.Contains('.')))
            {
                json.Add(item.Key, item.Value.ToString());
            }

            //Complex properties
            var keys = form.Keys
                .Where(_ => _.Contains('.'))
                .Select(_ => Parts(_)[0])
                .DistinctBy(_ => _)
                .ToList();

            foreach (var key in keys)
            {
                if (key.Contains('['))
                {
                    var arrayName = key[..key.IndexOf('[')];
                    var array = AddItemsForArray(form, arrayName);
                    if (!json.ContainsKey(Name(arrayName)))
                        json.Add(Name(arrayName), array);
                }
                else
                {
                    Dictionary<string, object> dict = new();
                    AddItemsForPath(form, dict, key);
                    json.Add(key, dict);
                }
            }

            return JsonSerializer.Serialize(json, new JsonSerializerOptions { WriteIndented = true });
        }

        protected virtual object GetValueForKey(string key, StringValues value)
        {
            return value.ToString();
        }

        private void AddItemsForPath(IFormCollection form, Dictionary<string, object> dict, string path)
        {
            var itemsForPath = form.Where(x => Path(x.Key) == path).ToList();
            itemsForPath.ForEach(x =>
            {
                if (!dict.ContainsKey(Name(x.Key)))
                {
                    var value = GetValueForKey(Name(x.Key), x.Value);
                    dict.Add(Name(x.Key), value);
                }
            });

            var paths = FindPaths(form, path);
            paths.ForEach(_ =>
            {
                if (_.Contains('['))
                {
                    var arrayName = _[.._.IndexOf('[')];
                    var array = AddItemsForArray(form, arrayName);
                    if (!dict.ContainsKey(Name(arrayName)))
                        dict.Add(Name(arrayName), array);
                }
                else
                {
                    var newDict = new Dictionary<string, object>();
                    AddItemsForPath(form, newDict, _);

                    var parts = Parts(_);
                    var propertyKey = parts[^1]; // Get parent object name

                    if (newDict.Count > 0)
                        dict.Add(propertyKey, newDict);
                }
            });
        }

        private List<object> AddItemsForArray(IFormCollection form, string arrayName)
        {
            var result = new List<object>();

            var arrayItems = form.Where(x => ArrayPath(x.Key) == arrayName)
                .Select(_ => ArrayItem(_.Key))
                .DistinctBy(_ => _)
                .ToList();

            foreach (var item in arrayItems)
            {
                var items = form.Where(_ => Path(_.Key) == item).ToList();
                var dict = new Dictionary<string, object>();
                items.ForEach(_ => dict.Add(Name(_.Key), _.Value.ToString()));

                var paths = FindPaths(form, item);
                paths.ForEach(_ =>
                {
                    if (Name(_).Contains('['))
                    {

                    }
                    else
                    {
                        var newDict = new Dictionary<string, object>();
                        AddItemsForPath(form, newDict, _);

                        var parts = Parts(_);
                        var propertyKey = parts[^1]; // Get parent object name

                        if (newDict.Count > 0)
                            dict.Add(propertyKey, newDict);
                    }
                });

                result.Add(dict);
            }

            return result;
        }

        private static List<string> FindPaths(IFormCollection form, string key)
        {
            var level = key.Split('.').Length + 1;

            var paths = new List<string>();
            foreach (var item in form.Where(_ => Path(_.Key) != key && Path(_.Key).StartsWith(key)))
            {
                var path = PathForLevel(item.Key, level);
                if (!string.IsNullOrEmpty(path) && !paths.Contains(path))
                    paths.Add(path);
            }

            return paths.OrderBy(x => x).ToList();
        }

        private static List<string> Parts(string key)
        {
            return key.Split('.').ToList();
        }

        private static string Path(string key)
        {
            if (key.Contains('.'))
                return key[..key.LastIndexOf('.')];

            return string.Empty;
        }

        private static string? ArrayPath(string key)
        {
            if (key.Contains('['))
            {
                var arrayPath = key[..key.IndexOf('[')];
                return arrayPath;
            }

            return string.Empty;
        }

        private static string ArrayItem(string key)
        {
            var parts = Parts(key);
            var arrayItem = new List<string>();

            foreach (var part in parts)
            {
                arrayItem.Add(part);
                if (part.Contains('['))
                    break;
            }

            return string.Join('.', arrayItem);
        }

        private static string PathForLevel(string key, int level)
        {
            var partCount = key.Split('.').Length;

            if (partCount - 1 == level)
            {
                var index = -1;
                for (var i = 0; i < level; i++)
                {
                    index = key.IndexOf('.', index + 1);
                }

                return key[..index];
            }

            return string.Empty;
        }

        private static string Name(string key)
        {
            if (key.Contains('.'))
                return key[(key.LastIndexOf('.') + 1)..];

            return key;
        }
    }
}
