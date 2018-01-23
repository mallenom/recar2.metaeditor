using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace Native.OpenGL
{
	/// <summary>Функции библиотеки opengl32.dll.</summary>
	[SuppressUnmanagedCodeSecurity]
#pragma warning disable IDE1006 // Naming Styles
	static class gl
#pragma warning restore IDE1006 // Naming Styles
	{
		private const string DllName = "opengl32.dll";

		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static T GetExtension<T>()
			where T : class
		{
			var type = typeof(T);
			var attr = type.GetCustomAttribute<OpenGLExtensionAttribute>();
			if(attr == null)
			{
				throw new InvalidOperationException("Specified type is not an OpenGL extension method.");
			}
			var procPtr = GetProcAddress(attr.Name);
			if(procPtr == IntPtr.Zero)
			{
				return default(T);
			}
			return Marshal.GetDelegateForFunctionPointer(procPtr, type) as T;
		}

		[DllImport(DllName, EntryPoint = "glGetString")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		private static extern IntPtr GetStringInternal(StringNames name);

		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static string GetString(StringNames name)
		{
			var ptr = GetStringInternal(name);
			return Marshal.PtrToStringAnsi(ptr);
		}

		[DllImport(DllName, EntryPoint = "glGetError")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern Errors GetError();

		[DllImport(DllName, EntryPoint = "glClearColor")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void ClearColor(float red, float green, float blue, float alpha);

		[DllImport(DllName, EntryPoint = "glClear")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Clear(BufferMask mask);

		[DllImport(DllName, EntryPoint = "glFlush")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Flush();

		[DllImport(DllName, EntryPoint = "glFinish")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Finish();

		[DllImport(DllName, EntryPoint = "glEnable")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Enable(SwitchTarget cap);

		[DllImport(DllName, EntryPoint = "glIsEnabled")]
		[return: MarshalAs(UnmanagedType.Bool)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern bool IsEnabled(SwitchTarget cap);

		[DllImport(DllName, EntryPoint = "glDisable")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Disable(SwitchTarget cap);

		[DllImport(DllName, EntryPoint = "glPolygonOffset")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void PolygonOffset(float a, float b);

		[DllImport(DllName, EntryPoint = "glLineWidth")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void LineWidth(float width);

		[DllImport(DllName, EntryPoint = "glBlendFunc")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void BlendFunc(BlendMode sfactor, BlendMode dfactor);

		[DllImport(DllName, EntryPoint = "glPolygonMode")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void PolygonMode(FaceType face, PolygonMode mode);

		[DllImport(DllName, EntryPoint = "glCullFace")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void CullFace(FaceType face);

		[DllImport(DllName, EntryPoint = "glFrontFace")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void FrontFace(FrontFace mode);

		[DllImport(DllName, EntryPoint = "glViewport")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Viewport(int x, int y, int width, int height);


		[DllImport(DllName, EntryPoint = "glTexCoord1i")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(int s);

		[DllImport(DllName, EntryPoint = "glTexCoord1s")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(short s);

		[DllImport(DllName, EntryPoint = "glTexCoord1f")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(float s);

		[DllImport(DllName, EntryPoint = "glTexCoord1d")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(double s);

		[DllImport(DllName, EntryPoint = "glTexCoord2i")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(int s, int t);

		[DllImport(DllName, EntryPoint = "glTexCoord2s")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(short s, short t);

		[DllImport(DllName, EntryPoint = "glTexCoord2f")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(float s, float t);

		[DllImport(DllName, EntryPoint = "glTexCoord2d")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(double s, double t);

		[DllImport(DllName, EntryPoint = "glTexCoord3i")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(int s, int t, int r);

		[DllImport(DllName, EntryPoint = "glTexCoord3s")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(short s, short t, short r);

		[DllImport(DllName, EntryPoint = "glTexCoord3f")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(float s, float t, float r);

		[DllImport(DllName, EntryPoint = "glTexCoord3d")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(double s, double t, float r);

		[DllImport(DllName, EntryPoint = "glTexCoord4i")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(int s, int t, int r, int q);

		[DllImport(DllName, EntryPoint = "glTexCoord4s")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(short s, short t, short r, short q);

		[DllImport(DllName, EntryPoint = "glTexCoord4f")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(float s, float t, float r, float q);

		[DllImport(DllName, EntryPoint = "glTexCoord4d")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexCoord(double s, double t, float r, float q);


		[DllImport(DllName, EntryPoint = "glColor3b")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(sbyte red, sbyte green, sbyte blue);

		[DllImport(DllName, EntryPoint = "glColor3s")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(short red, short green, short blue);

		[DllImport(DllName, EntryPoint = "glColor3i")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(int red, int green, int blue);

		[DllImport(DllName, EntryPoint = "glColor3f")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(float red, float green, float blue);

		[DllImport(DllName, EntryPoint = "glColor3d")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(double red, double green, double blue);

		[DllImport(DllName, EntryPoint = "glColor3ub")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(byte red, byte green, byte blue);

		[DllImport(DllName, EntryPoint = "glColor3us")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(ushort red, ushort green, ushort blue);

		[DllImport(DllName, EntryPoint = "glColor3ui")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(uint red, uint green, uint blue);

		[DllImport(DllName, EntryPoint = "glColor4b")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(sbyte red, sbyte green, sbyte blue, sbyte alpha);

		[DllImport(DllName, EntryPoint = "glColor4s")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(short red, short green, short blue, short alpha);

		[DllImport(DllName, EntryPoint = "glColor4i")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(int red, int green, int blue, int alpha);

		[DllImport(DllName, EntryPoint = "glColor4f")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(float red, float green, float blue, float alpha);

		[DllImport(DllName, EntryPoint = "glColor4d")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(double red, double green, double blue, double alpha);

		[DllImport(DllName, EntryPoint = "glColor4ub")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(byte red, byte green, byte blue, byte alpha);

		[DllImport(DllName, EntryPoint = "glColor4us")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(ushort red, ushort green, ushort blue, ushort alpha);

		[DllImport(DllName, EntryPoint = "glColor4ui")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Color(uint red, uint green, uint blue, uint alpha);


		[DllImport(DllName, EntryPoint = "glVertex2i")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Vertex(int x, int y);

		[DllImport(DllName, EntryPoint = "glVertex2s")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Vertex(short x, short y);

		[DllImport(DllName, EntryPoint = "glVertex2f")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Vertex(float x, float y);

		[DllImport(DllName, EntryPoint = "glVertex2d")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Vertex(double x, double y);

		[DllImport(DllName, EntryPoint = "glVertex3i")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Vertex(int x, int y, int z);

		[DllImport(DllName, EntryPoint = "glVertex3s")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Vertex(short x, short y, short z);

		[DllImport(DllName, EntryPoint = "glVertex3f")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Vertex(float x, float y, float z);

		[DllImport(DllName, EntryPoint = "glVertex3d")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Vertex(double x, double y, double z);

		[DllImport(DllName, EntryPoint = "glVertex4i")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Vertex(int x, int y, int z, int w);

		[DllImport(DllName, EntryPoint = "glVertex4s")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Vertex(short x, short y, short z, short w);

		[DllImport(DllName, EntryPoint = "glVertex4f")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Vertex(float x, float y, float z, short w);

		[DllImport(DllName, EntryPoint = "glVertex4d")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Vertex(double x, double y, double z, double w);


		[DllImport(DllName, EntryPoint = "glVertexPointer")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void VertexPointer(int size, DataTypes type, int stride, float[] pointer);

		[DllImport(DllName, EntryPoint = "glEnableClientState")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void EnableClientState(VertexArrays array);

		[DllImport(DllName, EntryPoint = "glDrawArrays")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void DrawArrays(BeginMode mode, int first, int count);

		[DllImport(DllName, EntryPoint = "glDrawElements")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void DrawElements(BeginMode mode, int count, DataTypes type, IntPtr indices);

		[DllImport(DllName, EntryPoint = "wglGetCurrentContext")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr GetCurrentContext();

		[DllImport(DllName, EntryPoint = "wglCreateContext")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr CreateContext(IntPtr deviceContext);

		[DllImport(DllName, EntryPoint = "wglMakeCurrent", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern bool MakeCurrent(IntPtr deviceContext, IntPtr renderingContext);

		[DllImport(DllName, EntryPoint = "wglShareLists", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern bool ShareLists(IntPtr hglrc1, IntPtr hglrc2);

		[DllImport(DllName, EntryPoint = "glBegin")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Begin(BeginMode mode);

		[DllImport(DllName, EntryPoint = "glEnd")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void End();

		[DllImport(DllName, EntryPoint = "wglDeleteContext")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern bool DeleteContext(IntPtr renderingContext);

		[DllImport(DllName, EntryPoint = "wglGetProcAddress")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern IntPtr GetProcAddress(string name);


		[DllImport(DllName, EntryPoint = "glGenTextures")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		private static extern void CreateTexturesInternal([In] int n, [Out, MarshalAs(UnmanagedType.LPArray)] uint[] textures);

		[DllImport(DllName, EntryPoint = "glGenTextures")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		private static extern void CreateTexturesInternal([In] int n, [Out] out uint texture);

		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static void CreateTextures(uint[] textures)
		{
			if(textures != null && textures.Length > 0)
			{
				CreateTexturesInternal(textures.Length, textures);
			}
		}

		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static uint CreateTexture()
		{
			uint texture;
			CreateTexturesInternal(1, out texture);
			return texture;
		}

		[DllImport(DllName, EntryPoint = "glDeleteTextures")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		private static extern void DeleteTexturesInternal([In] int n, [In, MarshalAs(UnmanagedType.LPArray)] uint[] textures);

		[DllImport(DllName, EntryPoint = "glDeleteTextures")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		private static extern void DeleteTexturesInternal([In] int n, [In] ref uint texture);

		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static void DeleteTextures(uint[] textures)
		{
			if(textures != null && textures.Length > 0)
			{
				DeleteTexturesInternal(textures.Length, textures);
			}
		}

		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static void DeleteTexture(uint texture)
		{
			DeleteTexturesInternal(1, ref texture);
		}

		[DllImport(DllName, EntryPoint = "glBindTexture")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void BindTexture(GetTargets target, uint texture);

		[DllImport(DllName, EntryPoint = "glPixelStorei")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void PixelStore(PixelStore pname, int param);

		[DllImport(DllName, EntryPoint = "glTexImage2D")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexImage2D(GetTargets target, int level, TextureFormats internalformat, int width, int height, int border, PixelFormats format, DataTypes type, IntPtr pixels);

		[DllImport(DllName, EntryPoint = "glTexParameteri")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexParameter(GetTargets target, TextureParameters parameter, TextureParameterValues value);

		[DllImport(DllName, EntryPoint = "glTexParameterf")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexParameter(GetTargets target, TextureParameters parameter, float value);

		[DllImport(DllName, EntryPoint = "glTexParameteri")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void TexParameter(GetTargets target, TextureParameters parameter, int value);

		//[DllImport(DllName, EntryPoint = "glTexEnvf")]
		//public static extern void TexEnv(GLenum target, GLenum pname, float value);

		//[DllImport(DllName, EntryPoint = "glTexEnvi")]
		//public static extern void TexEnv(GLenum target, GLenum pname, int value);

		[DllImport(DllName, EntryPoint = "glGetIntegerv")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		private static extern void GetIntegerV(GetTargets pname, [Out] int[] @params);

		[DllImport(DllName, EntryPoint = "glGetIntegerv")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		private static extern void GetIntegerV(GetTargets pname, out int @params);

		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static int GetInteger(GetTargets target)
		{
			int result;
			GetIntegerV(target, out result);
			return result;
		}

		[DllImport(DllName, EntryPoint = "glGetFloatv")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		private static extern void GetFloatV(GetTargets pname, [Out] float[] v);

		[DllImport(DllName, EntryPoint = "glGetFloatv")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		private static extern void GetFloatV(GetTargets pname, out float v);

		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static float GetFloat(GetTargets target)
		{
			float result;
			GetFloatV(target, out result);
			return result;
		}

		[DllImport(DllName, EntryPoint = "glPointSize")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void PointSize(float size);


		[DllImport(DllName, EntryPoint = "glLoadIdentity")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void LoadIdentity();

		[DllImport(DllName, EntryPoint = "glMatrixMode")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void MatrixMode(MatrixMode mode);

		[DllImport(DllName, EntryPoint = "glPushMatrix")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void PushMatrix();

		[DllImport(DllName, EntryPoint = "glPopMatrix")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void PopMatrix();

		[DllImport(DllName, EntryPoint = "glOrtho")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Ortho(double left, double right, double bottom, double top, double nearVal, double farVal);

		[DllImport(DllName, EntryPoint = "glTranslatef")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Translate(float x, float y, float z);

		[DllImport(DllName, EntryPoint = "glTranslated")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Translate(double x, double y, double z);

		[DllImport(DllName, EntryPoint = "glRotatef")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Rotate(float angle, float x, float y, float z);

		[DllImport(DllName, EntryPoint = "glRotated")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Rotate(double angle, double x, double y, double z);

		[DllImport(DllName, EntryPoint = "glScalef")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Scale(float x, float y, float z);

		[DllImport(DllName, EntryPoint = "glScaled")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Scale(double x, double y, double z);

		[DllImport(DllName, EntryPoint = "glMultMatrixd")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void MultMatrix([In] double[] m);

		[DllImport(DllName, EntryPoint = "glMultMatrixf")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void MultMatrix([In] float[] m);

		[DllImport(DllName, EntryPoint = "glDepthFunc")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void DepthFunc(DepthFunction func​);

		[DllImport(DllName, EntryPoint = "glClearDepth")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void ClearDepth(double depth);

		[DllImport(DllName, EntryPoint = "glClearStencil")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void ClearStencil(int s);

		[DllImport(DllName, EntryPoint = "glScissor")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void Scissor(int x, int y, int width, int height);

		[DllImport(DllName, EntryPoint = "glDrawBuffer")]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void DrawBuffer(DrawBufferMode buf);
	}

#pragma warning disable IDE1006 // Naming Styles
	/// <summary>wgl-расширения OpenGL.</summary>
	static class wgl
#pragma warning restore IDE1006 // Naming Styles
	{
		[return: MarshalAs(UnmanagedType.Bool)]
		[OpenGLExtension(name: "wglSwapIntervalEXT")]
		[SuppressUnmanagedCodeSecurity]
		public delegate bool SetSwapInterval(int interval);

		[OpenGLExtension(name: "wglGetSwapIntervalEXT")]
		[SuppressUnmanagedCodeSecurity]
		public delegate int GetSwapInterval();
	}

	[System.AttributeUsage(AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	sealed class OpenGLExtensionAttribute : Attribute
	{
		public OpenGLExtensionAttribute(string name)
		{
			Name = name;
		}

		public string Name { get; }
	}

	enum DepthFunction : uint
	{
		Never          = 0x0200,
		Less           = 0x0201,
		Equal          = 0x0202,
		LessOrEqual    = 0x0203,
		Greater        = 0x0204,
		NotEqual       = 0x0205,
		GreaterOrEqual = 0x0206,
		Always         = 0x0207,
	}

	enum MatrixMode : uint
	{
		ModelView  = 0x1700,
		Projection = 0x1701,
		Texture    = 0x1702,
	}

	[Flags]
	enum BufferMask : uint
	{
		GL_DEPTH_BUFFER_BIT   = 0x00000100,
		GL_STENCIL_BUFFER_BIT = 0x00000400,
		GL_COLOR_BUFFER_BIT   = 0x00004000
	}

	enum SwitchTarget : uint
	{
		GL_ALPHA_TEST = 0x0BC0,
		GL_COLOR_MATERIAL = 0x0B57,
		GL_DEPTH_TEST = 0x0B71,
		GL_LIGHTING = 0x0B50,
		GL_STENCIL_TEST = 0x0B90,
		GL_CULL_FACE = 0x0B44,
		GL_POLYGON_OFFSET_POINT = 0x2A01,
		GL_POLYGON_OFFSET_LINE = 0x2A02,
		GL_POLYGON_OFFSET_FILL = 0x8037,
		GL_BLEND = 0x0BE2,
		GL_LINE_SMOOTH = 0x0B20,
		GL_TEXTURE_2D = 0x0DE1
	}

	enum BeginMode : uint
	{
		GL_POINTS = 0x0000,
		GL_LINES = 0x0001,
		GL_LINE_LOOP = 0x0002,
		GL_LINE_STRIP = 0x0003,
		GL_TRIANGLES = 0x0004,
		GL_TRIANGLE_STRIP = 0x0005,
		GL_TRIANGLE_FAN = 0x0006,
		GL_QUADS = 0x0007,
		GL_QUAD_STRIP = 0x0008,
		GL_POLYGON = 0x0009
	}

	enum ShaderType : uint
	{
		GL_VERTEX_SHADER = 0x8B31,
		GL_FRAGMENT_SHADER = 0x8B30
	}

	enum ShaderStatus : uint
	{
		GL_DELETE_STATUS = 0x8B80,
		GL_COMPILE_STATUS = 0x8B81,
		GL_LINK_STATUS = 0x8B82,
		GL_VALIDATE_STATUS = 0x8B83,
		GL_INFO_LOG_LENGTH = 0x8B84
	}

	enum PolygonMode : uint
	{
		GL_POINT = 0x1B00,
		GL_LINE = 0x1B01,
		GL_FILL = 0x1B02
	}

	enum FaceType : uint
	{
		GL_FRONT = 0x0404,
		GL_BACK = 0x0405,
		GL_FRONT_AND_BACK = 0x0408
	}

	enum FrontFace : uint
	{
		GL_CW = 0x0900,
		GL_CCW = 0x0901
	}

	enum TextureFormats : uint
	{
		GL_ALPHA4 = 0x803B,
		GL_ALPHA8 = 0x803C,
		GL_ALPHA12 = 0x803D,
		GL_ALPHA16 = 0x803E,
		GL_LUMINANCE4 = 0x803F,
		GL_LUMINANCE8 = 0x8040,
		GL_LUMINANCE12 = 0x8041,
		GL_LUMINANCE16 = 0x8042,
		GL_LUMINANCE4_ALPHA4 = 0x8043,
		GL_LUMINANCE6_ALPHA2 = 0x8044,
		GL_LUMINANCE8_ALPHA8 = 0x8045,
		GL_LUMINANCE12_ALPHA4 = 0x8046,
		GL_LUMINANCE12_ALPHA12 = 0x8047,
		GL_LUMINANCE16_ALPHA16 = 0x8048,
		GL_INTENSITY = 0x8049,
		GL_INTENSITY4 = 0x804A,
		GL_INTENSITY8 = 0x804B,
		GL_INTENSITY12 = 0x804C,
		GL_INTENSITY16 = 0x804D,
		GL_R3_G3_B2 = 0x2A10,
		GL_RGB4 = 0x804F,
		GL_RGB5 = 0x8050,
		GL_RGB8 = 0x8051,
		GL_RGB10 = 0x8052,
		GL_RGB12 = 0x8053,
		GL_RGB16 = 0x8054,
		GL_RGBA2 = 0x8055,
		GL_RGBA4 = 0x8056,
		GL_RGB5_A1 = 0x8057,
		GL_RGBA8 = 0x8058,
		GL_RGB10_A2 = 0x8059,
		GL_RGBA12 = 0x805A,
		GL_RGBA16 = 0x805B
	}

	enum PixelFormats : uint
	{
		GL_COLOR_INDEX = 0x1900,
		GL_STENCIL_INDEX = 0x1901,
		GL_DEPTH_COMPONENT = 0x1902,
		GL_RED = 0x1903,
		GL_GREEN = 0x1904,
		GL_BLUE = 0x1905,
		GL_ALPHA = 0x1906,
		GL_RGB = 0x1907,
		GL_RGBA = 0x1908,
		GL_LUMINANCE = 0x1909,
		GL_LUMINANCE_ALPHA = 0x190A,
		GL_BGR = 0x80E0,
		GL_BGRA = 0x80E1
	}

	enum DrawBufferMode : uint
	{
		GL_NONE = 0,
		GL_FRONT_LEFT = 0x0400,
		GL_FRONT_RIGHT = 0x0401,
		GL_BACK_LEFT = 0x0402,
		GL_BACK_RIGHT = 0x0403,
		GL_FRONT = 0x0404,
		GL_BACK = 0x0405,
		GL_LEFT = 0x0406,
		GL_RIGHT = 0x0407,
		GL_FRONT_AND_BACK = 0x0408,
		GL_AUX0 = 0x0409,
		GL_AUX1 = 0x040A,
		GL_AUX2 = 0x040B,
		GL_AUX3 = 0x040C
	}

	enum BlendMode : uint
	{
		GL_ZERO = 0,
		GL_ONE = 1,
		GL_SRC_COLOR = 0x0300,
		GL_ONE_MINUS_SRC_COLOR = 0x0301,
		GL_SRC_ALPHA = 0x0302,
		GL_ONE_MINUS_SRC_ALPHA = 0x0303,
		GL_DST_ALPHA = 0x0304,
		GL_ONE_MINUS_DST_ALPHA = 0x0305,
		GL_DST_COLOR = 0x0306,
		GL_ONE_MINUS_DST_COLOR = 0x0307,
		GL_SRC_ALPHA_SATURATE = 0x0308
	}

	enum StringNames : uint
	{
		GL_VENDOR = 0x1F00,
		GL_RENDERER = 0x1F01,
		GL_VERSION = 0x1F02,
		GL_EXTENSIONS = 0x1F03,
		GL_SHADING_LANGUAGE_VERSION = 0x8B8C
	}

	enum Errors : uint
	{
		GL_NO_ERROR = 0,
		GL_INVALID_ENUM = 0x0500,
		GL_INVALID_VALUE = 0x0501,
		GL_INVALID_OPERATION = 0x0502,
		GL_STACK_OVERFLOW = 0x0503,
		GL_STACK_UNDERFLOW = 0x0504,
		GL_OUT_OF_MEMORY = 0x0505
	}

	enum BindBufferTargets : uint
	{
		GL_ARRAY_BUFFER = 0x8892,
		GL_ELEMENT_ARRAY_BUFFER = 0x8893,
		GL_UNIFORM_BUFFER = 0x8A11
	}

	enum BufferUsages : uint
	{
		GL_STREAM_DRAW = 0x88E0,
		GL_STREAM_READ = 0x88E1,
		GL_STREAM_COPY = 0x88E2,
		GL_STATIC_DRAW = 0x88E4,
		GL_STATIC_READ = 0x88E5,
		GL_STATIC_COPY = 0x88E6,
		GL_DYNAMIC_DRAW = 0x88E8,
		GL_DYNAMIC_READ = 0x88E9,
		GL_DYNAMIC_COPY = 0x88EA
	}

	enum DataTypes : uint
	{
		GL_BYTE = 0x1400,
		GL_UNSIGNED_BYTE = 0x1401,
		GL_SHORT = 0x1402,
		GL_UNSIGNED_SHORT = 0x1403,
		GL_INT = 0x1404,
		GL_UNSIGNED_INT = 0x1405,
		GL_FLOAT = 0x1406,
		GL_2_BYTES = 0x1407,
		GL_3_BYTES = 0x1408,
		GL_4_BYTES = 0x1409,
		GL_DOUBLE = 0x140A
	}

	enum VertexArrays : uint
	{
		GL_VERTEX_ARRAY = 0x8074,
		GL_NORMAL_ARRAY = 0x8075,
		GL_COLOR_ARRAY = 0x8076,
		GL_INDEX_ARRAY = 0x8077,
		GL_TEXTURE_COORD_ARRAY = 0x8078,
		GL_EDGE_FLAG_ARRAY = 0x8079,
		GL_VERTEX_ARRAY_SIZE = 0x807A,
		GL_VERTEX_ARRAY_TYPE = 0x807B,
		GL_VERTEX_ARRAY_STRIDE = 0x807C,
		GL_NORMAL_ARRAY_TYPE = 0x807E,
		GL_NORMAL_ARRAY_STRIDE = 0x807F,
		GL_COLOR_ARRAY_SIZE = 0x8081,
		GL_COLOR_ARRAY_TYPE = 0x8082,
		GL_COLOR_ARRAY_STRIDE = 0x8083,
		GL_INDEX_ARRAY_TYPE = 0x8085,
		GL_INDEX_ARRAY_STRIDE = 0x8086,
		GL_TEXTURE_COORD_ARRAY_SIZE = 0x8088,
		GL_TEXTURE_COORD_ARRAY_TYPE = 0x8089,
		GL_TEXTURE_COORD_ARRAY_STRIDE = 0x808A,
		GL_EDGE_FLAG_ARRAY_STRIDE = 0x808C,
		GL_VERTEX_ARRAY_POINTER = 0x808E,
		GL_NORMAL_ARRAY_POINTER = 0x808F,
		GL_COLOR_ARRAY_POINTER = 0x8090,
		GL_INDEX_ARRAY_POINTER = 0x8091,
		GL_TEXTURE_COORD_ARRAY_POINTER = 0x8092,
		GL_EDGE_FLAG_ARRAY_POINTER = 0x8093,
		GL_V2F = 0x2A20,
		GL_V3F = 0x2A21,
		GL_C4UB_V2F = 0x2A22,
		GL_C4UB_V3F = 0x2A23,
		GL_C3F_V3F = 0x2A24,
		GL_N3F_V3F = 0x2A25,
		GL_C4F_N3F_V3F = 0x2A26,
		GL_T2F_V3F = 0x2A27,
		GL_T4F_V4F = 0x2A28,
		GL_T2F_C4UB_V3F = 0x2A29,
		GL_T2F_C3F_V3F = 0x2A2A,
		GL_T2F_N3F_V3F = 0x2A2B,
		GL_T2F_C4F_N3F_V3F = 0x2A2C,
		GL_T4F_C4F_N3F_V4F = 0x2A2D
	}

	enum PixelStore : uint
	{
		GL_PACK_ROW_LENGTH = 0x0D02,
		GL_UNPACK_ROW_LENGTH = 0x0CF2,
		GL_PACK_ALIGNMENT = 0x0D05,
		GL_UNPACK_ALIGNMENT = 0x0CF5,
	}

	enum GetTargets : uint
	{
		GL_CURRENT_COLOR = 0x0B00,
		GL_CURRENT_INDEX = 0x0B01,
		GL_CURRENT_NORMAL = 0x0B02,
		GL_CURRENT_TEXTURE_COORDS = 0x0B03,
		GL_CURRENT_RASTER_COLOR = 0x0B04,
		GL_CURRENT_RASTER_INDEX = 0x0B05,
		GL_CURRENT_RASTER_TEXTURE_COORDS = 0x0B06,
		GL_CURRENT_RASTER_POSITION = 0x0B07,
		GL_CURRENT_RASTER_POSITION_VALID = 0x0B08,
		GL_CURRENT_RASTER_DISTANCE = 0x0B09,
		GL_POINT_SMOOTH = 0x0B10,
		GL_POINT_SIZE = 0x0B11,
		GL_POINT_SIZE_RANGE = 0x0B12,
		GL_POINT_SIZE_GRANULARITY = 0x0B13,
		GL_LINE_SMOOTH = 0x0B20,
		GL_LINE_WIDTH = 0x0B21,
		GL_LINE_WIDTH_RANGE = 0x0B22,
		GL_LINE_WIDTH_GRANULARITY = 0x0B23,
		GL_LINE_STIPPLE = 0x0B24,
		GL_LINE_STIPPLE_PATTERN = 0x0B25,
		GL_LINE_STIPPLE_REPEAT = 0x0B26,
		GL_LIST_MODE = 0x0B30,
		GL_MAX_LIST_NESTING = 0x0B31,
		GL_LIST_BASE = 0x0B32,
		GL_LIST_INDEX = 0x0B33,
		GL_POLYGON_MODE = 0x0B40,
		GL_POLYGON_SMOOTH = 0x0B41,
		GL_POLYGON_STIPPLE = 0x0B42,
		GL_EDGE_FLAG = 0x0B43,
		GL_CULL_FACE = 0x0B44,
		GL_CULL_FACE_MODE = 0x0B45,
		GL_FRONT_FACE = 0x0B46,
		GL_LIGHTING = 0x0B50,
		GL_LIGHT_MODEL_LOCAL_VIEWER = 0x0B51,
		GL_LIGHT_MODEL_TWO_SIDE = 0x0B52,
		GL_LIGHT_MODEL_AMBIENT = 0x0B53,
		GL_SHADE_MODEL = 0x0B54,
		GL_COLOR_MATERIAL_FACE = 0x0B55,
		GL_COLOR_MATERIAL_PARAMETER = 0x0B56,
		GL_COLOR_MATERIAL = 0x0B57,
		GL_FOG = 0x0B60,
		GL_FOG_INDEX = 0x0B61,
		GL_FOG_DENSITY = 0x0B62,
		GL_FOG_START = 0x0B63,
		GL_FOG_END = 0x0B64,
		GL_FOG_MODE = 0x0B65,
		GL_FOG_COLOR = 0x0B66,
		GL_DEPTH_RANGE = 0x0B70,
		GL_DEPTH_TEST = 0x0B71,
		GL_DEPTH_WRITEMASK = 0x0B72,
		GL_DEPTH_CLEAR_VALUE = 0x0B73,
		GL_DEPTH_FUNC = 0x0B74,
		GL_ACCUM_CLEAR_VALUE = 0x0B80,
		GL_STENCIL_TEST = 0x0B90,
		GL_STENCIL_CLEAR_VALUE = 0x0B91,
		GL_STENCIL_FUNC = 0x0B92,
		GL_STENCIL_VALUE_MASK = 0x0B93,
		GL_STENCIL_FAIL = 0x0B94,
		GL_STENCIL_PASS_DEPTH_FAIL = 0x0B95,
		GL_STENCIL_PASS_DEPTH_PASS = 0x0B96,
		GL_STENCIL_REF = 0x0B97,
		GL_STENCIL_WRITEMASK = 0x0B98,
		GL_MATRIX_MODE = 0x0BA0,
		GL_NORMALIZE = 0x0BA1,
		GL_VIEWPORT = 0x0BA2,
		GL_MODELVIEW_STACK_DEPTH = 0x0BA3,
		GL_PROJECTION_STACK_DEPTH = 0x0BA4,
		GL_TEXTURE_STACK_DEPTH = 0x0BA5,
		GL_MODELVIEW_MATRIX = 0x0BA6,
		GL_PROJECTION_MATRIX = 0x0BA7,
		GL_TEXTURE_MATRIX = 0x0BA8,
		GL_ATTRIB_STACK_DEPTH = 0x0BB0,
		GL_CLIENT_ATTRIB_STACK_DEPTH = 0x0BB1,
		GL_ALPHA_TEST = 0x0BC0,
		GL_ALPHA_TEST_FUNC = 0x0BC1,
		GL_ALPHA_TEST_REF = 0x0BC2,
		GL_DITHER = 0x0BD0,
		GL_BLEND_DST = 0x0BE0,
		GL_BLEND_SRC = 0x0BE1,
		GL_BLEND = 0x0BE2,
		GL_LOGIC_OP_MODE = 0x0BF0,
		GL_INDEX_LOGIC_OP = 0x0BF1,
		GL_COLOR_LOGIC_OP = 0x0BF2,
		GL_AUX_BUFFERS = 0x0C00,
		GL_DRAW_BUFFER = 0x0C01,
		GL_READ_BUFFER = 0x0C02,
		GL_SCISSOR_BOX = 0x0C10,
		GL_SCISSOR_TEST = 0x0C11,
		GL_INDEX_CLEAR_VALUE = 0x0C20,
		GL_INDEX_WRITEMASK = 0x0C21,
		GL_COLOR_CLEAR_VALUE = 0x0C22,
		GL_COLOR_WRITEMASK = 0x0C23,
		GL_INDEX_MODE = 0x0C30,
		GL_RGBA_MODE = 0x0C31,
		GL_DOUBLEBUFFER = 0x0C32,
		GL_STEREO = 0x0C33,
		GL_RENDER_MODE = 0x0C40,
		GL_PERSPECTIVE_CORRECTION_HINT = 0x0C50,
		GL_POINT_SMOOTH_HINT = 0x0C51,
		GL_LINE_SMOOTH_HINT = 0x0C52,
		GL_POLYGON_SMOOTH_HINT = 0x0C53,
		GL_FOG_HINT = 0x0C54,
		GL_TEXTURE_GEN_S = 0x0C60,
		GL_TEXTURE_GEN_T = 0x0C61,
		GL_TEXTURE_GEN_R = 0x0C62,
		GL_TEXTURE_GEN_Q = 0x0C63,
		GL_PIXEL_MAP_I_TO_I = 0x0C70,
		GL_PIXEL_MAP_S_TO_S = 0x0C71,
		GL_PIXEL_MAP_I_TO_R = 0x0C72,
		GL_PIXEL_MAP_I_TO_G = 0x0C73,
		GL_PIXEL_MAP_I_TO_B = 0x0C74,
		GL_PIXEL_MAP_I_TO_A = 0x0C75,
		GL_PIXEL_MAP_R_TO_R = 0x0C76,
		GL_PIXEL_MAP_G_TO_G = 0x0C77,
		GL_PIXEL_MAP_B_TO_B = 0x0C78,
		GL_PIXEL_MAP_A_TO_A = 0x0C79,
		GL_PIXEL_MAP_I_TO_I_SIZE = 0x0CB0,
		GL_PIXEL_MAP_S_TO_S_SIZE = 0x0CB1,
		GL_PIXEL_MAP_I_TO_R_SIZE = 0x0CB2,
		GL_PIXEL_MAP_I_TO_G_SIZE = 0x0CB3,
		GL_PIXEL_MAP_I_TO_B_SIZE = 0x0CB4,
		GL_PIXEL_MAP_I_TO_A_SIZE = 0x0CB5,
		GL_PIXEL_MAP_R_TO_R_SIZE = 0x0CB6,
		GL_PIXEL_MAP_G_TO_G_SIZE = 0x0CB7,
		GL_PIXEL_MAP_B_TO_B_SIZE = 0x0CB8,
		GL_PIXEL_MAP_A_TO_A_SIZE = 0x0CB9,
		GL_UNPACK_SWAP_BYTES = 0x0CF0,
		GL_UNPACK_LSB_FIRST = 0x0CF1,
		GL_UNPACK_ROW_LENGTH = 0x0CF2,
		GL_UNPACK_SKIP_ROWS = 0x0CF3,
		GL_UNPACK_SKIP_PIXELS = 0x0CF4,
		GL_UNPACK_ALIGNMENT = 0x0CF5,
		GL_PACK_SWAP_BYTES = 0x0D00,
		GL_PACK_LSB_FIRST = 0x0D01,
		GL_PACK_ROW_LENGTH = 0x0D02,
		GL_PACK_SKIP_ROWS = 0x0D03,
		GL_PACK_SKIP_PIXELS = 0x0D04,
		GL_PACK_ALIGNMENT = 0x0D05,
		GL_MAP_COLOR = 0x0D10,
		GL_MAP_STENCIL = 0x0D11,
		GL_INDEX_SHIFT = 0x0D12,
		GL_INDEX_OFFSET = 0x0D13,
		GL_RED_SCALE = 0x0D14,
		GL_RED_BIAS = 0x0D15,
		GL_ZOOM_X = 0x0D16,
		GL_ZOOM_Y = 0x0D17,
		GL_GREEN_SCALE = 0x0D18,
		GL_GREEN_BIAS = 0x0D19,
		GL_BLUE_SCALE = 0x0D1A,
		GL_BLUE_BIAS = 0x0D1B,
		GL_ALPHA_SCALE = 0x0D1C,
		GL_ALPHA_BIAS = 0x0D1D,
		GL_DEPTH_SCALE = 0x0D1E,
		GL_DEPTH_BIAS = 0x0D1F,
		GL_MAX_EVAL_ORDER = 0x0D30,
		GL_MAX_LIGHTS = 0x0D31,
		GL_MAX_CLIP_PLANES = 0x0D32,
		GL_MAX_TEXTURE_SIZE = 0x0D33,
		GL_MAX_PIXEL_MAP_TABLE = 0x0D34,
		GL_MAX_ATTRIB_STACK_DEPTH = 0x0D35,
		GL_MAX_MODELVIEW_STACK_DEPTH = 0x0D36,
		GL_MAX_NAME_STACK_DEPTH = 0x0D37,
		GL_MAX_PROJECTION_STACK_DEPTH = 0x0D38,
		GL_MAX_TEXTURE_STACK_DEPTH = 0x0D39,
		GL_MAX_VIEWPORT_DIMS = 0x0D3A,
		GL_MAX_CLIENT_ATTRIB_STACK_DEPTH = 0x0D3B,
		GL_SUBPIXEL_BITS = 0x0D50,
		GL_INDEX_BITS = 0x0D51,
		GL_RED_BITS = 0x0D52,
		GL_GREEN_BITS = 0x0D53,
		GL_BLUE_BITS = 0x0D54,
		GL_ALPHA_BITS = 0x0D55,
		GL_DEPTH_BITS = 0x0D56,
		GL_STENCIL_BITS = 0x0D57,
		GL_ACCUM_RED_BITS = 0x0D58,
		GL_ACCUM_GREEN_BITS = 0x0D59,
		GL_ACCUM_BLUE_BITS = 0x0D5A,
		GL_ACCUM_ALPHA_BITS = 0x0D5B,
		GL_NAME_STACK_DEPTH = 0x0D70,
		GL_AUTO_NORMAL = 0x0D80,
		GL_MAP1_COLOR_4 = 0x0D90,
		GL_MAP1_INDEX = 0x0D91,
		GL_MAP1_NORMAL = 0x0D92,
		GL_MAP1_TEXTURE_COORD_1 = 0x0D93,
		GL_MAP1_TEXTURE_COORD_2 = 0x0D94,
		GL_MAP1_TEXTURE_COORD_3 = 0x0D95,
		GL_MAP1_TEXTURE_COORD_4 = 0x0D96,
		GL_MAP1_VERTEX_3 = 0x0D97,
		GL_MAP1_VERTEX_4 = 0x0D98,
		GL_MAP2_COLOR_4 = 0x0DB0,
		GL_MAP2_INDEX = 0x0DB1,
		GL_MAP2_NORMAL = 0x0DB2,
		GL_MAP2_TEXTURE_COORD_1 = 0x0DB3,
		GL_MAP2_TEXTURE_COORD_2 = 0x0DB4,
		GL_MAP2_TEXTURE_COORD_3 = 0x0DB5,
		GL_MAP2_TEXTURE_COORD_4 = 0x0DB6,
		GL_MAP2_VERTEX_3 = 0x0DB7,
		GL_MAP2_VERTEX_4 = 0x0DB8,
		GL_MAP1_GRID_DOMAIN = 0x0DD0,
		GL_MAP1_GRID_SEGMENTS = 0x0DD1,
		GL_MAP2_GRID_DOMAIN = 0x0DD2,
		GL_MAP2_GRID_SEGMENTS = 0x0DD3,
		GL_TEXTURE_1D = 0x0DE0,
		GL_TEXTURE_2D = 0x0DE1,
		GL_FEEDBACK_BUFFER_POINTER = 0x0DF0,
		GL_FEEDBACK_BUFFER_SIZE = 0x0DF1,
		GL_FEEDBACK_BUFFER_TYPE = 0x0DF2,
		GL_SELECTION_BUFFER_POINTER = 0x0DF3,
		GL_SELECTION_BUFFER_SIZE = 0x0DF4,
		GL_MAX_TEXTURE_MAX_ANISOTROPY_EXT = 0x84FF
	}

	enum TextureUnits : uint
	{
		GL_TEXTURE0  = 0x84C0,
		GL_TEXTURE1  = 0x84C1,
		GL_TEXTURE2  = 0x84C2,
		GL_TEXTURE3  = 0x84C3,
		GL_TEXTURE4  = 0x84C4,
		GL_TEXTURE5  = 0x84C5,
		GL_TEXTURE6  = 0x84C6,
		GL_TEXTURE7  = 0x84C7,
		GL_TEXTURE8  = 0x84C8,
		GL_TEXTURE9  = 0x84C9,
		GL_TEXTURE10 = 0x84CA,
		GL_TEXTURE11 = 0x84CB,
		GL_TEXTURE12 = 0x84CC,
		GL_TEXTURE13 = 0x84CD,
		GL_TEXTURE14 = 0x84CE,
		GL_TEXTURE15 = 0x84CF,
		GL_TEXTURE16 = 0x84D0,
		GL_TEXTURE17 = 0x84D1,
		GL_TEXTURE18 = 0x84D2,
		GL_TEXTURE19 = 0x84D3,
		GL_TEXTURE20 = 0x84D4,
		GL_TEXTURE21 = 0x84D5,
		GL_TEXTURE22 = 0x84D6,
		GL_TEXTURE23 = 0x84D7,
		GL_TEXTURE24 = 0x84D8,
		GL_TEXTURE25 = 0x84D9,
		GL_TEXTURE26 = 0x84DA,
		GL_TEXTURE27 = 0x84DB,
		GL_TEXTURE28 = 0x84DC,
		GL_TEXTURE29 = 0x84DD,
		GL_TEXTURE30 = 0x84DE,
		GL_TEXTURE31 = 0x84DF,
	}

	enum TextureParameters : uint
	{
		GL_TEXTURE_MAG_FILTER = 0x2800,
		GL_TEXTURE_MIN_FILTER = 0x2801,
		GL_TEXTURE_WRAP_S = 0x2802,
		GL_TEXTURE_WRAP_T = 0x2803,
		GL_TEXTURE_MAX_ANISOTROPY_EXT = 0x84FE
	}

	enum TextureParameterValues : uint
	{
		GL_NEAREST = 0x2600,
		GL_LINEAR = 0x2601,
		GL_NEAREST_MIPMAP_NEAREST = 0x2700,
		GL_LINEAR_MIPMAP_NEAREST = 0x2701,
		GL_NEAREST_MIPMAP_LINEAR = 0x2702,
		GL_LINEAR_MIPMAP_LINEAR = 0x2703,
		GL_CLAMP_TO_EDGE = 0x812F,
		GL_CLAMP = 0x2900,
		GL_MIRRORED_REPEAT = 0x8370,
		GL_REPEAT = 0x2901
	}

	enum FrameBufferBindTarget : uint
	{
		GL_READ_FRAMEBUFFER = 0x8CA8,
		GL_DRAW_FRAMEBUFFER = 0x8CA9,
		GL_FRAMEBUFFER = 0x8D40
	}

	enum FrameBufferAttachment : uint
	{
		GL_COLOR_ATTACHMENT0 = 0x8CE0,
		GL_COLOR_ATTACHMENT1 = 0x8CE1,
		GL_COLOR_ATTACHMENT2 = 0x8CE2,
		GL_COLOR_ATTACHMENT3 = 0x8CE3,
		GL_COLOR_ATTACHMENT4 = 0x8CE4,
		GL_COLOR_ATTACHMENT5 = 0x8CE5,
		GL_COLOR_ATTACHMENT6 = 0x8CE6,
		GL_COLOR_ATTACHMENT7 = 0x8CE7,
		GL_COLOR_ATTACHMENT8 = 0x8CE8,
		GL_COLOR_ATTACHMENT9 = 0x8CE9,
		GL_COLOR_ATTACHMENT10 = 0x8CEA,
		GL_COLOR_ATTACHMENT11 = 0x8CEB,
		GL_COLOR_ATTACHMENT12 = 0x8CEC,
		GL_COLOR_ATTACHMENT13 = 0x8CED,
		GL_COLOR_ATTACHMENT14 = 0x8CEE,
		GL_COLOR_ATTACHMENT15 = 0x8CEF,
		GL_DEPTH_ATTACHMENT = 0x8D00,
		GL_STENCIL_ATTACHMENT = 0x8D20
	}

	enum RenderBufferBindTarget : uint
	{
		GL_RENDERBUFFER = 0x8D41
	}

	enum RenderBufferComponent : uint
	{
		GL_DEPTH_COMPONENT16 = 0x81A5,
		GL_DEPTH_COMPONENT24 = 0x81A6,
		GL_DEPTH_COMPONENT32 = 0x81A7
	}

	enum FrameBufferStatus : uint
	{
		GL_FRAMEBUFFER_COMPLETE = 0x8CD5,
		GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 0x8CD6,
		GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 0x8CD7,
		GL_FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER = 0x8CDB,
		GL_FRAMEBUFFER_INCOMPLETE_READ_BUFFER = 0x8CDC,
		GL_FRAMEBUFFER_UNSUPPORTED = 0x8CDD
	}
}
