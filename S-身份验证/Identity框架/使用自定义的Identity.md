# 使用自定义的Identity.md

添加包:

```dotnetcli
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore 
```

## 配置标识

### 定义类

定义框架所需要的三个类: MyUser, MyRole, MyContext
即: **用户**、**用户身份**、**数据上下文**

```csharp
// **用户**
public class MyUser : IdentityUser<Guid>{ }
// **用户身份**
public class MyRole : IdentityRole<Guid>{ }
// **数据上下文**
public class MyContext : IdentityDbContext<MyUser, MyRole, Guid>
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {
    }
}
```

### 配置服务

```csharp
var sqliteConnection = builder.Configuration.GetConnectionString("sqliteConnection")
?? throw new InvalidOperationException("Configuration sqliteConnection not found.");
builder.Services.AddDbContext<MyContext>(option =>
{
    option.UseSqlite(sqliteConnection);
});

builder.Services.AddDataProtection();
builder.Services.AddIdentityCore<MyUser>(option =>
{
    var passwordOptions = option.Password;
    passwordOptions.RequireDigit = false;// 密码是否必须包含数字
    passwordOptions.RequireLowercase = false;
    passwordOptions.RequireUppercase = false;
    passwordOptions.RequiredLength = 6;
    option.Password = passwordOptions;
    // 指定验证 token 的样式, 设置为简单模式. DefaultEmailProvider生成的 token(验证码) 样式比较简单
    option.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    // 与上方同理.
    option.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
});

// Note: 直接新建实例便好, 会自动附加. 可于构造函数中查看
IdentityBuilder identity = new(typeof(MyUser), typeof(MyRole), builder.Services);
identity.AddEntityFrameworkStores<MyContext>()
    .AddDefaultTokenProviders()
    .AddUserManager<UserManager<MyUser>>()
    .AddRoleManager<RoleManager<MyRole>>();
```
