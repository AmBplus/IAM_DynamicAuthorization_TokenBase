using Base.AspCore.Infrastructure.Messages;
namespace Base.AspCore.Infrastructure.Messages;

/// <summary>
///     Version 3.0
/// </summary>
public interface IMessageHandler
{
    bool AddPageError(string? message);

    bool AddPageWarning(string? message);

    bool AddPageSuccess(string? message);
    bool AddPageInformation(string? message);


    bool AddToastError(string? message);

    bool AddToastWarning(string? message);

    bool AddToastSuccess(string? message);
    bool AddToastInformation(string? message);


    bool AddMessage(MessageType type, string? message);
    public bool AddRangeToastErrors( IEnumerable<string> messages);
    public bool AddRangeToastSuccess( IEnumerable<string> messages);
    public bool AddRangeToastWarning( IEnumerable<string> messages);
    public bool AddRangeToastInformation( IEnumerable<string> messages);
    public bool AddRangePageErrors( IEnumerable<string> messages);
    public bool AddRangePageSuccess( IEnumerable<string> messages);
    public bool AddRangePageInformation( IEnumerable<string> messages);
    public bool AddRangePageWarning( IEnumerable<string> messages);
}
