using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace XLuaTest
{
    [LuaCallCSharp]
    public class LuaAdapter : MonoBehaviour
    {
        public TextAsset luaScript;
        //public Injection[] injections = new Injection[] { };

        internal static float lastGCTime = 0;
        internal const float GCInterval = 1;//1 second 

        private Action luaAwake;
        private Action luaStart;
        private Action luaUpdate;
        private Action luaOnDestroy;
        private Action<Collider> luaOnTriggerEnter;

        private LuaTable scriptEnv;

        void Awake()
        {
            scriptEnv = GameManager.luaEnv.NewTable();

            // 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
            LuaTable meta = GameManager.luaEnv.NewTable();
            meta.Set("__index", GameManager.luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();


            scriptEnv.Set("self", this);
            /*
            foreach (var injection in injections)
            {
                scriptEnv.Set(injection.name, injection.value);
            }
            GameManager.luaEnv.DoString(luaScript.text, "LuaTestScript", scriptEnv);
            */


            luaAwake = scriptEnv.Get<Action>("awake");
            scriptEnv.Get("start", out luaStart);
            scriptEnv.Get("update", out luaUpdate);
            scriptEnv.Get("ondestroy", out luaOnDestroy);
            scriptEnv.Get("ontriggerenter", out luaOnTriggerEnter);

            if (luaAwake != null)
            {
                luaAwake();
            }
        }

        void Start()
        {
            if (luaStart != null)
            {
                luaStart();
            }
        }

        void Update()
        {
            if (luaUpdate != null)
            {
                luaUpdate();
            }
            if (Time.time - LuaBehaviour.lastGCTime > GCInterval)
            {
                GameManager.luaEnv.Tick();
                LuaBehaviour.lastGCTime = Time.time;
            }
        }

        void OnDestroy()
        {
            if (luaOnDestroy != null)
            {
                luaOnDestroy();
            }
            luaOnTriggerEnter = null;
            luaOnDestroy = null;
            luaUpdate = null;
            luaStart = null;
            scriptEnv.Dispose();
            //injections = null;
        }

        void OnTriggerEnter(Collider other)
        {
            if (luaOnTriggerEnter != null) 
            {
                luaOnTriggerEnter(other);
            }
        }
    }
}
