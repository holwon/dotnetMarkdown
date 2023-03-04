# 使用 Identity 框架

参考:
[配置标识支持](https://learn.microsoft.com/zh-cn/training/modules/secure-aspnet-core-identity/1-introduction)

添加包

```dotnetcli
dotnet-aspnet-codegenerator
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore 
dotnet add package Microsoft.AspNetCore.Identity.UI 
dotnet add package Microsoft.EntityFrameworkCore.Design 
dotnet add package Microsoft.EntityFrameworkCore.SqlServer 
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

使用基架向项目添加默认标识组件。 在终端中运行以下命令：

```dotnetcli
dotnet aspnet-codegenerator identity --useDefaultUI --dbContext RazorPagesPizzaAuth
```
