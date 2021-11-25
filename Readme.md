# **NSystem.Data.SQLiteCipher**
本仓库基于[efcore](https://github.com/dotnet/efcore)中Microsoft.Data.Sqlite.Core进行开发，因为SQLite需要加密，之前使用的System.Data.SQLite无法使用加密，而Microsoft.Data.Sqlite.Core无法使用连接字符串加密方式，再有项目需要兼容较低版本的.NetFramework，所以将自己的代码和Microsoft.Data.Sqlite（v5.0.9）进行合并，从而衍生出此项目（NSystem.Data.SQLiteCipher）。而对于使用EntityFramework这个ORM来说，它对于Microsoft.Data.Sqlite.Core的依赖使得也许重新封装EFCore，所以形成另一个项目（NSystem.Data.SQLiteCipherEntity），后续会跟进efcore的更新，使此项目的易用度得到提升，敬请期待！<br/>
除此之外还包括【NSQLitePCL.Raw.Core】和【NSQLitePCL.Raw.Liber】，移植于[SQLitePCL.raw](https://github.com/ericsink/SQLitePCL.raw)（v2.0.6），由于SQLitePCL.raw系列生成过于繁琐，所以将此库中的项目删繁就简，只保留应用中的Sqlcipher部分，但依旧可以支持SQLitePCLRaw.lib.*的组件。<br/>
## **类库介绍**
1.使用连接字符串中带密码以及版本号等内容进行匹配，未加载连接池应用。如，<br/>

>DataSource=xxx.sqlite;Version=3;Password=xxx;

2.可使用SqliteConnectionPool.GetConnection(string connString)使用连接池进行连接获取SqliteConnection，可大大减少频繁DbConnection.Open的耗时。<br/>
## **安装说明**
可以通过Nuget获取此包的发布

```
Install-Package NSystem.Data.SQLiteCipher
```
## **免责声明**
如侵犯原作者权益，望告知，发现问题立即整改。
