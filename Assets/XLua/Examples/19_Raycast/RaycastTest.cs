using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace XLuaTest
{
    [LuaCallCSharp]
    public class RaycastTest : MonoBehaviour
    {
        internal static LuaEnv luaEnv = new LuaEnv(); //all lua behaviour shared one luaenv only!

		void Start()
        {
			//luaEnv.DoString("require 'Main'"); //入口/Resources/
			luaEnv.DoString(@"
				local EventTest = require ('EventTest');
				gameObject = CS.UnityEngine.GameObject.Find('RaycastTest');
				local script = gameObject:AddComponent(typeof(CS.XLuaTest.LuaAdapter));
				script:LoadLua('EventTest.lua', 'EventTest');
			");
		}

		// 射线 //需要重新绑定代码
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, int maxDistance)
		{
			return Physics.Raycast(ray, out hitInfo, maxDistance);
		}
	}
}
