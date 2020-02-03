using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace XLuaTest
{
    [LuaCallCSharp]
    public class Instance : MonoBehaviour
    {
        internal static LuaEnv luaEnv = new LuaEnv(); //all lua behaviour shared one luaenv only!

        void Start()
        {
            luaEnv.DoString("require 'Instance'"); //入口/Resources/
        }
    }
}
