
#define EXCEPTION_MESSAGES_LOCALE_EN
//#define NO_AGGRESSIVE_INLINING
//#define EXCEPTION_MESSAGES_LOCALE_RU
namespace Mallenom
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Runtime.CompilerServices;
	using System.Threading;

	/// <summary>
	/// A static class for retail validated assertions.
	/// Instead of breaking into the debugger an exception is thrown.
	/// </summary>
// ReSharper disable PartialTypeWithSinglePart
	static partial class Verify
// ReSharper restore PartialTypeWithSinglePart
	{
#pragma warning disable 3021
		// ReSharper disable UnusedMember.Global
		// ReSharper disable UnusedParameter.Global
		// ReSharper disable InconsistentNaming
		// ReSharper disable MemberCanBePrivate.Global

		/// <summary>Argument verification methods.</summary>
// ReSharper disable PartialTypeWithSinglePart
		public static partial class Argument
// ReSharper restore PartialTypeWithSinglePart
		{
			#region String Argument Verification

			/// <summary>Ensure that a string argument is neither <c>null</c> nor empty.</summary>
			/// <param name="value">The string to validate.</param>
			/// <param name="parameterName">The name of the parameter that will be presented if an exception is thrown.</param>
			/// <exception cref="T:System.ArgumentException">
			/// <paramref name="value"/> is empty.
			/// </exception>
			/// <exception cref="T:System.ArgumentNullException">
			/// <paramref name="value"/> == <c>null</c>.
			/// </exception>
			/// <example><code>
			/// void Method(string argument)
			/// {
			///		Verify.Argument.IsNeitherNullNorEmpty(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("value:null => halt")]
			[AssertionMethod]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNeitherNullNorEmpty([AssertionCondition(AssertionConditionType.IS_NOT_NULL)]string value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

#if EXCEPTION_MESSAGES_LOCALE_RU
				const string errorMessage = "Параметр не может быть равен null или быть пустой строкой.";
				#else
				const string errorMessage = "The parameter can not be either null or empty.";
#endif
				if(string.IsNullOrEmpty(value))
				{
					if(value == null)
					{
						throw new ArgumentNullException(parameterName, errorMessage);
					}
					throw new ArgumentException(errorMessage, parameterName);
				}
			}

			/// <summary>Ensure that a string argument is neither <c>null</c> nor does it consist only of whitespace.</summary>
			/// <param name="value">The string to validate.</param>
			/// <param name="parameterName">The name of the parameter that will be presented if an exception is thrown.</param>
			/// <exception cref="T:System.ArgumentException">
			/// <paramref name="value"/> is empty or consists of whitespace only.
			/// </exception>
			/// <exception cref="T:System.ArgumentNullException">
			/// <paramref name="value"/> == <c>null</c>.
			/// </exception>
			/// <example><code>
			/// void Method(string argument)
			/// {
			///		Verify.Argument.IsNeitherNullNorWhitespace(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("value:null => halt")]
			[AssertionMethod]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNeitherNullNorWhitespace([AssertionCondition(AssertionConditionType.IS_NOT_NULL)]string value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

#if EXCEPTION_MESSAGES_LOCALE_RU
				const string errorMessage = "Параметр не может быть равен null, быть пустой строкой или состоять целиком из пробельных символов.";
				#else
				const string errorMessage = "The parameter can not be either null or empty or consist only of white space characters.";
#endif
				if(string.IsNullOrWhiteSpace(value))
				{
					if(value == null)
					{
						throw new ArgumentNullException(parameterName, errorMessage);
					}
					throw new ArgumentException(errorMessage, parameterName);
				}
			}

			#endregion

			#region Null/Not Null/Default Value

			/// <summary>Verifies that an argument is not default value.</summary>
			/// <typeparam name="T">Type of the object to validate. Must be a value-type.</typeparam>
			/// <param name="value">Value to validate.</param>
			/// <param name="parameterName">The name of the parameter that will be presented if an exception is thrown.</param>
			/// <exception cref="T:System.ArgumentException">
			/// <paramref name="value"/> equals <c>default(T)</c>.
			/// </exception>
			/// <example><code>
			/// void Method(int argument)
			/// {
			///		Verify.Argument.IsNotDefault(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNotDefault<T>(T value, [InvokerParameterName] string parameterName) where T : struct
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

#if EXCEPTION_MESSAGES_LOCALE_RU
				const string errorMessage = "Параметр не должен быть равен значению по умолчанию ({0}).";
				#else
				const string errorMessage = "The parameter must not be the default value ({0}).";
#endif
				if(default(T).Equals(value))
				{
					throw new ArgumentException(
						string.Format(
							CultureInfo.InvariantCulture,
							errorMessage,
							default(T).ToString()),
						parameterName);
				}
			}

			/// <summary>Verifies that an argument is not <c>null</c>.</summary>
			/// <typeparam name="T">Type of the object to validate. Must be a reference-type.</typeparam>
			/// <param name="value">The object to validate.</param>
			/// <param name="parameterName">The name of the parameter that will be presented if an exception is thrown.</param>
			/// <exception cref="T:System.ArgumentNullException">
			/// <paramref name="value"/> == <c>null</c>.
			/// </exception>
			/// <example><code>
			/// void Method(object argument)
			/// {
			///		Verify.Argument.IsNotNull(argument, nameof(argument));
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("value:null => halt")]
			[AssertionMethod]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNotNull<T>([AssertionCondition(AssertionConditionType.IS_NOT_NULL)][NoEnumeration]T value, [InvokerParameterName] string parameterName) where T : class
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value == null)
				{
					throw new ArgumentNullException(parameterName);
				}
			}

			/// <summary>Verifies that an argument is <c>null</c>.</summary>
			/// <typeparam name="T">Type of the object to validate. Must be a reference-type.</typeparam>
			/// <param name="value">The object to validate.</param>
			/// <param name="parameterName">The name of the parameter that will be presented if an exception is thrown.</param>
			/// <exception cref="T:System.ArgumentException">
			/// <paramref name="value"/> != <c>null</c>.
			/// </exception>
			/// <example><code>
			/// void Method(object argument)
			/// {
			///		Verify.Argument.IsNull(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("value:notnull => halt")]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNull<T>([AssertionCondition(AssertionConditionType.IS_NULL)]T value, [InvokerParameterName] string parameterName) where T : class
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

#if EXCEPTION_MESSAGES_LOCALE_RU
				const string errorMessage = "Параметр должен быть равен null.";
				#else
				const string errorMessage = "The parameter must be null.";
#endif
				if(value != null)
				{
					throw new ArgumentException(errorMessage, parameterName);
				}
			}

			#endregion

			#region IsNotNegative() overloads

