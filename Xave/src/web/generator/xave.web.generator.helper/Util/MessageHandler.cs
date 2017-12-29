using System;
using System.Diagnostics;

public static class MessageHandler
{
    public static string GetErrorMessage(Exception e, string additionalMessage = "")
    {
        string errorMessage = string.Format("ERROR: {0}, \r\n \r\nInnerException: {1}, \r\n \r\nStackTrace: {2}",
                    e.Message,
                    e.InnerException != null ? e.InnerException.Message : string.Empty,
                    e.StackTrace);

        return string.IsNullOrEmpty(additionalMessage) ? errorMessage : errorMessage + additionalMessage;
    }

    public static string GenerateResponseMessage(string str)
    {
        string retVal = string.Format("<root>{0}</root>", str);

        //Debug.WriteLine(retVal);

        return retVal;
    }
}
