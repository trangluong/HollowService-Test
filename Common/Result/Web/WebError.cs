using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Dynamic;
using System.Net;
using Common.Contracts;
using System.Diagnostics.Contracts;

namespace Common.Result.Web
{
    public class WebError
    {
        public static readonly WebError AccessDenied = new WebError(WebErrorCode.AccessDenied, string.Empty, HttpStatusCode.Forbidden);
        public static readonly WebError AccountDisabled = new WebError(WebErrorCode.AccountDisabled, "Account Disabled", HttpStatusCode.Unauthorized);
        // public static readonly WebError AccountDisabled = new WebError(WebErrorCode.AccountDisabled, "Account Disabled", HttpStatusCode.Unauthorized);
        public static readonly WebError BadRequest = new WebError(WebErrorCode.BadRequest, string.Empty, HttpStatusCode.BadRequest);
        public static readonly WebError CaptchaInvalid = new WebError(WebErrorCode.CaptchaInvalid, "Captcha was not validated", HttpStatusCode.Conflict);
        public static readonly WebError ContentIsNotMultipart = new WebError(WebErrorCode.ContentIsNotMulipart, string.Empty, HttpStatusCode.BadRequest);
        public static readonly WebError CreditCardDeclined = new WebError(WebErrorCode.CreditCardDeclined, "Credit Card Declined", HttpStatusCode.Conflict);
        public static readonly WebError CreditCardExpired = new WebError(WebErrorCode.CreditCardExpired, "Credit Card Expired", HttpStatusCode.Conflict);
        public static readonly WebError CreditCardInvalid = new WebError(WebErrorCode.CreditCardInvalid, "Credit Card Invalid", HttpStatusCode.Conflict);
        public static readonly WebError CurrentPasswordIsNotCorrect = new WebError(WebErrorCode.CurrentPasswordIsNotCorrect, "Current password is not correct", HttpStatusCode.Forbidden);
        public static readonly WebError Duplicate = new WebError(WebErrorCode.Duplicate, "Object already exists", HttpStatusCode.BadRequest);
        public static readonly WebError DuplicateRecords = new WebError(WebErrorCode.DuplicateRecords, "Duplicate records in request", HttpStatusCode.BadRequest);
        public static readonly WebError EmailAddressNotConfirmed = new WebError(WebErrorCode.EmailAddressNotConfirmed, "Email address is not confirmed", HttpStatusCode.Forbidden);
        public static readonly WebError EmailNotValid = new WebError(WebErrorCode.EmailNotValid, "The email address is not valid", HttpStatusCode.BadRequest);
        public static readonly WebError ExternalOperationFailed = new WebError(WebErrorCode.ExternalOperationFailed, string.Empty, HttpStatusCode.InternalServerError);
        public static readonly WebError ExternalServiceError = new WebError(WebErrorCode.ExternalServiceError, string.Empty, HttpStatusCode.BadRequest);
        public static readonly WebError FailedToProcessFile = new WebError(WebErrorCode.FailedToProcessFile, "Failed To Process Uploaded File", HttpStatusCode.BadRequest, true);
        public static readonly WebError FailedToStoreResource = new WebError(WebErrorCode.FailedToStoreResource, "Failed to store resource", HttpStatusCode.InternalServerError);
        public static readonly WebError FieldIsInvalid = new WebError(WebErrorCode.FieldIsInvalid, "Field invalid", HttpStatusCode.BadRequest, true);
        public static readonly WebError FieldNotSet = new WebError(WebErrorCode.FieldNotSet, "Field not Set", HttpStatusCode.BadRequest, true);
        public static readonly WebError ImportInProgress = new WebError(WebErrorCode.ImportInProgress, "Import already in progress", HttpStatusCode.BadRequest, true);
        public static readonly WebError InternalServerError = new WebError(WebErrorCode.Unknown, string.Empty, HttpStatusCode.InternalServerError);
        public static readonly WebError InvalidAcceptEncodingHeader = new WebError(WebErrorCode.InvalidAcceptEncodingHeader, string.Empty, HttpStatusCode.BadRequest);
        public static readonly WebError InvalidCredentials = new WebError(WebErrorCode.InvalidCredentials, "Invalid credentials", HttpStatusCode.Unauthorized);
        public static readonly WebError NotFound = new WebError(WebErrorCode.NotFound, "Not Found", HttpStatusCode.NotFound, true);
        public static readonly WebError NotImplemented = new WebError(WebErrorCode.NotImplemented, string.Empty, HttpStatusCode.NotImplemented);
        public static readonly WebError PaymentDeclined = new WebError(WebErrorCode.PaymentDeclined, "Payment Declined", HttpStatusCode.Conflict);
        public static readonly WebError PaymentProvider = new WebError(WebErrorCode.PaymentProvider, "Payment Provider Error", HttpStatusCode.InternalServerError, true);
        public static readonly WebError LoginErrorInvalid = new WebError(WebErrorCode.LoginErrorInvalid, "Username or Password is incorrect.", HttpStatusCode.InternalServerError, true);
        public static readonly WebError LoginErrorLock = new WebError(WebErrorCode.LoginErrorLock, "Account is now locked. Please try again in an hour.", HttpStatusCode.InternalServerError, true);
        public static readonly WebError LoginErrorInactive = new WebError(WebErrorCode.LoginErrorInactive, "Account is now inactive", HttpStatusCode.InternalServerError, true);
        public static readonly WebError ForgotPasswordLock = new WebError(WebErrorCode.ForgotPasswordLock, "Your account has been locked after 3 failed attemps. Please retry after one hour.", HttpStatusCode.InternalServerError, true);
        // public static readonly WebError ForgotPasswordLock = new WebError(WebErrorCode.ForgotPasswordLock, "Your account has been locked after 3 failed attemps. Please retry after one hour.", HttpStatusCode.InternalServerError, true);
        public static readonly WebError InvalidGrant = new WebError(WebErrorCode.InvalidCredentials, "Invalid grant type.", HttpStatusCode.InternalServerError, true);

