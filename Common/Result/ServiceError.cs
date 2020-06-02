using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Linq;

namespace Common.Result
{
    public class ServiceError
    {
        public static readonly ServiceError AccessDenied = new ServiceError(ServiceErrorCode.AccessDenied, "Access denied");
        public static readonly ServiceError AccountDisabled = new ServiceError(ServiceErrorCode.AccountDisabled, "Account is disabled");
        public static readonly ServiceError CreditCardDeclined = new ServiceError(ServiceErrorCode.CreditCardDeclined, "Credit Card Declined");
        public static readonly ServiceError CreditCardExpired = new ServiceError(ServiceErrorCode.CreditCardExpired, "Credit Card Expired");
        public static readonly ServiceError CreditCardInvalid = new ServiceError(ServiceErrorCode.CreditCardInvalid, "Credit Card Invalid");
        public static readonly ServiceError CurrentPasswordIsNotCorrect = new ServiceError(ServiceErrorCode.CurrentPasswordIsNotCorrect, "Current password is not correct");
        public static readonly ServiceError Duplicate = new ServiceError(ServiceErrorCode.Duplicate, "Object already exists");
        public static readonly ServiceError DuplicateRecords = new ServiceError(ServiceErrorCode.DuplicateRecords, "Duplicate records exist in request");
        public static readonly ServiceError EmailAddressNotConfirmed = new ServiceError(ServiceErrorCode.EmailAddressNotConfirmed, "Email address is not confirmed");
        public static readonly ServiceError FailedToProcessFile = new ServiceError(ServiceErrorCode.FailedToProcessFile, "Failed to process file. Error message: {0}");
        public static readonly ServiceError FailedToStoreResource = new ServiceError(ServiceErrorCode.FailedToStoreResource, "Failed to store resource with key = {0}");
        public static readonly ServiceError FieldIsInvalid = new ServiceError(ServiceErrorCode.FieldIsInvalid, "The field: {0} is invalid with value: {1}");
        public static readonly ServiceError ImportInProgress = new ServiceError(ServiceErrorCode.ImportInProgress, "An import is already in progress");
        public static readonly ServiceError InvalidAcceptEncodingHeader = new ServiceError(ServiceErrorCode.InvalidAcceptEncodingHeader, "Invalid Accept-Encoding header. Should contain gzip or deflate");
        public static readonly ServiceError NotFound = new ServiceError(ServiceErrorCode.NotFound, "The requested item was not found: {0}");
        public static readonly ServiceError PaymentDeclined = new ServiceError(ServiceErrorCode.PaymentDeclined, "Payment declined");
        public static readonly ServiceError Unknown = new ServiceError(ServiceErrorCode.Unknown, "Unknown error");
        public static readonly ServiceError UserAlreadyRegistered = new ServiceError(ServiceErrorCode.UserAlreadyRegistered, "User already registered");

        public static readonly ServiceError MissingIdField = new ServiceError(ServiceErrorCode.MissingIdField, "The Id field cannot be NULL.");

        #region PCA Address Error
        public static readonly ServiceError PcaBlankSearchAddress = new ServiceError(ServiceErrorCode.BadRequest, "The Search address cannot be blank or too short.");
        #endregion

        public ServiceErrorCode Code { get; }
        public string Message { get; }
        public IDictionary<string, object> MessageFields { get; }
        public static IDictionary<ServiceErrorCode, string[]> FieldNames { get; set; } = new Dictionary<ServiceErrorCode, string[]>
        {
            { ServiceErrorCode.FailedToStoreResource, new [] {"ResourceKey"} },
            { ServiceErrorCode.FieldIsInvalid, new [] {"FieldName", "Value"} }
        };

        private ServiceError(ServiceErrorCode code, string message)
        {
            Contract.Requires(message != null);
            Code = code;
            Message = message;
            MessageFields = new Dictionary<string, object>();
        }

        [Obsolete("use " + nameof(CreateFrom))]
        public ServiceError(ServiceError baseError, params object[] fields)
        {
            Code = baseError.Code;
            if (fields.Any())
            {
                MessageFields = new Dictionary<string, object>();
                Message = string.Format(baseError.Message, fields);
                if (!FieldNames.TryGetValue(Code, out var names)) return;
                var entries = names.Zip(fields, (n, f) => new KeyValuePair<string, object>(n, f));
                foreach (var entry in entries)
                    MessageFields.Add(entry);

            }
            else
            {
                MessageFields = baseError.MessageFields;
                Message = baseError.Message;
            }
        }

        public ServiceError CreateFrom(params object[] fields)
        {
            return new ServiceError(this, fields);
        }

        public override string ToString() => $"{Code}: {Message}";
    }
}
