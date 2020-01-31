using UnityEngine;
using XLua;

namespace XLuaTest
{
    public class LuaSocket : MonoBehaviour
    {
        void Start()
        {
            LuaEnv luaenv = new LuaEnv();
            luaenv.DoString("require 'LuaSocketTest'"); //不用手动AddBuildin，已经在官方LuaEnv中内置了
            luaenv.Dispose();
        }
    }
}
