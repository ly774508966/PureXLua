using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance;

    private Transform parent;
    Transform Parent
    {
        get
        {
            if (parent == null)
            {
                GameObject go = GameObject.Find("Canvas");
                if (go != null) parent = go.transform;
            }
            return parent;
        }
    }

    void Awake()
    {
        Instance = this;
    }

    public void CreatePanel(string className, XLua.LuaFunction func = null)
    {
        Debug.Log("C# CreatePanel: " + className);

        //AssetBundle.LoadAsset();
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + className);

        GameObject go = Instantiate(prefab) as GameObject;
        go.name = className;
        go.layer = LayerMask.NameToLayer("Default");
        go.transform.parent = Parent;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;

        go.AddComponent<XLuaTest.LuaAdapter>();

        if (func != null) func.Call(go);
    }

    public void ClosePanel(string className)
    {
        var panelObj = Parent.Find(className);
        if (panelObj == null) return;
        Destroy(panelObj.gameObject);
    }
}
