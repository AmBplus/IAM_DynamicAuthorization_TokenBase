using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Base.Shared;
using Base.AspCore.Infrastructure.Messages;

namespace Base.AspCore.Infrastructure.Messages;

/// <summary>
///     Version 3.0
/// </summary>
public static class Utility
{
    public static bool AddMessage
    (ITempDataDictionary tempData,
        MessageType type, string? message)
    {
       

        if (message == null) return false;
        message =
           message.Slugify(); // Need Change 

        List<string>? list;

        var tempDataItems =
            tempData[type.ToString()] as
                IList<string>;

        if (tempDataItems == null)
        {
            list = new List<string>();
        }
        else
        {
            list =
                tempDataItems as
                    List<string>;

            if (list == null) list = tempDataItems.ToList();
        }

        tempData[type.ToString()] = list;
        // **************************************************

        if (list.Contains(message)) return false;

        list.Add(message);

        return true;
    }
}
