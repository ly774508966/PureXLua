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
        Init();

        //luaEnv.AddLoader(MyLoader);

        //luaEnv.DoString("require 'Lua/LuaA'"); //必须跟路径
        //luaEnv.DoString("require 'Lua.LuaA'"); //或用.的方式

        //第三方库
        //luaEnv.AddBuildin("rapidjson", XLua.LuaDLL.Lua.LoadRapidJson);
        //luaEnv.DoString("require 'rapidjson'");
        //luaEnv.AddBuildin("pb", XLua.LuaDLL.Lua.LoadPb);
        //luaEnv.DoString("require 'protobufmain'");
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
        var gameManagerLua = Resources.Load<TextAsset>("Lua/Manager/GameManager.lua");
        luaEnv.DoString(gameManagerLua.text); //控制权交给Lua
    }

    #region 测试代码

    public static string luacode
    {
        get { return Resources.Load<TextAsset>("addressbook.proto").text; }
    }

    public byte[] MyLoader(ref string filepath)
    {
        //streamingAssets作为lua加载路径
        string path = Path.Combine(Application.streamingAssetsPath, filepath + ".lua.txt");
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(path));
    }

    #endregion
}
