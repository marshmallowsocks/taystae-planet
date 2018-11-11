using UnityEngine;
using System.Diagnostics;
using System.Reflection;
using System;

//Mono 3.5 doesn't support the CallerInfo API
//Implement CallerMemberName ourselves

namespace System.Runtime.CompilerServices
{
    sealed class CallerMemberNameAttribute : Attribute
    {

    }
}

public class Logger
{
    
    private static void Message(string messageType, string message, string caller)
    {
        StackTrace stack = new StackTrace();
        string methodName = caller;
        MethodBase callingClassParent;
        string callingClassParentType;
        string callingClassMethod;
        
        //Frame 0 = Logger.Message
        //Frame 1 = Logger.<MessageType>
        //Frame 2 = Actual method we need
        //Frame 3 = Methods calling parent
        string callingClass = stack.GetFrame(2).GetMethod().DeclaringType.Name;
        string logString;

        try
        {
            callingClassParent = stack.GetFrame(3).GetMethod();
            callingClassParentType = callingClassParent.DeclaringType.Name;
            callingClassMethod = callingClassParent.Name;
        }
        catch
        {
            callingClassParentType = GameStrings.NOT_AVAILABLE;
            callingClassMethod = GameStrings.NOT_AVAILABLE;
        }

        logString = String.Format(GameStrings.LOG_STRING, callingClass, methodName, message, callingClassParentType, callingClassMethod);

        switch (messageType)
        {
            case GameStrings.WTF:
                UnityEngine.Debug.LogError(logString);
                break;
            case GameStrings.WARNING:
                UnityEngine.Debug.LogWarning(logString);
                break;
            case GameStrings.INFO:
                UnityEngine.Debug.Log(logString);
                break;
        }
    }

    public static void WTF<T>(T wtfMessage, WTFException.Severity severity, [System.Runtime.CompilerServices.CallerMemberName] string caller = "")
    {
        Message(GameStrings.WTF, wtfMessage.ToString(), caller);
        if (severity.Equals(WTFException.Severity.FATAL))
        {
            throw new WTFException(wtfMessage.ToString(), severity);
        }
    }

    public static void Warn<T>(T warnMessage, [System.Runtime.CompilerServices.CallerMemberName] string caller = "")
    {
        Message(GameStrings.WARNING, warnMessage.ToString(), caller);
    }

    public static void Info<T>(T infoMessage, [System.Runtime.CompilerServices.CallerMemberName] string caller = "")
    {
        Message(GameStrings.INFO, infoMessage.ToString(), caller);
    }
}
