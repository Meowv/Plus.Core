#if NETCOREAPP3_1

using JetBrains.Annotations;
using Plus;

namespace Microsoft.AspNetCore.Http
{
    public static class PlusHttpRequestExtensions
    {
        private const string RequestedWithHeader = "X-Requested-With";
        private const string XmlHttpRequest = "XMLHttpRequest";

        public static bool IsAjax([NotNull] this HttpRequest request)
        {
            Check.NotNull(request, nameof(request));

            if (request.Headers == null)
            {
                return false;
            }

            return request.Headers[RequestedWithHeader] == XmlHttpRequest;
        }

        public static bool CanAccept([NotNull] this HttpRequest request, [NotNull] string contentType)
        {
            Check.NotNull(request, nameof(request));
            Check.NotNull(contentType, nameof(contentType));

            return request.Headers["Accept"].ToString().Contains(contentType);
        }
    }
}

#endif