using System;
using System.Collections.Generic;
using System.Threading;

using Mallenom;

namespace Recar2
{
	static class ExceptionExtensions
	{
		public static bool IsCritical(this Exception exception)
		{
			Assert.IsNotNull(exception);

			switch(exception)
			{
				case NullReferenceException _:
				case StackOverflowException _:
				case OutOfMemoryException _:
				case ThreadAbortException _:
				case IndexOutOfRangeException _:
				case AccessViolationException _:
					return true;
			}
			return false;
		}

		public static IEnumerable<Exception> AsEnumerable(this Exception exception)
		{
			while(exception != null)
			{
				yield return exception;
				if(exception is AggregateException aggregate)
				{
					foreach(var innerException in aggregate.InnerExceptions)
						foreach(var exc in innerException.AsEnumerable())
						{
							yield return exc;
						}
				}
				else
				{
					exception = exception.InnerException;
				}
			}
		}
	}
}
