using System;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class MainClass
{
    public string name = "Ocean";
    public static int age = 100;

    public void CSharpMethod(string name, out int count)
    {
        Debug.Log("这是C#里的一个方法");
        count = name.Length;
    }

    public string TestRef(string name, ref int count)
    {
        Debug.Log(name);
        Debug.Log(count);
        count = name.Length;
        return name + count;
    }

    public float SmoothDamp(float from_x, float to_x, ref float speed_x, float smoothTime)
    {
        return 0;
    }
}
