using System;

namespace Finbourne.Configuration.Sdk.Extensions
{
    /// <summary>
    /// An exception thrown when missing config parameters
    /// </summary>
    public class MissingConfigException : Exception
    {
        /// <inheritdoc />
        public MissingConfigException(string message) : base(message)
        {
        }
    }
}