using Common.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Common.Result
{
    /// <summary>
    /// A Service Result that should either contain a valid result, or ServiceError(s)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ServiceResult<T>
    {
        public T Payload { get; set; }
        public IList<ServiceError> Errors { get; }

        /// <summary>
        /// Create a successful ServiceResult
        /// </summary>
        public ServiceResult()
        {
            Errors = new List<ServiceError>();
        }

        /// <summary>
        /// Create a failure ServiceResult with the specified errors
        /// </summary>
        public ServiceResult(ServiceError serviceError, params object[] fields) : this()
        {
            Contract.Requires(serviceError != null);
            Contract.Requires(fields != null);
            AddError(serviceError, fields);
        }

        /// <summary>
        /// Create a successful ServiceResult with the specified result
        /// </summary>
        public ServiceResult(T result) : this()
        {
            Contract.Requires(result != null);
            Payload = result;
        }

        public ServiceResult<T> AddError(ServiceError serviceError, params object[] fields)
        {
            Contract.Requires(serviceError != null);
            Contract.Requires(fields != null);
            Errors.Add(fields.Any() ? new ServiceError(serviceError, fields) : serviceError);
            return this;
        }

        public ServiceResult<T> AddErrors(IEnumerable<ServiceError> serviceErrors)
        {
            Contract.Requires(serviceErrors != null);
            foreach (var serviceError in serviceErrors)
                Errors.Add(serviceError);


            return this;
        }

        /// <summary>
        /// Adds several errors to the result.
        /// </summary>
        /// <param name="errors">The errors to add.</param>
        /// <returns>The service result, for chaining.</returns>
        public ServiceResult<T> AddErrors(IList<ServiceError> errors)
        {
            Contract.Requires(errors != null);

            Errors.AddRange(errors);

            return this;
        }

        public bool HasErrors => Errors.Any();

        public bool HasNoErrors => !HasErrors;

        /// <summary>
        /// Returns true if <paramref name="errorCode"/> is among the errors.
        /// </summary>
        /// <param name="errorCode">The error being looked for.</param>
        /// <returns>True if the error is present.</returns>
        public bool HasErrorCode(ServiceErrorCode errorCode)
        {
            return Errors.FirstOrDefault(err => err.Code == errorCode) != null;
        }

        public static ServiceResult<T> AsErrorResult(ServiceResult<T> oldResult)
        {
            var result = new ServiceResult<T>();
            result.Errors.AddRange(oldResult.Errors);
            return result;
        }

        /// <summary>
        /// Filter out any errors that do not pass the given predicate.
        /// </summary>
        /// <param name="filter">function that returns true for the errors to keep.</param>
        public void FilterErrors(Func<ServiceError, bool> filter)
        {
            var filtered = Errors.Where(filter).ToList();
            Errors.Clear();
            Errors.AddRange(filtered);
        }

        /// <summary>
        /// Get a description of the first error that occured.
        /// </summary>
        public string FirstErrorText => Errors.FirstOrDefault()?.ToString();
    }
}