        static readonly IDictionary<ServiceErrorCode, WebError> ErrorsMap =
            new Dictionary<ServiceErrorCode, WebError>
            {
                [ServiceError.AccessDenied.Code] = AccessDenied,
                [ServiceError.AccountDisabled.Code] = AccountDisabled,
                [ServiceError.CreditCardDeclined.Code] = CreditCardDeclined,
                [ServiceError.CreditCardExpired.Code] = CreditCardExpired,
                [ServiceError.CreditCardInvalid.Code] = CreditCardInvalid,
                [ServiceError.CurrentPasswordIsNotCorrect.Code] = CurrentPasswordIsNotCorrect,
                [ServiceError.Duplicate.Code] = Duplicate,
                [ServiceError.DuplicateRecords.Code] = DuplicateRecords,
                [ServiceError.EmailAddressNotConfirmed.Code] = AccessDenied,
                [ServiceError.EmailAddressNotConfirmed.Code] = EmailAddressNotConfirmed,
                [ServiceError.FailedToProcessFile.Code] = FailedToProcessFile,
                [ServiceError.FailedToStoreResource.Code] = FailedToStoreResource,
                [ServiceError.FieldIsInvalid.Code] = FieldIsInvalid,
                [ServiceError.ImportInProgress.Code] = ImportInProgress,
                [ServiceError.InvalidAcceptEncodingHeader.Code] = InvalidAcceptEncodingHeader,
                [ServiceError.NotFound.Code] = NotFound,
                [ServiceError.PaymentDeclined.Code] = PaymentDeclined,
                [ServiceError.Unknown.Code] = InternalServerError
            };

        /// <summary>
        /// Web error code
        /// </summary>
        public WebErrorCode Code { get; }

        /// <summary>
        /// Message associated with error code returned to client
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Http status code associated with error code returned to client
        /// </summary>
        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; }

        public ExpandoObject MessageFields { get; }

        bool UseServiceErrorMessage { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>S
        /// <param name="message"></param>
        /// <param name="httpStatusCode"></param>
        /// <param name="useServiceErrorMessage">If true, then the service error message will be used</param>
        public WebError(WebErrorCode code, string message, HttpStatusCode httpStatusCode, bool useServiceErrorMessage = false)
        {
            Code = code;
            Message = ContractHelper.RequiresNotNull(message);
            HttpStatusCode = httpStatusCode;
            UseServiceErrorMessage = useServiceErrorMessage;
            MessageFields = new ExpandoObject();
        }

        public WebError(string message)
        {
            Code = WebErrorCode.BadRequest;
            Message = ContractHelper.RequiresNotNull(message);
            HttpStatusCode = HttpStatusCode.BadRequest;
            UseServiceErrorMessage = false;
            MessageFields = new ExpandoObject();
        }

        public static WebError FromServiceError(ServiceError serviceError)
        {
            Contract.Requires(serviceError != null);
            if (!ErrorsMap.ContainsKey(serviceError.Code))
            {
                throw new InvalidOperationException($"ServiceError {serviceError.Code} is not mapped to WebError");
            }

            return ErrorsMap[serviceError.Code].WithMessageFromServiceError(serviceError);
        }

        public override string ToString() => $"{Code}: {Message}";

        WebError WithMessageFromServiceError(ServiceError serviceError)
        {
            if (!UseServiceErrorMessage) return this;
            var error = new WebError(Code, serviceError.Message, HttpStatusCode);
            var fieldsAsDict = (IDictionary<string, object>)error.MessageFields;
            foreach (var field in serviceError.MessageFields)
                fieldsAsDict.Add(field);


            return error;
        }
    }
}
