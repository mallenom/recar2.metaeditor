#define DEV_DEBUG
namespace Mallenom
{
	using System;
	using System.Diagnostics;
	using System.Threading;

	/// <summary>A static class for verifying assumptions.</summary>
	static class Assert
	{
		// ReSharper disable UnusedMember.Global
		// ReSharper disable MemberCanBePrivate.Global

		// ReSharper disable InconsistentNaming
		[DebuggerHidden]
		[ContractAnnotation("=> halt")]
		private static void _Break()
			// ReSharper restore InconsistentNaming
		{
#if DEV_DEBUG
			if(Debugger.IsAttached)
			{
				Debugger.Break();
			}
			else
			{
				Debug.Assert(false);
			}
#else
			Debug.Assert(false);
			#endif
		}

		/// <summary>A function signature for Assert.Evaluate.</summary>
		public delegate void EvaluateFunction();

		/// <summary>A function signature for Assert.Implies.</summary>
		/// <returns>Returns the truth of a predicate.</returns>
		public delegate bool ImplicationFunction();

		/// <summary>
		/// Executes the specified argument.
		/// </summary>
		/// <param name="argument">The function to execute.</param>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		public static void Evaluate(EvaluateFunction argument)
		{
			IsNotNull(argument);
			argument();
		}

		/// <summary>
		/// Verifies that two generic type data are equal.  The assertion fails if they are not.
		/// </summary>
		/// <typeparam name="T">The generic type to compare for equality.</typeparam>
		/// <param name="expected">The first generic type data to compare.  This is is the expected value.</param>
		/// <param name="actual">The second generic type data to compare.  This is the actual value.</param>
		/// <remarks>This breaks into the debugger in the case of a failed assertion.</remarks>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		public static void AreEqual<T>(T expected, T actual)
		{
			if(null == expected)
			{
				// Two nulls are considered equal, regardless of type semantics.
				if(null != actual && !actual.Equals(expected))
				{
					_Break();
				}
			}
			else if(!expected.Equals(actual))
			{
				_Break();
			}
		}

		/// <summary>
		/// Verifies that two generic type data are not equal.  The assertion fails if they are.
		/// </summary>
		/// <typeparam name="T">The generic type to compare for inequality.</typeparam>
		/// <param name="notExpected">The first generic type data to compare.  This is is the value that's not expected.</param>
		/// <param name="actual">The second generic type data to compare.  This is the actual value.</param>
		/// <remarks>This breaks into the debugger in the case of a failed assertion.</remarks>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		public static void AreNotEqual<T>(T notExpected, T actual)
		{
			if(null == notExpected)
			{
				// Two nulls are considered equal, regardless of type semantics.
				if(null == actual || actual.Equals(notExpected))
				{
					_Break();
				}
			}
			else if(notExpected.Equals(actual))
			{
				_Break();
			}
		}

		/// <summary>
		/// Verifies that if the specified condition is true, then so is the result.
		/// The assertion fails if the condition is true but the result is false.
		/// </summary>
		/// <param name="condition">if set to <c>true</c> [condition].</param>
		/// <param name="result">
		/// A second Boolean statement.  If the first was true then so must this be.
		/// If the first statement was false then the value of this is ignored.
		/// </param>
		/// <remarks>This breaks into the debugger in the case of a failed assertion.</remarks>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		public static void Implies(bool condition, bool result)
		{
			if(condition && !result)
			{
				_Break();
			}
		}

		/// <summary>
		/// Lazy evaluation overload.  Verifies that if a condition is true, then so is a secondary value.
		/// </summary>
		/// <param name="condition">The conditional value.</param>
		/// <param name="result">A function to be evaluated for truth if the condition argument is true.</param>
		/// <remarks>
		/// This overload only evaluates the result if the first condition is true.
		/// </remarks>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		public static void Implies(bool condition, ImplicationFunction result)
		{
			if(condition && !result())
			{
				_Break();
			}
		}

		/// <summary>
		/// Verifies that a string has content.  I.e. it is not null and it is not empty.
		/// </summary>
		/// <param name="value">The string to verify.</param>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		[ContractAnnotation("value:null => halt")]
		public static void IsNeitherNullNorEmpty(string value)
		{
			IsFalse(string.IsNullOrEmpty(value));
		}

		/// <summary>
		/// Verifies that a string has content.  I.e. it is not null and it is not purely whitespace.
		/// </summary>
		/// <param name="value">The string to verify.</param>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		[ContractAnnotation("value:null => halt")]
		public static void IsNeitherNullNorWhitespace([NotNull]string value)
		{
			if(string.IsNullOrEmpty(value))
			{
				_Break();
			}

			if(value.Trim().Length == 0)
			{
				_Break();
			}
		}

		/// <summary>
		/// Verifies the specified value is not null.  The assertion fails if it is.
		/// </summary>
		/// <typeparam name="T">The generic reference type.</typeparam>
		/// <param name="value">The value to check for nullness.</param>
		/// <remarks>This breaks into the debugger in the case of a failed assertion.</remarks>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		[ContractAnnotation("value:null => halt")]
		[AssertionMethod]
		public static void IsNotNull<T>([AssertionCondition(AssertionConditionType.IS_NOT_NULL)][NoEnumeration]T value) where T : class
		{
			if(null == value)
			{
				_Break();
			}
		}

		[Conditional("DEBUG")]
		[DebuggerHidden]
		public static void IsDefault<T>(T value) where T : struct
		{
			if(!value.Equals(default(T)))
			{
				Fail();
			}
		}

		[Conditional("DEBUG")]
		[DebuggerHidden]
		public static void IsNotDefault<T>(T value) where T : struct
		{
			if(value.Equals(default(T)))
			{
				Fail();
			}
		}

