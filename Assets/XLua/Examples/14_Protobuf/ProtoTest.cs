using UnityEngine;
using XLua;

namespace XLuaTest
{
    public class ProtoTest : MonoBehaviour
    {
        void Start()
        {
            LuaEnv luaenv = new LuaEnv();
            luaenv.AddBuildin("pb", XLua.LuaDLL.Lua.LoadPb);
            luaenv.DoString("require 'ProtoTest'");
            luaenv.Dispose();
        }

        public static string protoStr 
        {
            get
            {
                //return "acca";
                return Resources.Load<TextAsset>("addressbook.proto").text;
            }
        }
    }
}
