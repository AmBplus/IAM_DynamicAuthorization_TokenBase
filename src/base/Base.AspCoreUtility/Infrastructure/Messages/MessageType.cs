namespace Base.AspCore.Infrastructure.Messages;

/// <summary>
///     Version 3.0
/// </summary>
public enum MessageType : byte
{
    PageError,
    PageWarning,
    PageSuccess,
    PageInformation,

    ToastError,
    ToastWarning,
    ToastSuccess,
    ToastInformation
}
