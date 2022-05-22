using System.Collections.Generic;
using System.Linq;

namespace MqttTopicBuilder.Common
{
    /// <summary>
    /// Custom value object implementation based on the one provided by Microsoft
    /// see: https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects#value-object-implementation-in-c
    /// </summary>
    /// <remarks>
    /// Be sure that the classes inheriting from this one are immutable and equals by
    /// value and not by identity
    /// </remarks>
    public abstract class ValueObject
    {
        /// <summary>
        /// Checks whether or not two <see cref="ValueObject"/> are equals
        /// </summary>
        /// <param name="left">First value object</param>
        /// <param name="right">Second value object</param>
        /// <returns>True if they are equals by value</returns>
        public static bool operator ==(ValueObject left, ValueObject right)
            => EqualOperator(left, right);

        /// <summary>
        /// Checks whether or not two <see cref="ValueObject"/> are not equals
        /// </summary>
        /// <param name="left">First value object</param>
        /// <param name="right">Second value object</param>
        /// <returns>True if they are not equals by value</returns>
        public static bool operator !=(ValueObject left, ValueObject right)
            => NotEqualOperator(left, right);

        /// <summary>
        /// Define a custom equality check for value objects
        /// </summary>
        /// <param name="left">First value object</param>
        /// <param name="right">Second value object</param>
        /// <returns>True if they are equals by value</returns>
        /// <remarks>
        /// Since they are <see cref="ValueObject"/>, equality is not defined by identity
        /// </remarks>
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            return left?.Equals(right) != false;
        }

        /// <summary>
        /// Define a custom non-equality check for value objects based on
        /// <see cref="EqualOperator"/>
        /// </summary>
        /// <param name="left">First value object</param>
        /// <param name="right">Second value object</param>
        /// <returns>True if they are not equals by value</returns>
        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
            => ! EqualOperator(left, right);

        /// <summary>
        /// Retrieve all atomic values held by the value object
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of those values</returns>
        protected abstract IEnumerable<object> GetAtomicValues();

        /// <summary>
        /// Checks equality for the current value object against another one
        /// </summary>
        /// <param name="obj">Other <see cref="ValueObject"/> to evaluate the equality</param>
        /// <returns>True if both <see cref="ValueObject"/> are equals by value</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject) obj;
            
            using var thisValues = GetAtomicValues()
                .GetEnumerator();

            using var otherValues = other.GetAtomicValues()
                .GetEnumerator();
            
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (thisValues.Current is null ^ otherValues.Current is null)
                {
                    return false;
                }

                if (thisValues.Current != null 
                    && ! thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }
            return ! thisValues.MoveNext() && ! otherValues.MoveNext();
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            return GetAtomicValues()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }
    }
}