		/// <summary>
		/// Verifies that the specified condition is false.  The assertion fails if it is true.
		/// </summary>
		/// <param name="condition">The expression that should be <c>false</c>.</param>
		/// <remarks>This breaks into the debugger in the case of a failed assertion.</remarks>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		[ContractAnnotation("condition:true => halt")]
		[AssertionMethod]
		public static void IsFalse([AssertionCondition(AssertionConditionType.IS_FALSE)]bool condition)
		{
			if(condition)
			{
				_Break();
			}
		}

		/// <summary>
		/// Verifies that the specified condition is false.  The assertion fails if it is true.
		/// </summary>
		/// <param name="condition">The expression that should be <c>false</c>.</param>
		/// <param name="message">The message to display if the condition is <c>true</c>.</param>
		/// <remarks>This breaks into the debugger in the case of a failed assertion.</remarks>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		[ContractAnnotation("condition:true => halt")]
		public static void IsFalse(bool condition, string message)
		{
			if(condition)
			{
				_Break();
			}
		}

		/// <summary>
		/// Verifies that the specified condition is true.  The assertion fails if it is not.
		/// </summary>
		/// <param name="condition">A condition that is expected to be <c>true</c>.</param>
		/// <remarks>This breaks into the debugger in the case of a failed assertion.</remarks>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		[ContractAnnotation("condition:false => halt")]
		[AssertionMethod]
		public static void IsTrue([AssertionCondition(AssertionConditionType.IS_TRUE)]bool condition)
		{
			if(!condition)
			{
				_Break();
			}
		}

		/// <summary>
		/// Verifies that the specified condition is true.  The assertion fails if it is not.
		/// </summary>
		/// <param name="condition">A condition that is expected to be <c>true</c>.</param>
		/// <param name="message">The message to write in case the condition is <c>false</c>.</param>
		/// <remarks>This breaks into the debugger in the case of a failed assertion.</remarks>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		[ContractAnnotation("condition:false => halt")]
		[AssertionMethod]
		public static void IsTrue([AssertionCondition(AssertionConditionType.IS_TRUE)]bool condition, string message)
		{
			if(!condition)
			{
				_Break();
			}
		}

		/// <summary>
		/// This line should never be executed.  The assertion always fails.
		/// </summary>
		/// <remarks>This breaks into the debugger in the case of a failed assertion.</remarks>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		[ContractAnnotation("=> halt")]
		public static void Fail()
		{
			_Break();
		}

		/// <summary>
		/// This line should never be executed.  The assertion always fails.
		/// </summary>
		/// <param name="message">The message to display if this function is executed.</param>
		/// <remarks>This breaks into the debugger in the case of a failed assertion.</remarks>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		[ContractAnnotation("=> halt")]
		public static void Fail(string message)
		{
			_Break();
		}

		/// <summary>
		/// Verifies that the specified object is null.  The assertion fails if it is not.
		/// </summary>
		/// <param name="item">The item to verify is null.</param>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		[ContractAnnotation("item:notnull => halt")]
		[AssertionMethod]
		public static void IsNull<T>([AssertionCondition(AssertionConditionType.IS_NULL)]T item) where T : class
		{
			if(null != item)
			{
				_Break();
			}
		}

		/// <summary>
		/// Verifies that the specified value is within the expected range.  The assertion fails if it isn't.
		/// </summary>
		/// <param name="lowerBoundInclusive">The lower bound inclusive value.</param>
		/// <param name="value">The value to verify.</param>
		/// <param name="upperBoundInclusive">The upper bound inclusive value.</param>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		public static void BoundedDoubleInc(double lowerBoundInclusive, double value, double upperBoundInclusive)
		{
			if(value < lowerBoundInclusive || value > upperBoundInclusive)
			{
				_Break();
			}
		}

		/// <summary>
		/// Verifies that the specified value is within the expected range.  The assertion fails if it isn't.
		/// </summary>
		/// <param name="lowerBoundInclusive">The lower bound inclusive value.</param>
		/// <param name="value">The value to verify.</param>
		/// <param name="upperBoundExclusive">The upper bound exclusive value.</param>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		public static void BoundedInteger(int lowerBoundInclusive, int value, int upperBoundExclusive)
		{
			if(value < lowerBoundInclusive || value >= upperBoundExclusive)
			{
				_Break();
			}
		}

		/// <summary>
		/// Verify the current thread's apartment state is what's expected.  The assertion fails if it isn't
		/// </summary>
		/// <param name="expectedState">
		/// The expected apartment state for the current thread.
		/// </param>
		/// <remarks>This breaks into the debugger in the case of a failed assertion.</remarks>
		[Conditional("DEBUG")]
		[DebuggerHidden]
		public static void IsApartmentState(ApartmentState expectedState)
		{
			if(Thread.CurrentThread.GetApartmentState() != expectedState)
			{
				_Break();
			}
		}

		[Conditional("DEBUG")]
		[DebuggerHidden]
		public static void NullableIsNotNull<T>(T? value) where T : struct
		{
			if(null == value)
			{
				_Break();
			}
		}

		[Conditional("DEBUG")]
		[DebuggerHidden]
		public static void NullableIsNull<T>(T? value) where T : struct
		{
			if(null != value)
			{
				_Break();
			}
		}

#if WPF

		[Conditional("DEBUG")]
		[DebuggerHidden]
		public static void IsNotOnMainThread()
		{
			if(System.Windows.Application.Current.Dispatcher.CheckAccess())
			{
				_Break();
			}
		}

#elif WINFORMS

#endif

		// ReSharper restore MemberCanBePrivate.Global
		// ReSharper restore UnusedMember.Global
	}
}
