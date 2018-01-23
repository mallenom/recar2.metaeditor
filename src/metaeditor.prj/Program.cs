using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using Mallenom.Diagnostics;
using Mallenom.Diagnostics.Logs;
using Mallenom.Localization;
using Mallenom.Storage;

namespace Recar2.MetaEditor
{
	static class Program
	{
		private static readonly ILog Log = LogManager.GetCurrentClassLog();

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			LogManager.GetRepository().RootLogger.AddAppender(new FileAppender());
			LogManager.GetRepository().RootLogger.AddAppender(new LogViewAppender());

			if(!Debugger.IsAttached)
			{
				Application.ThreadException += Application_ThreadException;
				AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			}

			Log.Info("Start application.");

			try
			{
				using(var core = new Core())
				{
					var form = new MainForm(core);
					var configFilename = Path.ChangeExtension(Application.ExecutablePath, "config.xml");
					if(File.Exists(configFilename))
					{
						LoadConfiguration(configFilename, form);
					}
					form.Closed += (sender, args) => SaveConfiguration(configFilename, form);

					Application.Run(form);
				}
				Log.Info("Correct shutdown application.");
			}
			catch(Exception exc)
			{
				Log.Fatal("Fatal error.", exc);
			}
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs arg)
		{
			try
			{
				HandleUnhandledException((Exception)arg.ExceptionObject);
				Process.GetCurrentProcess().Kill();
			}
			catch
			{
			}
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs arg)
		{
			try
			{
				HandleUnhandledException(arg.Exception);
				Environment.Exit(0);
			}
			catch
			{
			}
		}

		private static void HandleUnhandledException(Exception exc)
		{
			Locale.Current.TryGetString("CriticalError", out string message);
			Log.Fatal(message, exc);
			using(var form = new ExceptionForm(exc))
			{
				form.ShowDialog();
			}
		}

		private static void LoadConfiguration(string configFilename, MainForm form)
		{
			try
			{
				XmlDataFormat.DeserializeFrom(configFilename, reader =>
				{
					var root = reader.TryGetChildStorage("Configuration");
					var uiReader = root?.TryGetChildStorage("UI");
					if(uiReader != null)
					{
						form.LoadConfiguration(uiReader);
					}
				});
			}
			catch(Exception exc)
			{
				Log.Error("Configuration load fail.", exc);
			}
		}

		private static void SaveConfiguration(string configFilename, MainForm form)
		{
			XmlDataFormat.SerializeTo(configFilename, writer =>
			{
				var root = writer.BeginChildObject("Configuration");
				var uiWriter = root.BeginChildObject("UI");
				form.SaveConfiguration(uiWriter);
				root.EndChildObject(uiWriter);
				writer.EndChildObject(root);
			});
		}
	}
}
