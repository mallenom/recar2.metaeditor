/////////////////////////////////////////////////////////////////////////////
//
/////////////////////////////////////////////////////////////////////////////
using System.Reflection;
using System.Runtime.InteropServices;

#if DEBUG
[assembly: AssemblyProduct("Recar2.MetaEditor - DEBUG")]
#else
[assembly: AssemblyProduct("Recar2.MetaEditor")]
#endif

[assembly: AssemblyCompany("Mallenom Systems")]
[assembly: AssemblyCopyright("© 2017, Mallenom Systems")]
[assembly: AssemblyTrademark("Mallenom Systems")]
[assembly: AssemblyCulture("")]

[assembly: AssemblyVersion(Rev.Major + "." + Rev.Minor + "." + Rev.Patch + "." + Rev.Revision)]
[assembly: AssemblyFileVersion(Rev.Major + "." + Rev.Minor + "." + Rev.Patch + "." + Rev.Revision)]
[assembly: AssemblyInformationalVersion(Rev.Major + "." + Rev.Minor + "." + Rev.Patch + "-" + Rev.Configuration + "-" + Rev.Revision + "[" + Rev.Hash + "]")]
[assembly: AssemblyConfiguration(Rev.Configuration)]

//[assembly: AssemblyTimeStamp("2017-12-22 15:59:09 +0300", "2017-12-22 15:59:09 +0300")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2243:AttributeStringLiteralsShouldParseCorrectly",
	Justification = "AssemblyInformationalVersion does not need to be a parsable version")]

static class Rev
{ 
	public const string Major = "0";
	public const string Minor = "8";
	public const string Patch = "12";
	public const string Configuration = "dev";
	public const string Revision = "286";
	public const string Hash = "abc7d76";
}
