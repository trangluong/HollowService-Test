using Common.Contracts;
using Common.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Text;

namespace Common.Result.Web
{
    /// <summary>
    /// Bundles results of a web service calls with information about whether the call succeeded and error code(s).
    /// </summary>
    /// <typeparam name="T">The type of data returned from the web service.</typeparam>
    public class WebServiceResult<T> : IHttpStatusCode
    {
        /// <summary>
        /// The data returned from the web service call.
        /// </summary>        
        public T Payload { get; set; }

        /// <summary>
        /// A list of zero or more errors that were encountered by the web service.
        /// </summary>
        [JsonProperty(PropertyName = "errors")]
        public IList<WebError> WebErrors { get; set; }

        /// <summary>
        /// A helper property so that we can assign errors to this object directly
        /// from <see cref="ServiceError"/>.
        /// </summary>
        [JsonIgnore]
        public IList<ServiceError> ServiceErrors
        {
            set
            {
                Contract.Requires(value != null);

                WebErrors = value.Select(WebError.FromServiceError).ToList();
            }
        }

        /// <summary>
        /// If not null, this code is returned rather than the computed code.
        /// </summary>
        [JsonIgnore]
        public HttpStatusCode? StatusCodeOverride { get; set; }

        /// <summary>
        /// Create a new web service result with no errors.
        /// </summary>
        public WebServiceResult()
        {
            WebErrors = new List<WebError>();
        }

        /// <summary>
        /// Create a new web service result with no errors with the specified result
        /// </summary>
        public WebServiceResult(T payload)
        {
            Payload = ContractHelper.RequiresNotNull(payload);
            WebErrors = new List<WebError>();
        }

        /// <summary>
        /// Create a new web service result with an <paramref name="error"/> to return to the caller.
        /// </summary>
        /// <param name="error">The error encountered by the web service.</param>
        public WebServiceResult(WebError error) : this()
        {
            WebErrors.Add(ContractHelper.RequiresNotNull(error));
        }

        /// <summary>
        /// Create a new web service result that passes along the result of a call to a service.
        /// </summary>
        /// <param name="result">The result of a service call.</param>
        public WebServiceResult(ServiceResult<T> payload)
        {
            Payload = ContractHelper.RequiresNotNull(payload).Payload;
            WebErrors = payload.Errors.Select(WebError.FromServiceError).ToList();
        }

        /// <summary>
        /// Create a new web service result that passes along the service errors
        /// </summary>
        public WebServiceResult(ServiceError error)
        {
            ContractHelper.RequiresNotNull(error);
            WebErrors = SingletonList.Of(WebError.FromServiceError(error));
        }

        /// <summary>
        /// Indicates if this <see cref="WebServiceResult{T}"/> has no errors.
        /// </summary>
        // public bool Success => !WebErrors.Any();
        public int StatusCode => (int)HttpStatusCode;

        /// <summary>
        /// Returns <see cref="HttpStatusCode"/> related to current <see cref="WebServiceResult{T}"/>.
        /// </summary>
        [JsonIgnore]
        public HttpStatusCode HttpStatusCode =>
            StatusCodeOverride ?? (!WebErrors.Any() ? HttpStatusCode.OK : WebErrors.First().HttpStatusCode);
    }
}