#if EXCEPTION_MESSAGES_LOCALE_RU
			const string IsNotNegativeErrorMessage = "Параметр должен быть неотрицательным.";
			#else
			private const string IsNotNegativeErrorMessage = "The parameter must be non-negative.";
#endif

			/// <summary>
			/// Verifies that argument is not a negative number. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; 0.
			/// </exception>
			/// <example><code>
			/// void Method(sbyte argument)
			/// {
			///		Verify.Argument.IsNotNegative(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNotNegative(sbyte value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value < 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsNotNegativeErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is not a negative number. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; 0.
			/// </exception>
			/// <example><code>
			/// void Method(short argument)
			/// {
			///		Verify.Argument.IsNotNegative(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNotNegative(short value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value < 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsNotNegativeErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is not a negative number. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; 0.
			/// </exception>
			/// <example><code>
			/// void Method(int argument)
			/// {
			///		Verify.Argument.IsNotNegative(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNotNegative(int value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value < 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsNotNegativeErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is not a negative number. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; 0.
			/// </exception>
			/// <example><code>
			/// void Method(long argument)
			/// {
			///		Verify.Argument.IsNotNegative(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNotNegative(long value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value < 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsNotNegativeErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is not a negative number. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; 0.
			/// </exception>
			/// <example><code>
			/// void Method(float argument)
			/// {
			///		Verify.Argument.IsNotNegative(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNotNegative(float value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value < 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsNotNegativeErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is not a negative number. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; 0.
			/// </exception>
			/// <example><code>
			/// void Method(double argument)
			/// {
			///		Verify.Argument.IsNotNegative(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNotNegative(double value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value < 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsNotNegativeErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is not a negative number. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; 0.
			/// </exception>
			/// <example><code>
			/// void Method(decimal argument)
			/// {
			///		Verify.Argument.IsNotNegative(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNotNegative(decimal value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value < 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsNotNegativeErrorMessage);
				}
			}

			#endregion

			#region IsNegative() overloads

#if EXCEPTION_MESSAGES_LOCALE_RU
			const string IsNegativeErrorMessage = "Параметр должен быть отрицательным.";
			#else
			private const string IsNegativeErrorMessage = "The parameter must be a negative number.";
#endif

			/// <summary>
			/// Verifies that argument is a negative number. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &gt;= 0.
			/// </exception>
			/// <example><code>
			/// void Method(sbyte argument)
			/// {
			///		Verify.Argument.IsNegative(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNegative(sbyte value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value >= 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsNegativeErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is a negative number. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &gt;= 0.
			/// </exception>
			/// <example><code>
			/// void Method(short argument)
			/// {
			///		Verify.Argument.IsNegative(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNegative(short value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value >= 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsNegativeErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is a negative number. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &gt;= 0.
			/// </exception>
			/// <example><code>
			/// void Method(int argument)
			/// {
			///		Verify.Argument.IsNegative(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNegative(int value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value >= 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsNegativeErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is a negative number. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &gt;= 0.
			/// </exception>
			/// <example><code>
			/// void Method(long argument)
			/// {
			///		Verify.Argument.IsNegative(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNegative(long value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value >= 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsNegativeErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is a negative number. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &gt;= 0.
			/// </exception>
			/// <example><code>
			/// void Method(float argument)
			/// {
			///		Verify.Argument.IsNegative(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNegative(float value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value >= 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsNegativeErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is a negative number. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &gt;= 0.
			/// </exception>
			/// <example><code>
			/// void Method(double argument)
			/// {
			///		Verify.Argument.IsNegative(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNegative(double value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value >= 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsNegativeErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is a negative number. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &gt;= 0.
			/// </exception>
			/// <example><code>
			/// void Method(decimal argument)
			/// {
			///		Verify.Argument.IsNegative(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNegative(decimal value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value >= 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsNegativeErrorMessage);
				}
			}

			#endregion

			#region IsPositive() overloads

#if EXCEPTION_MESSAGES_LOCALE_RU
			const string IsPositiveErrorMessage = "Параметр должен положительным числом.";
			#else
			private const string IsPositiveErrorMessage = "The parameter must be a positive number.";
#endif

			/// <summary>
			/// Verifies that argument is a positive number (&gt; 0). Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt;= 0.
			/// </exception>
			/// <example><code>
			/// void Method(sbyte argument)
			/// {
			///		Verify.Argument.IsPositive(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsPositive(sbyte value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value <= 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsPositiveErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is a positive number (&gt; 0). Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt;= 0.
			/// </exception>
			/// <example><code>
			/// void Method(byte argument)
			/// {
			///		Verify.Argument.IsPositive(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsPositive(byte value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value == 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsPositiveErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is a positive number (&gt; 0). Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt;= 0.
			/// </exception>
			/// <example><code>
			/// void Method(short argument)
			/// {
			///		Verify.Argument.IsPositive(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsPositive(short value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value <= 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsPositiveErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is a positive number (&gt; 0). Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt;= 0.
			/// </exception>
			/// <example><code>
			/// void Method(int argument)
			/// {
			///		Verify.Argument.IsPositive(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsPositive(int value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value <= 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsPositiveErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is a positive number (&gt; 0). Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt;= 0.
			/// </exception>
			/// <example><code>
			/// void Method(long argument)
			/// {
			///		Verify.Argument.IsPositive(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsPositive(long value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value <= 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsPositiveErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is a positive number (&gt; 0). Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt;= 0.
			/// </exception>
			/// <example><code>
			/// void Method(float argument)
			/// {
			///		Verify.Argument.IsPositive(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsPositive(float value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value <= 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsPositiveErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is a positive number (&gt; 0). Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt;= 0.
			/// </exception>
			/// <example><code>
			/// void Method(double argument)
			/// {
			///		Verify.Argument.IsPositive(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsPositive(double value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value <= 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsPositiveErrorMessage);
				}
			}

			/// <summary>
			/// Verifies that argument is a positive number (&gt; 0). Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="value">Parameter value to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt;= 0.
			/// </exception>
			/// <example><code>
			/// void Method(decimal argument)
			/// {
			///		Verify.Argument.IsPositive(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsPositive(decimal value, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value <= 0)
				{
					throw new ArgumentOutOfRangeException(parameterName, IsPositiveErrorMessage);
				}
			}

			#endregion

			#region IsInRange() overloads

#if EXCEPTION_MESSAGES_LOCALE_RU
			const string IsInRangeErrorMessage = "Значение должно лежать в диапазоне [{0}, {1}].";
			#else
			private const string IsInRangeErrorMessage = "Value must be bounded with [{0}, {1}].";
