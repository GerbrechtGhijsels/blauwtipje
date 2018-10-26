using System;

namespace BlauwtipjeApp.Core.Services.Tree.Decorators.Exceptions
{
    public class TreeDecorateException : Exception
    {
        public TreeDecorateException()
        {
            
        }

        public TreeDecorateException(string message)
            : base(message)
        { }

        public TreeDecorateException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
