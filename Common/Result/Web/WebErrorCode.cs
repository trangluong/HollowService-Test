using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Result.Web
{
    public enum WebErrorCode
    {
        AccessDenied,
        AccountDisabled,
        BadRequest,
        CaptchaInvalid,
        ContentIsNotMulipart,
        CreditCardDeclined,
        CreditCardExpired,
        CreditCardInvalid,
        CurrentPasswordIsNotCorrect,
        Duplicate,
        DuplicateRecords,
        EmailAddressNotConfirmed,
        EmailNotValid,
        ExternalOperationFailed,
        ExternalServiceError,
        FailedToProcessFile,
        FailedToStoreResource,
        FieldIsInvalid,
        FieldNotSet,
        ImportInProgress,
        InvalidAcceptEncodingHeader,
        InvalidCredentials,
        NotFound,
        NotImplemented,
        PaymentDeclined,
        PaymentProvider,
        Unknown,
        UserAlreadyRegistered,
        LoginErrorInvalid,
        LoginErrorLock,
        LoginErrorInactive,
        ForgotPasswordLock,
        ForgotPasswordSendLink,
        ForgotPasswordIncorrectAnswer,
        ForgotUserNameSendLink,
        ForgotUserNameAccountNotFound,
        ForgotPasswordReset
    }
}