#endif

			/// <summary>
			/// Verifies the specified statement is <c>true</c>. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="statement">The statement to be verified as <c>true</c>.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="statement"/> == <c>false</c>.
			/// </exception>
			/// <example><code>
			/// void Method(int argument)
			/// {
			///		Verify.Argument.IsInRange(argument &gt; 4, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsInRange(bool statement, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(!statement)
				{
					throw new ArgumentOutOfRangeException(parameterName);
				}
			}

			/// <summary>
			/// Verifies the specified statement is <c>true</c>. Throws an <see cref="ArgumentOutOfRangeException"/> if it's not.
			/// </summary>
			/// <param name="statement">The statement to be verified as <c>true</c>.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <param name="message">The message to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// <paramref name="statement"/> == <c>false</c>.
			/// </exception>
			/// <example><code>
			/// void Method(int argument)
			/// {
			///		Verify.Argument.IsInRange(argument &gt; 4, "argument", "Must be greater than 4.");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsInRange(bool statement, [InvokerParameterName] string parameterName, string message)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(!statement)
				{
					throw new ArgumentOutOfRangeException(parameterName, message);
				}
			}

			/// <summary>
			/// Verifies that the specified value is within the expected range. 
			/// Throws an <see cref="ArgumentOutOfRangeException"/> if it isn't.
			/// </summary>
			/// <param name="lowerBoundInclusive">The lower bound inclusive value.</param>
			/// <param name="value">The value to verify.</param>
			/// <param name="upperBoundInclusive">The upper bound inclusive value.</param>
			/// <param name="parameterName">Parameter name to include in <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; <paramref name="lowerBoundInclusive"/> or
			/// <paramref name="value"/> &gt; <paramref name="upperBoundInclusive"/>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsInRange(sbyte lowerBoundInclusive, sbyte value, sbyte upperBoundInclusive, [InvokerParameterName] string parameterName)
			{
				Assert.IsTrue(upperBoundInclusive >= lowerBoundInclusive);
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(value < lowerBoundInclusive || value > upperBoundInclusive)
				{
					throw new ArgumentOutOfRangeException(
						parameterName,
						string.Format(
							CultureInfo.InvariantCulture,
							IsInRangeErrorMessage,
							lowerBoundInclusive,
							upperBoundInclusive));
				}
			}

			/// <summary>
			/// Verifies that the specified value is within the expected range. 
			/// Throws an <see cref="ArgumentOutOfRangeException"/> if it isn't.
			/// </summary>
			/// <param name="lowerBoundInclusive">The lower bound inclusive value.</param>
			/// <param name="value">The value to verify.</param>
			/// <param name="upperBoundInclusive">The upper bound inclusive value.</param>
			/// <param name="parameterName">Parameter name to include in <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; <paramref name="lowerBoundInclusive"/> or
			/// <paramref name="value"/> &gt; <paramref name="upperBoundInclusive"/>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsInRange(byte lowerBoundInclusive, byte value, byte upperBoundInclusive, [InvokerParameterName] string parameterName)
			{
				Assert.IsTrue(upperBoundInclusive >= lowerBoundInclusive);
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(value < lowerBoundInclusive || value > upperBoundInclusive)
				{
					throw new ArgumentOutOfRangeException(
						parameterName,
						string.Format(
							CultureInfo.InvariantCulture,
							IsInRangeErrorMessage,
							lowerBoundInclusive,
							upperBoundInclusive));
				}
			}

			/// <summary>
			/// Verifies that the specified value is within the expected range. 
			/// Throws an <see cref="ArgumentOutOfRangeException"/> if it isn't.
			/// </summary>
			/// <param name="lowerBoundInclusive">The lower bound inclusive value.</param>
			/// <param name="value">The value to verify.</param>
			/// <param name="upperBoundInclusive">The upper bound inclusive value.</param>
			/// <param name="parameterName">Parameter name to include in <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; <paramref name="lowerBoundInclusive"/> or
			/// <paramref name="value"/> &gt; <paramref name="upperBoundInclusive"/>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsInRange(char lowerBoundInclusive, char value, char upperBoundInclusive, [InvokerParameterName] string parameterName)
			{
				Assert.IsTrue(upperBoundInclusive >= lowerBoundInclusive);
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(value < lowerBoundInclusive || value > upperBoundInclusive)
				{
					throw new ArgumentOutOfRangeException(
						parameterName,
						string.Format(
							CultureInfo.InvariantCulture,
							IsInRangeErrorMessage,
							lowerBoundInclusive,
							upperBoundInclusive));
				}
			}

			/// <summary>
			/// Verifies that the specified value is within the expected range. 
			/// Throws an <see cref="ArgumentOutOfRangeException"/> if it isn't.
			/// </summary>
			/// <param name="lowerBoundInclusive">The lower bound inclusive value.</param>
			/// <param name="value">The value to verify.</param>
			/// <param name="upperBoundInclusive">The upper bound inclusive value.</param>
			/// <param name="parameterName">Parameter name to include in <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; <paramref name="lowerBoundInclusive"/> or
			/// <paramref name="value"/> &gt; <paramref name="upperBoundInclusive"/>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsInRange(short lowerBoundInclusive, short value, short upperBoundInclusive, [InvokerParameterName] string parameterName)
			{
				Assert.IsTrue(upperBoundInclusive >= lowerBoundInclusive);
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(value < lowerBoundInclusive || value > upperBoundInclusive)
				{
					throw new ArgumentOutOfRangeException(
						parameterName,
						string.Format(
							CultureInfo.InvariantCulture,
							IsInRangeErrorMessage,
							lowerBoundInclusive,
							upperBoundInclusive));
				}
			}

			/// <summary>
			/// Verifies that the specified value is within the expected range. 
			/// Throws an <see cref="ArgumentOutOfRangeException"/> if it isn't.
			/// </summary>
			/// <param name="lowerBoundInclusive">The lower bound inclusive value.</param>
			/// <param name="value">The value to verify.</param>
			/// <param name="upperBoundInclusive">The upper bound inclusive value.</param>
			/// <param name="parameterName">Parameter name to include in <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; <paramref name="lowerBoundInclusive"/> or
			/// <paramref name="value"/> &gt; <paramref name="upperBoundInclusive"/>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsInRange(ushort lowerBoundInclusive, ushort value, ushort upperBoundInclusive, [InvokerParameterName] string parameterName)
			{
				Assert.IsTrue(upperBoundInclusive >= lowerBoundInclusive);
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(value < lowerBoundInclusive || value > upperBoundInclusive)
				{
					throw new ArgumentOutOfRangeException(
						parameterName,
						string.Format(
							CultureInfo.InvariantCulture,
							IsInRangeErrorMessage,
							lowerBoundInclusive,
							upperBoundInclusive));
				}
			}

			/// <summary>
			/// Verifies that the specified value is within the expected range. 
			/// Throws an <see cref="ArgumentOutOfRangeException"/> if it isn't.
			/// </summary>
			/// <param name="lowerBoundInclusive">The lower bound inclusive value.</param>
			/// <param name="value">The value to verify.</param>
			/// <param name="upperBoundInclusive">The upper bound inclusive value.</param>
			/// <param name="parameterName">Parameter name to include in <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; <paramref name="lowerBoundInclusive"/> or
			/// <paramref name="value"/> &gt; <paramref name="upperBoundInclusive"/>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsInRange(int lowerBoundInclusive, int value, int upperBoundInclusive, [InvokerParameterName] string parameterName)
			{
				Assert.IsTrue(upperBoundInclusive >= lowerBoundInclusive);
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(value < lowerBoundInclusive || value > upperBoundInclusive)
				{
					throw new ArgumentOutOfRangeException(
						parameterName,
						string.Format(
							CultureInfo.InvariantCulture,
							IsInRangeErrorMessage,
							lowerBoundInclusive,
							upperBoundInclusive));
				}
			}

			/// <summary>
			/// Verifies that the specified value is within the expected range. 
			/// Throws an <see cref="ArgumentOutOfRangeException"/> if it isn't.
			/// </summary>
			/// <param name="lowerBoundInclusive">The lower bound inclusive value.</param>
			/// <param name="value">The value to verify.</param>
			/// <param name="upperBoundInclusive">The upper bound inclusive value.</param>
			/// <param name="parameterName">Parameter name to include in <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; <paramref name="lowerBoundInclusive"/> or
			/// <paramref name="value"/> &gt; <paramref name="upperBoundInclusive"/>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsInRange(uint lowerBoundInclusive, uint value, uint upperBoundInclusive, [InvokerParameterName] string parameterName)
			{
				Assert.IsTrue(upperBoundInclusive >= lowerBoundInclusive);
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(value < lowerBoundInclusive || value > upperBoundInclusive)
				{
					throw new ArgumentOutOfRangeException(
						parameterName,
						string.Format(
							CultureInfo.InvariantCulture,
							IsInRangeErrorMessage,
							lowerBoundInclusive,
							upperBoundInclusive));
				}
			}

			/// <summary>
			/// Verifies that the specified value is within the expected range. 
			/// Throws an <see cref="ArgumentOutOfRangeException"/> if it isn't.
			/// </summary>
			/// <param name="lowerBoundInclusive">The lower bound inclusive value.</param>
			/// <param name="value">The value to verify.</param>
			/// <param name="upperBoundInclusive">The upper bound inclusive value.</param>
			/// <param name="parameterName">Parameter name to include in <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; <paramref name="lowerBoundInclusive"/> or
			/// <paramref name="value"/> &gt; <paramref name="upperBoundInclusive"/>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsInRange(long lowerBoundInclusive, long value, long upperBoundInclusive, [InvokerParameterName] string parameterName)
			{
				Assert.IsTrue(upperBoundInclusive >= lowerBoundInclusive);
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(value < lowerBoundInclusive || value > upperBoundInclusive)
				{
					throw new ArgumentOutOfRangeException(
						parameterName,
						string.Format(
							CultureInfo.InvariantCulture,
							IsInRangeErrorMessage,
							lowerBoundInclusive,
							upperBoundInclusive));
				}
			}

			/// <summary>
			/// Verifies that the specified value is within the expected range. 
			/// Throws an <see cref="ArgumentOutOfRangeException"/> if it isn't.
			/// </summary>
			/// <param name="lowerBoundInclusive">The lower bound inclusive value.</param>
			/// <param name="value">The value to verify.</param>
			/// <param name="upperBoundInclusive">The upper bound inclusive value.</param>
			/// <param name="parameterName">Parameter name to include in <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; <paramref name="lowerBoundInclusive"/> or
			/// <paramref name="value"/> &gt; <paramref name="upperBoundInclusive"/>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsInRange(ulong lowerBoundInclusive, ulong value, ulong upperBoundInclusive, [InvokerParameterName] string parameterName)
			{
				Assert.IsTrue(upperBoundInclusive >= lowerBoundInclusive);
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(value < lowerBoundInclusive || value > upperBoundInclusive)
				{
					throw new ArgumentOutOfRangeException(
						parameterName,
						string.Format(
							CultureInfo.InvariantCulture,
							IsInRangeErrorMessage,
							lowerBoundInclusive,
							upperBoundInclusive));
				}
			}

			/// <summary>
			/// Verifies that the specified value is within the expected range. 
			/// Throws an <see cref="ArgumentOutOfRangeException"/> if it isn't.
			/// </summary>
			/// <param name="lowerBoundInclusive">The lower bound inclusive value.</param>
			/// <param name="value">The value to verify.</param>
			/// <param name="upperBoundInclusive">The upper bound inclusive value.</param>
			/// <param name="parameterName">Parameter name to include in <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; <paramref name="lowerBoundInclusive"/> or
			/// <paramref name="value"/> &gt; <paramref name="upperBoundInclusive"/>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsInRange(float lowerBoundInclusive, float value, float upperBoundInclusive, [InvokerParameterName] string parameterName)
			{
				Assert.IsTrue(upperBoundInclusive >= lowerBoundInclusive);
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(value < lowerBoundInclusive || value > upperBoundInclusive)
				{
					throw new ArgumentOutOfRangeException(
						parameterName,
						string.Format(
							CultureInfo.InvariantCulture,
							IsInRangeErrorMessage,
							lowerBoundInclusive,
							upperBoundInclusive));
				}
			}

			/// <summary>
			/// Verifies that the specified value is within the expected range. 
			/// Throws an <see cref="ArgumentOutOfRangeException"/> if it isn't.
			/// </summary>
			/// <param name="lowerBoundInclusive">The lower bound inclusive value.</param>
			/// <param name="value">The value to verify.</param>
			/// <param name="upperBoundInclusive">The upper bound inclusive value.</param>
			/// <param name="parameterName">Parameter name to include in <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; <paramref name="lowerBoundInclusive"/> or
			/// <paramref name="value"/> &gt; <paramref name="upperBoundInclusive"/>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsInRange(double lowerBoundInclusive, double value, double upperBoundInclusive, [InvokerParameterName] string parameterName)
			{
				Assert.IsTrue(upperBoundInclusive >= lowerBoundInclusive);
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(value < lowerBoundInclusive || value > upperBoundInclusive)
				{
					throw new ArgumentOutOfRangeException(
						parameterName,
						string.Format(
							CultureInfo.InvariantCulture,
							IsInRangeErrorMessage,
							lowerBoundInclusive,
							upperBoundInclusive));
				}
			}

			/// <summary>
			/// Verifies that the specified value is within the expected range. 
			/// Throws an <see cref="ArgumentOutOfRangeException"/> if it isn't.
			/// </summary>
			/// <param name="lowerBoundInclusive">The lower bound inclusive value.</param>
			/// <param name="value">The value to verify.</param>
			/// <param name="upperBoundInclusive">The upper bound inclusive value.</param>
			/// <param name="parameterName">Parameter name to include in <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="ArgumentOutOfRangeException">
			/// <paramref name="value"/> &lt; <paramref name="lowerBoundInclusive"/> or
			/// <paramref name="value"/> &gt; <paramref name="upperBoundInclusive"/>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsInRange(decimal lowerBoundInclusive, decimal value, decimal upperBoundInclusive, [InvokerParameterName] string parameterName)
			{
				Assert.IsTrue(upperBoundInclusive >= lowerBoundInclusive);
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(value < lowerBoundInclusive || value > upperBoundInclusive)
				{
					throw new ArgumentOutOfRangeException(
						parameterName,
						string.Format(
							CultureInfo.InvariantCulture,
							IsInRangeErrorMessage,
							lowerBoundInclusive,
							upperBoundInclusive));
				}
			}

			#endregion

			#region IsValidIndex() overloads

