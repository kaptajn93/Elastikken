using System;
using System.Runtime.Serialization;

namespace Elastikken.Parsing
{
    [Serializable]
    public class ParsingException : Exception
    {
        public ParsingException()
        {
        }

        public ParsingException(string message)
            : base(message)
        {
        }

        public ParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ParsingException(string fileName, int elementIndex, Exception inner) : base(BuildMessage(fileName, string.Empty, elementIndex, inner), inner)
        {
        }

        public ParsingException(string fileName, string message, int elementIndex, Exception inner)
            : base(BuildMessage(fileName, message, elementIndex, inner), inner)
        {
        }

        private static string BuildMessage(string fileName, string message, int elementIndex, Exception innerException)
        {
            return
                string.Format(
                    "Error parsing file: \"{0}\", {1} at element with index {2}, the error was: {3}",
                    fileName, message, elementIndex, innerException.Message);
        }

        protected ParsingException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}