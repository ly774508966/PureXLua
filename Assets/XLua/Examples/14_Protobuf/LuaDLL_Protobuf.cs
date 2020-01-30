namespace XLua.LuaDLL
{
	using System;
	using System.Text;
	using System.Runtime.InteropServices;
	using XLua;

	public partial class Lua
	{
		//增加lua-protobuf支持
		[DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_pb(System.IntPtr L);
		[MonoPInvokeCallback(typeof(LuaDLL.lua_CSFunction))]
		public static int LoadPb(System.IntPtr L)
		{
			return luaopen_pb(L);
		}

		[DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_protobuf_c(System.IntPtr L);
		[MonoPInvokeCallback(typeof(LuaDLL.lua_CSFunction))]
		public static int LoadProtobufC(System.IntPtr L)
		{
			return luaopen_protobuf_c(L);
		}
	}
}