#if EXCEPTION_MESSAGES_LOCALE_RU
			const string IsValidIndexErrorMessage = "Значение должно лежать в диапазоне [0, {0}).";
			#else
			private const string IsValidIndexErrorMessage = "Value must be bounded with [0, {0}).";
#endif

			/// <summary>Verifies that the specified value is a valid index for a collection.</summary>
			/// <param name="itemsCount">Collection items count.</param>
			/// <param name="value">Index value.</param>
			/// <param name="parameterName">Parameter name to include in <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="ArgumentOutOfRangeException">
			/// <paramref name="value"/> is not a valid index.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsValidIndex(short itemsCount, short value, [InvokerParameterName] string parameterName)
			{
				Assert.IsTrue(itemsCount >= 0);
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value < 0 || value >= itemsCount)
				{
					throw new ArgumentOutOfRangeException(
						parameterName,
						string.Format(
							CultureInfo.InvariantCulture,
							IsValidIndexErrorMessage,
							itemsCount));
				}
			}

			/// <summary>Verifies that the specified value is a valid index for a collection.</summary>
			/// <param name="itemsCount">Collection items count.</param>
			/// <param name="value">Index value.</param>
			/// <param name="parameterName">Parameter name to include in <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="ArgumentOutOfRangeException">
			/// <paramref name="value"/> is not a valid index.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsValidIndex(int itemsCount, int value, [InvokerParameterName] string parameterName)
			{
				Assert.IsTrue(itemsCount >= 0);
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value < 0 || value >= itemsCount)
				{
					throw new ArgumentOutOfRangeException(
						parameterName,
						string.Format(
							CultureInfo.InvariantCulture,
							IsValidIndexErrorMessage,
							itemsCount));
				}
			}

			/// <summary>Verifies that the specified value is a valid index for a collection.</summary>
			/// <param name="itemsCount">Collection items count.</param>
			/// <param name="value">Index value.</param>
			/// <param name="parameterName">Parameter name to include in <see cref="ArgumentOutOfRangeException"/>.</param>
			/// <exception cref="ArgumentOutOfRangeException">
			/// <paramref name="value"/> is not a valid index.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsValidIndex(long itemsCount, long value, [InvokerParameterName] string parameterName)
			{
				Assert.IsTrue(itemsCount >= 0);
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(value < 0 || value >= itemsCount)
				{
					throw new ArgumentOutOfRangeException(
						parameterName,
						string.Format(
							CultureInfo.InvariantCulture,
							IsValidIndexErrorMessage,
							itemsCount));
				}
			}

			#endregion

			#region Equality/Inequality

			/// <summary>
			/// Verifies that argument equals expected value. Throws an <see cref="ArgumentException"/> if it's not.
			/// </summary>
			/// <param name="expected">Expected value.</param>
			/// <param name="actual">Actual value.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="T:System.ArgumentException">
			/// <paramref name="actual"/> != <paramref name="expected"/>.
			/// </exception>
			/// <example><code>
			/// void Method(int argument)
			/// {
			///		Verify.Argument.IsEqualTo(10, argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsEqualTo<T>(T expected, T actual, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

