using System.IO;
using UnityEngine;
using XLua;

namespace XLuaTest
{
    public class CustomLoader : MonoBehaviour
    {
        void Start()
        {
            LuaEnv luaenv = new LuaEnv();
            luaenv.AddLoader(MyLoader); //加载搜索lua文件的路径
            luaenv.Dispose();
        }

        private byte[] MyLoader(ref string filePath)
        {
            string path = Path.Combine(Application.streamingAssetsPath, filePath + ".lua.txt");
            //系统转字节数组
            return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(path));
        }
    }
}
