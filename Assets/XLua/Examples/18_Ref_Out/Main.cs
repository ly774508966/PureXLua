using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace XLuaTest
{
    [LuaCallCSharp]
    public class Main : MonoBehaviour
    {
        internal static LuaEnv luaEnv = new LuaEnv(); //all lua behaviour shared one luaenv only!

        void Start()
        {
            luaEnv.DoString("require 'Main'"); //入口/Resources/
        }
    }
}