#if EXCEPTION_MESSAGES_LOCALE_RU
				const string errorMessage = "Параметр должен быть равен {0}.";
				#else
				const string errorMessage = "The parameter value must be {0}.";
#endif
				if(expected == null)
				{
					if(null != actual && !actual.Equals(expected))
					{
						throw new ArgumentException(
							string.Format(
								CultureInfo.InvariantCulture,
								errorMessage,
								expected),
							parameterName);
					}
				}
				else if(!expected.Equals(actual))
				{
					throw new ArgumentException(
						string.Format(
							CultureInfo.InvariantCulture,
							errorMessage,
							expected),
						parameterName);
				}
			}

			/// <summary>
			/// Verifies that argument equals expected value. Throws an <see cref="ArgumentException"/> if it's not.
			/// </summary>
			/// <param name="expected">Expected value.</param>
			/// <param name="actual">Actual value.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <param name="message">The message to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="T:System.ArgumentException">
			/// <paramref name="actual"/> != <paramref name="expected"/>.
			/// </exception>
			/// <example><code>
			/// void Method(int argument)
			/// {
			///		Verify.Argument.IsEqualTo(10, argument, "argument", "Must be 10.");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsEqualTo<T>(T expected, T actual, [InvokerParameterName] string parameterName, string message)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(expected == null)
				{
					if(null != actual && !actual.Equals(expected))
					{
						throw new ArgumentException(message, parameterName);
					}
				}
				else if(!expected.Equals(actual))
				{
					throw new ArgumentException(message, parameterName);
				}
			}

			/// <summary>
			/// Verifies that argument is not of unexpected value. Throws an <see cref="ArgumentException"/> if it is.
			/// </summary>
			/// <param name="notExpected">Invalid value.</param>
			/// <param name="actual">Actual value.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="T:System.ArgumentException">
			/// <paramref name="actual"/> == <paramref name="notExpected"/>.
			/// </exception>
			/// <example><code>
			/// void Method(int argument)
			/// {
			///		Verify.Argument.IsNotEqualTo(10, argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNotEqualTo<T>(T notExpected, T actual, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

#if EXCEPTION_MESSAGES_LOCALE_RU
				const string errorMessage = "Параметр не должен быть равен {0}.";
				#else
				const string errorMessage = "The parameter value must not be {0}.";
