using Microsoft.AspNetCore.Http;

namespace AW.Infrastructure.Http
{
    public static class HttpContextHelper
    {
        private static IHttpContextAccessor accessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            accessor = httpContextAccessor;
        }

        public static HttpContext HttpContext
        {
            get
            {
                if (accessor == null)
                    return null;

                return accessor.HttpContext;
            }
        }
    }
}