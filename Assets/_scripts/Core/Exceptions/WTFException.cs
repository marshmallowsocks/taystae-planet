using System;

public class WTFException : Exception
{
    private string message;

    //Non fatal - a fixable error that shouldn't happen, but won't really kill the game
    //Fatal - Game destroyer
    public enum Severity
    {
        NON_FATAL,
        FATAL
    };

    private Severity wtfSeverity;

    public Severity WtfSeverity
    {
        get
        {
            return wtfSeverity;
        }
    }

    public override string Message
    {
        get
        {
            return message;
        }
    }

    public WTFException()
    {
        wtfSeverity = Severity.NON_FATAL;
        message = GameStrings.NON_FATAL;
    }

    public WTFException(string message, Severity severity) : base(message)
    {
        this.wtfSeverity = severity;
        this.message = message;
    }

    public WTFException(string message, Severity severity, Exception inner) : base(message, inner)
    {
        this.wtfSeverity = severity;
        this.message = message;
    }
}
