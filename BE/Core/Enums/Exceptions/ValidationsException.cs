using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GB.Core
{
    /// <summary>
    /// Represent errors that is occur during business entity validation.
    /// </summary>
    [Serializable]
    public class ValidationsException : Exception
    {
        /// <summary>
        /// Gets set of validation results of business entity that being validated.
        /// </summary>
        public IEnumerable<ValidationResult> ValidationResults { get; private set; }

        /// <summary>
        /// Gets Business Entity Name that has invalid information.
        /// </summary>
        public string EntityName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationsException"/> class.
        /// </summary>
        public ValidationsException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationsException"/> class with a specified business entity name and set of validation results.
        /// </summary>
        /// <param name="entityName">business entity name that has invalid information.</param>
        /// <param name="validationResults">set of validation results.</param>
        public ValidationsException(string entityName, IEnumerable<ValidationResult> validationResults)
            : this(string.Format("[{0}] Entity contains invalid information.", entityName))
        {
            this.ValidationResults = validationResults;
            this.EntityName = entityName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationsException"/>  class with a specified business entity name and set of validation messages.
        /// </summary>
        /// <param name="entityName">business entity name that has invalid information.</param>
        /// <param name="validationResults">set of validation messages.</param>
        public ValidationsException(string entityName, IEnumerable<string> validationResults)
            : this(string.Format("[{0}] Entity contains invalid information.", entityName))
        {
            ValidationResults = validationResults.Select(obj => new ValidationResult(obj));
            EntityName = entityName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationsException"/> class with a specified error message, business entity name and set of validation results.
        /// </summary>
        /// <param name="message">custom message that describes the error.</param>
        /// <param name="entityName">business entity name that has invalid information.</param>
        /// <param name="validationResults">set of validation results.</param>
        public ValidationsException(string message, string entityName, IEnumerable<ValidationResult> validationResults)
            : base(message)
        {
            this.EntityName = entityName;
            this.ValidationResults = validationResults;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationsException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ValidationsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationsException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ValidationsException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationsException"/> class with a specified error message, business entity name, set of validation result and the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">custom error message that explains the reason for the exception.</param>
        /// <param name="entityName">business entity name that has invalid information.</param>
        /// <param name="validationResults">set of validation results.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ValidationsException(string message, string entityName, IEnumerable<ValidationResult> validationResults, Exception inner)
            : base(message, inner)
        {
            this.EntityName = entityName;
            this.ValidationResults = validationResults;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationsException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected ValidationsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            EntityName = info.GetString(nameof(EntityName));
            ValidationResults = info.GetValue(nameof(ValidationResults), typeof(IEnumerable<ValidationResult>)) as IEnumerable<ValidationResult>;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(EntityName), EntityName);
            info.AddValue(nameof(ValidationResults), ValidationResults, typeof(IEnumerable<ValidationResult>));
        }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("Message : " + this.Message);

            if (ValidationResults != null)
                foreach (var mess in ValidationResults)
                    strBuilder.Append(Environment.NewLine + mess.ErrorMessage); 

            return strBuilder.ToString();
        }
    }
}
