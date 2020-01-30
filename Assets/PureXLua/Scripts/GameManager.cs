using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class GameManager : MonoBehaviour
{
    internal static LuaEnv luaEnv = new LuaEnv(); //all lua behaviour shared one luaenv only!
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //Init();

        //luaEnv.DoString("require 'Lua/LuaA'"); //必须跟路径
        //luaEnv.DoString("require 'Lua.LuaA'"); //或用.的方式

        luaEnv.AddBuildin("rapidjson", XLua.LuaDLL.Lua.LoadRapidJson);
        luaEnv.DoString("require 'rapidjson'");

        //luaEnv.AddBuildin("pb", XLua.LuaDLL.Lua.LoadPb);  //自定义扩展
        luaEnv.AddBuildin("pb", XLua.LuaDLL.Lua.LoadPb);
        luaEnv.DoString("require 'protobufmain'");
    }

    public static string luacode
    {
        get 
        {
            string code = string.Empty;
            code = Resources.Load<TextAsset>("addressbook.proto").text;
            return code;
        }
    }

    private void Init()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        CheckExtractResource();
    }

    //逐一检查本地文件存在与MD5
    void CheckExtractResource()
    {
        if (true)
            OnUpdateResource();
    }

    // 更新AssetBundle
    void OnUpdateResource()
    {
        OnResourceInited();
    }

    // 更新完成后
    void OnResourceInited()
    {
        //luaEnv.AddLoader(MyLoader);

        var gameManagerLua = Resources.Load<TextAsset>("Lua/Manager/GameManager.lua");
        luaEnv.DoString(gameManagerLua.text); //控制权交给Lua
    }

    public byte[] MyLoader(ref string filepath)
    {
        //2.streamingAssets下的lua文件加载
        //string path = Application.streamingAssetsPath + "/" + filepath + ".lua.txt";
        string path = Application.dataPath + "/Resources";
        //lua中代码文本转换字节数组
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(path));
    }
}
