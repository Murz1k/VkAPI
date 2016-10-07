using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VkApi
{

    [Serializable]
    public class ResponseException : ApplicationException
    {
        public ResponseException() { }

        public ResponseException(string message) : base(message) { }

        public ResponseException(string message, Exception inner) : base(message, inner) { }

        protected ResponseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
