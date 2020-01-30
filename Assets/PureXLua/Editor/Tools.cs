using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

public class Tools : EditorWindow
{
    static List<string> paths = new List<string>();
    static List<string> files = new List<string>();

    /// <summary>
    /// 遍历目录及其子目录
    /// </summary>
    static void Recursive(string path)
    {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        foreach (string filename in names)
        {
            string ext = Path.GetExtension(filename);
            if (ext.Equals(".meta")) continue;
            files.Add(filename.Replace('\\', '/'));
        }
        foreach (string dir in dirs)
        {
            paths.Add(dir.Replace('\\', '/'));
            Recursive(dir);
        }
    }

    [MenuItem("PureXLua/Build Protobuf-lua-gen File")]
    public static void BuildProtobufFile()
    {
        ClearConsole();

        string dir = Path.Combine(Application.dataPath + "/XLua/Examples/01_Helloworld/Resources/3rd/pblua");
        paths.Clear();
        files.Clear();
        Recursive(dir);
        //UnityEngine.Debug.Log(paths.Count + ", " + files.Count); //0, 3

        //string protoc = "d:/protoc/src/protoc.exe";
        //string protoc_gen_dir = "\"d:/protoc-gen-lua/plugin/protoc-gen-lua.bat\"";
        string root = Path.GetFullPath(@"./"); //上级目录
        string protoc = @"C:\Users\Administrator\Desktop\protobuf-2.5.0\vsprojects\Release\";
        //string protoc = Path.Combine(root, "Protoc/bin/protoc.exe");
        string protoc_gen_dir = Path.Combine(root, "Protoc/bin/lua/");

        foreach (string f in files)
        {
            string name = Path.GetFileName(f);
            string ext = Path.GetExtension(f);
            if (!ext.Equals(".proto")) continue;

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = protoc;
            info.Arguments = " --lua_out=./ --plugin=protoc-gen-lua=" + protoc_gen_dir + " " + name + "\npause";
            info.WindowStyle = ProcessWindowStyle.Normal;
            info.UseShellExecute = true;
            info.WorkingDirectory = dir;
            info.ErrorDialog = true;
            UnityEngine.Debug.Log(info.FileName + " " + info.Arguments);

            Process pro = Process.Start(info);
            pro.WaitForExit();
        }
        AssetDatabase.Refresh();
    }

    static void ClearConsole()
    {
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        Type logEntries = assembly.GetType("UnityEditor.LogEntries");
        MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");
        clearConsoleMethod.Invoke(new object(), null);
        //UnityEngine.Debug.Log("<color=green>clear!</color>");
    }
}
