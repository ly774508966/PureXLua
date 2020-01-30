# PureXLua

## 说明

由于Lua在移动端热更新上有着庞大的市场基础，以及XLua在优化方面的良好表现，希望实现一套基于XLua的纯Lua框架，方便高效搭建新的项目。

## 结构

- UI层
- 网络层
- 逻辑层

## 工具(Toolkit)

Proto生成

## 增加第三方库子模块

引用build_xlua_with_libs，已集成rapidjson，pbc，lua-protobuf，等。

VS2019编译修改bat
```
cmake -G "Visual Studio 16 2019" -A x64 ..
```

## 增加Example

14. 增加AddBuildin第三方插件调用示例；
15. CustomLoader；

## 参加资料

- [XLua官方项目](https://github.com/Tencent/xLua)
- [submodule使用](https://blog.csdn.net/guotianqing/article/details/82391665)
