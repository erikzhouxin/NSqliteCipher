# NSystem.Data.SQLiteCipher

本仓库基于[efcore](https://github.com/dotnet/efcore)中Microsoft.Data.Sqlite.Core进行开发，因为SQLite需要加密，之前使用的System.Data.SQLite无法使用加密，而Microsoft.Data.Sqlite.Core无法使用连接字符串加密方式，再有项目需要兼容较低版本的.NetFramework，所以干脆将自己的代码和Microsoft.Data.Sqlite（v3.1.9？）进行合并，从而衍生出此项目，后续会跟进Microsoft.Data.Sqlite的更新，使此项目的易用度得到提升，敬请期待！

可以通过Nuget获取此包的发布

```
Install-Package NSystem.Data.SQLiteCipher
```

如侵犯原作者权益，望告知，发现问题立即整改。