#endif
				if(notExpected == null)
				{
					if(null == actual || actual.Equals(notExpected))
					{
						throw new ArgumentException(
							string.Format(
								CultureInfo.InvariantCulture,
								errorMessage,
								notExpected),
							parameterName);
					}
				}
				else if(notExpected.Equals(actual))
				{
					throw new ArgumentException(
						string.Format(
							CultureInfo.InvariantCulture,
							errorMessage,
							notExpected),
						parameterName);
				}
			}

			/// <summary>
			/// Verifies that argument is not of unexpected value. Throws an <see cref="ArgumentException"/> if it is.
			/// </summary>
			/// <param name="notExpected">Invalid value.</param>
			/// <param name="actual">Actual value.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <param name="message">The message to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="T:System.ArgumentException">
			/// <paramref name="actual"/> == <paramref name="notExpected"/>.
			/// </exception>
			/// <example><code>
			/// void Method(int argument)
			/// {
			///		Verify.Argument.IsNotEqualTo(10, argument, "argument", "Must not be 10.");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNotEqualTo<T>(T notExpected, T actual, [InvokerParameterName] string parameterName, string message)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(notExpected == null)
				{
					if(null == actual || actual.Equals(notExpected))
					{
						throw new ArgumentException(message, parameterName);
					}
				}
				else if(notExpected.Equals(actual))
				{
					throw new ArgumentException(message, parameterName);
				}
			}

			#endregion

			#region IsTrue()/IsFalse() Statement Verification

			/// <summary>
			/// Verifies the specified statement is <c>true</c>. Throws an <see cref="ArgumentException"/> if it's not.
			/// </summary>
			/// <param name="statement">The statement to be verified as <c>true</c>.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="T:System.ArgumentException">
			/// <paramref name="statement"/> == <c>false</c>.
			/// </exception>
			/// <example><code>
			/// void Method(string argument)
			/// {
			///		Verify.Argument.IsTrue(argument.Length == 10, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("statement:false => halt")]
			[AssertionMethod]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsTrue([AssertionCondition(AssertionConditionType.IS_TRUE)]bool statement, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(!statement)
				{
					throw new ArgumentException(string.Empty, parameterName);
				}
			}

			/// <summary>
			/// Verifies the specified statement is <c>true</c>. Throws an <see cref="ArgumentException"/> if it's not.
			/// </summary>
			/// <param name="statement">The statement to be verified as <c>true</c>.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <param name="message">The message to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="T:System.ArgumentException">
			/// <paramref name="statement"/> == <c>false</c>.
			/// </exception>
			/// <example><code>
			/// void Method(string argument)
			/// {
			///		Verify.Argument.IsTrue(argument.Length == 10, "argument", "String length must be 10.");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("statement:false => halt")]
			[AssertionMethod]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsTrue([AssertionCondition(AssertionConditionType.IS_TRUE)]bool statement, [InvokerParameterName] string parameterName, string message)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(!statement)
				{
					throw new ArgumentException(message, parameterName);
				}
			}

			/// <summary>
			/// Verifies the specified statement is <c>false</c>. Throws an <see cref="ArgumentException"/> if it's not.
			/// </summary>
			/// <param name="statement">The statement to be verified as <c>false</c>.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="T:System.ArgumentException">
			/// <paramref name="statement"/> == <c>false</c>.
			/// </exception>
			/// <example><code>
			/// void Method(int argument)
			/// {
			///		Verify.Argument.IsFalse(argument == 10, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("statement:true => halt")]
			[AssertionMethod]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsFalse([AssertionCondition(AssertionConditionType.IS_FALSE)]bool statement, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(statement)
				{
					throw new ArgumentException(string.Empty, parameterName);
				}
			}

			/// <summary>
			/// Verifies the specified statement is <c>false</c>. Throws an <see cref="ArgumentException"/> if it's not.
			/// </summary>
			/// <param name="statement">The statement to be verified as <c>false</c>.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <param name="message">The message to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="T:System.ArgumentException">
			/// <paramref name="statement"/> == <c>false</c>.
			/// </exception>
			/// <example><code>
			/// void Method(int argument)
			/// {
			///		Verify.Argument.IsFalse(argument == 10, "argument", "Value must not be 10.");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("statement:true => halt")]
			[AssertionMethod]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsFalse([AssertionCondition(AssertionConditionType.IS_FALSE)]bool statement, [InvokerParameterName] string parameterName, string message)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(statement)
				{
					throw new ArgumentException(message, parameterName);
				}
			}

			#endregion

			#region Sequence & Collection Verification

			/// <summary>
			/// Verifies that sequence contains no <c>null</c> items.
			/// Throws an <see cref="ArgumentException"/> if contains.
			/// </summary>
			/// <param name="sequence">Sequence to validate.</param>
			/// <param name="parameterName">The name of the parameter that will be presented if an exception is thrown.</param>
			/// <exception cref="ArgumentNullException">
			/// <paramref name="sequence"/> == <c>null</c>.
			/// </exception>
			/// <exception cref="ArgumentException">
			/// <paramref name="sequence"/> contains <c>null</c> items.
			/// </exception>
			/// <example><code>
			/// void Method(object[] values)
			/// {
			///		Verify.Argument.HasNoNullItems(values, "values");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("sequence:null => halt")]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void HasNoNullItems<T>(IEnumerable<T> sequence, [InvokerParameterName] string parameterName)
				where T : class
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(sequence == null)
				{
					throw new ArgumentNullException(parameterName);
				}
				if(sequence.Any(item => item == null))
				{
#if EXCEPTION_MESSAGES_LOCALE_RU
						const string errorMessage = "Последовательность не должна содержать null-элементов.";
						#else
					const string errorMessage = "Sequence must not contain null items.";
#endif
					throw new ArgumentException(errorMessage, parameterName);
				}
			}

			#endregion

			#region File System Related

			/// <summary>Verifies that argument is an existing file.</summary>
			/// <param name="filePath">File path.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="T:System.ArgumentNullException">
			/// <paramref name="filePath"/> == <c>null</c>.
			/// </exception>
			/// <exception cref="T:System.ArgumentException">
			/// File does not exist or <paramref name="filePath"/> is not a valid path.
			/// </exception>
			/// <example><code>
			/// void Method(string argument)
			/// {
			///		Verify.Argument.FileExists(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("filePath:null => halt")]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void FileExists(string filePath, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorEmpty(parameterName);

				IsNeitherNullNorWhitespace(filePath, parameterName);
				if(!File.Exists(filePath))
				{
#if EXCEPTION_MESSAGES_LOCALE_RU
					const string errorMessage = "Файл \"{0}\" не существует.";
					#else
					const string errorMessage = "No file exists at \"{0}\".";
#endif
					throw new ArgumentException(
						string.Format(
							CultureInfo.InvariantCulture,
							errorMessage,
							filePath),
						parameterName);
				}
			}

			/// <summary>Verifies that argument is an existing directory.</summary>
			/// <param name="directoryPath">Directory path.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="T:System.ArgumentNullException">
			/// <paramref name="directoryPath"/> == <c>null</c>.
			/// </exception>
			/// <exception cref="T:System.ArgumentException">
			/// Directory does not exist or <paramref name="directoryPath"/> is not a valid path.
			/// </exception>
			/// <example><code>
			/// void Method(string argument)
			/// {
			///		Verify.Argument.DirectoryExists(argument, "argument");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("directoryPath:null => halt")]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void DirectoryExists(string directoryPath, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorEmpty(parameterName);

				IsNeitherNullNorWhitespace(directoryPath, parameterName);
				if(!Directory.Exists(directoryPath))
				{
#if EXCEPTION_MESSAGES_LOCALE_RU
					const string errorMessage = "Директория \"{0}\" не существует.";
					#else
					const string errorMessage = "No directory exists at \"{0}\".";
#endif
					throw new ArgumentException(
						string.Format(
							CultureInfo.InvariantCulture,
							errorMessage,
							directoryPath),
						parameterName);
				}
			}

			#endregion

			/// <summary>
			/// Verifies that the specified URI is absolute.
			/// </summary>
			/// <param name="uri">URI to verify.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="ArgumentNullException">
			/// <paramref name="uri"/> == <c>null</c>.
			/// </exception>
			/// <exception cref="ArgumentException">
			/// <paramref name="uri"/> is relative.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("uri:null => halt")]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsAbsoluteUri([NotNull]Uri uri, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorWhitespace(parameterName);

				if(uri == null)
				{
					throw new ArgumentNullException(parameterName);
				}
				if(!uri.IsAbsoluteUri)
				{
					throw new ArgumentException("The URI must be absolute.", parameterName);
				}
			}

			#region Argument Type Verification

			/// <summary>Verifies that parameter implements specified interface.</summary>
			/// <param name="parameter">Object to validate.</param>
			/// <param name="interfaceType">Interface type.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="ArgumentNullException">
			/// <paramref name="parameter"/> == <c>null</c>
			/// </exception>
			/// <exception cref="ArgumentException">
			/// <paramref name="parameter"/> does not implement specified interface.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("parameter:null => halt")]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void ImplementsInterface([NotNull]object parameter, [NotNull]Type interfaceType, [InvokerParameterName] string parameterName)
			{
				Assert.IsNotNull(interfaceType);
				Assert.IsTrue(interfaceType.IsInterface);
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(parameter == null)
				{
					throw new ArgumentNullException(parameterName);
				}
				var isImplemented = parameter.GetType().GetInterfaces().Any(ifaceType => ifaceType == interfaceType);
				if(!isImplemented)
				{
#if EXCEPTION_MESSAGES_LOCALE_RU
					const string errorMessage = "Объект должен реализовывать интерфейс {0}.";
					#else
					const string errorMessage = "The parameter must implement {0} interface.";
#endif
					throw new ArgumentException(
						string.Format(
							CultureInfo.InvariantCulture,
							errorMessage,
							interfaceType.ToString()),
						parameterName);
				}
			}

			/// <summary>Verifies that parameter is of specified type.</summary>
			/// <param name="parameter">Object to validate.</param>
			/// <param name="requiredType">Expected type.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="ArgumentNullException">
			/// <paramref name="parameter"/> == <c>null</c>
			/// </exception>
			/// <exception cref="ArgumentException">
			/// <paramref name="parameter"/> is not of specified type.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("parameter:null => halt")]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsOfExactType(object parameter, Type requiredType, [InvokerParameterName] string parameterName)
			{
				Assert.IsNotNull(requiredType);
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(parameter == null)
				{
					throw new ArgumentNullException(parameterName);
				}
				if(parameter.GetType() != requiredType)
				{
#if EXCEPTION_MESSAGES_LOCALE_RU
					const string errorMessage = "Объект должен иметь тип {0}.";
					#else
					const string errorMessage = "The parameter must be of type {0}.";
#endif
					throw new ArgumentException(
						string.Format(
							CultureInfo.InvariantCulture,
							errorMessage,
							requiredType.ToString()),
						parameterName);
				}
			}

			/// <summary>Verifies that parameter is of specified type.</summary>
			/// <typeparam name="T">Expected type.</typeparam>
			/// <param name="parameter">Object to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="ArgumentNullException">
			/// <paramref name="parameter"/> == <c>null</c>
			/// </exception>
			/// <exception cref="ArgumentException">
			/// <paramref name="parameter"/> is not of specified type.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("parameter:null => halt")]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsOfExactType<T>(object parameter, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(parameter == null)
				{
					throw new ArgumentNullException(parameterName);
				}
				if(parameter.GetType() != typeof(T))
				{
#if EXCEPTION_MESSAGES_LOCALE_RU
					const string errorMessage = "Объект должен иметь тип {0}.";
					#else
					const string errorMessage = "The parameter must be of type {0}.";
#endif
					throw new ArgumentException(
						string.Format(
							CultureInfo.InvariantCulture,
							errorMessage,
							typeof(T).ToString()),
						parameterName);
				}
			}

			/// <summary>Verifies that parameter is of specified type or type which is based on specified type.</summary>
			/// <param name="parameter">Object to validate.</param>
			/// <param name="requiredType">Expected type.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="ArgumentNullException">
			/// <paramref name="parameter"/> == <c>null</c>
			/// </exception>
			/// <exception cref="ArgumentException">
			/// <paramref name="parameter"/> is not of specified type.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("parameter:null => halt")]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsOfType(object parameter, Type requiredType, [InvokerParameterName] string parameterName)
			{
				Assert.IsNotNull(requiredType);
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(parameter == null)
				{
					throw new ArgumentNullException(parameterName);
				}
				bool isOfRequiredType = false;
				var parameterType = parameter.GetType();
				while(parameterType != null)
				{
					if(parameterType == requiredType)
					{
						isOfRequiredType = true;
						break;
					}
					parameterType = parameterType.BaseType;
				}
				if(!isOfRequiredType)
				{
#if EXCEPTION_MESSAGES_LOCALE_RU
					const string errorMessage = "Объект должен иметь тип {0} или его наследника.";
					#else
					const string errorMessage = "The parameter must be of type {0} or type which is derived from it.";
#endif
					throw new ArgumentException(
						string.Format(
							CultureInfo.InvariantCulture,
							errorMessage,
							requiredType.ToString()),
						parameterName);
				}
			}

			/// <summary>Verifies that parameter is of specified type or type which is based on specified type.</summary>
			/// <typeparam name="T">Expected type.</typeparam>
			/// <param name="parameter">Object to validate.</param>
			/// <param name="parameterName">Name of the parameter to include in the <see cref="ArgumentException"/>.</param>
			/// <exception cref="ArgumentNullException">
			/// <paramref name="parameter"/> == <c>null</c>
			/// </exception>
			/// <exception cref="ArgumentException">
			/// <paramref name="parameter"/> is not of specified type.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("parameter:null => halt")]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsOfType<T>(object parameter, [InvokerParameterName] string parameterName)
			{
				Assert.IsNeitherNullNorEmpty(parameterName);

				if(parameter == null)
				{
					throw new ArgumentNullException(parameterName);
				}
				bool isOfRequiredType = parameter is T;
				if(!isOfRequiredType)
				{
#if EXCEPTION_MESSAGES_LOCALE_RU
					const string errorMessage = "Объект должен иметь тип {0} или его наследника.";
					#else
					const string errorMessage = "The parameter must be of type {0} or type which is derived from it.";
#endif
					throw new ArgumentException(
						string.Format(
							CultureInfo.InvariantCulture,
							errorMessage,
							typeof(T).ToString()),
						parameterName);
				}
			}

			#endregion
		}

		/// <summary>Program, thread and object state verification methods.</summary>
		public static partial class State
		{
			/// <summary>Ensure that the current thread's apartment state is what's expected.</summary>
			/// <param name="requiredState">The required apartment state for the current thread.</param>
			/// <param name="message">The message string for the exception to be thrown if the state is invalid.</param>
			/// <exception cref="InvalidOperationException">
			/// Thrown if the calling thread's apartment state is not the same as the <paramref name="requiredState"/>.
			/// </exception>
			/// <example><code>
			/// void Method()
			/// {
			///		Verify.State.IsApartmentState(ApartmentState.STA, "Must run on STA thread.");
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsApartmentState(ApartmentState requiredState, string message)
			{
				Assert.AreNotEqual(ApartmentState.Unknown, requiredState);

				if(Thread.CurrentThread.GetApartmentState() != requiredState)
				{
					throw new InvalidOperationException(message);
				}
			}

			/// <summary>Ensure that the current thread's apartment state is what's expected.</summary>
			/// <param name="requiredState">The required apartment state for the current thread.</param>
			/// <exception cref="InvalidOperationException">
			/// Thrown if the calling thread's apartment state is not the same as the <paramref name="requiredState"/>.
			/// </exception>
			/// <example><code>
			/// void Method()
			/// {
			///		Verify.State.IsApartmentState(ApartmentState.STA);
			/// }
			/// </code></example>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsApartmentState(ApartmentState requiredState)
			{
				Assert.AreNotEqual(ApartmentState.Unknown, requiredState);

				var actualState = Thread.CurrentThread.GetApartmentState();
				if(actualState != requiredState)
				{
#if EXCEPTION_MESSAGES_LOCALE_RU
					const string errorMessage = "Метод должен выполняться в {0} потоке.";
					#else
					const string errorMessage = "Method must run on {0} thread.";
#endif
					throw new InvalidOperationException(
						string.Format(
							CultureInfo.InvariantCulture,
							errorMessage,
							requiredState));
				}
			}

			/// <summary>
			/// Verifies the specified statement is <c>true</c>true. Throws an <see cref="InvalidOperationException"/> if it's not.
			/// </summary>
			/// <param name="statement">The statement to be verified as <c>true</c>.</param>
			/// <exception cref="InvalidOperationException">
			/// <paramref name="statement"/> == <c>false</c>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("statement:false => halt")]
			[AssertionMethod]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsTrue([AssertionCondition(AssertionConditionType.IS_TRUE)]bool statement)
			{
				if(!statement)
				{
					throw new InvalidOperationException();
				}
			}

			/// <summary>
			/// Verifies the specified statement is <c>true</c>. Throws an <see cref="InvalidOperationException"/> if it's not.
			/// </summary>
			/// <param name="statement">The statement to be verified as <c>true</c>.</param>
			/// <param name="message">The message to include in the <see cref="InvalidOperationException"/>.</param>
			/// <exception cref="InvalidOperationException">
			/// <paramref name="statement"/> == <c>false</c>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("statement:false => halt")]
			[AssertionMethod]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsTrue([AssertionCondition(AssertionConditionType.IS_TRUE)]bool statement, string message)
			{
				if(!statement)
				{
					throw new InvalidOperationException(message);
				}
			}

			/// <summary>
			/// Verifies the specified statement is <c>false</c>. Throws an <see cref="InvalidOperationException"/> if it's not.
			/// </summary>
			/// <param name="statement">The statement to be verified as <c>false</c>.</param>
			/// <exception cref="InvalidOperationException">
			/// <paramref name="statement"/> == <c>true</c>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("statement:true => halt")]
			[AssertionMethod]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsFalse([AssertionCondition(AssertionConditionType.IS_FALSE)]bool statement)
			{
				if(statement)
				{
					throw new InvalidOperationException();
				}
			}

			/// <summary>
			/// Verifies the specified statement is <c>false</c>. Throws an <see cref="InvalidOperationException"/> if it's not.
			/// </summary>
			/// <param name="statement">The statement to be verified as <c>false</c>.</param>
			/// <param name="message">The message to include in the <see cref="InvalidOperationException"/>.</param>
			/// <exception cref="InvalidOperationException">
			/// <paramref name="statement"/> == <c>true</c>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("statement:true => halt")]
			[AssertionMethod]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsFalse([AssertionCondition(AssertionConditionType.IS_FALSE)]bool statement, string message)
			{
				if(statement)
				{
					throw new InvalidOperationException(message);
				}
			}

			/// <summary>
			/// Verifies the specified instance is not disposed. Throws an <see cref="ObjectDisposedException"/> if it is.
			/// </summary>
			/// <param name="instance">Object instance.</param>
			/// <param name="isDisposed">Instance disposed status.</param>
			/// <exception cref="ObjectDisposedException">
			/// <paramref name="isDisposed"/> == <c>true</c>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("isDisposed:true => halt")]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNotDisposed(object instance, bool isDisposed)
			{
				Assert.IsNotNull(instance);

				if(isDisposed)
				{
					throw new ObjectDisposedException(instance.GetType().Name);
				}
			}

			/// <summary>
			/// Verifies the specified instance is not disposed. Throws an <see cref="ObjectDisposedException"/> if it is.
			/// </summary>
			/// <param name="instance">Object instance.</param>
			/// <param name="isDisposed">Instance disposed status.</param>
			/// <param name="message">The message to include in the <see cref="ObjectDisposedException"/>.</param>
			/// <exception cref="ObjectDisposedException">
			/// <paramref name="isDisposed"/> == <c>true</c>.
			/// </exception>
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
			[DebuggerHidden]
			[ContractAnnotation("isDisposed:true => halt")]
#if !NO_AGGRESSIVE_INLINING
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public static void IsNotDisposed(object instance, bool isDisposed, string message)
			{
				Assert.IsNotNull(instance);

				if(isDisposed)
				{
					throw new ObjectDisposedException(instance.GetType().Name, message);
				}
			}
		}

#pragma warning restore 3021
		// ReSharper restore UnusedParameter.Global
		// ReSharper restore UnusedMember.Global
		// ReSharper restore InconsistentNaming
		// ReSharper restore MemberCanBePrivate.Global
	}
}
