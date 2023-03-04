# asp dotnet core 中使用 JWT 来进行授权

## 前言

本程序运行环境为 dotnet7.102, 使用 Sqlite 作为数据库, 使用 EF Core 作读写.

生成JWT需要3个包

1. Microsoft.AspNetCore.Authentication.JwtBearer
2. Microsoft.IdentityModel.Tokens //此包在dotnet 7中自带
3. System.IdentityModel.Tokens.Jwt //此包在dotnet 7中自带

## JWT

首先, 想要生成 `JWT` 首先我们需要先了解 `JWT` 的结构

具体可查阅 [JWT解释](./JWT解释.md)

## 程序预置设置

### 1. 设置 appsettings.json 文件

添加 Key, Issuer, Audience

```json
{
  "JWT": {
    "Key": "9%^!^pW_S7XwMlU4",
    "Issuer": "http://localhost:5053",
    "Audience": "http://localhost:5053"
  },
  "ConnectionStrings": {
    "DefaultConnectionString": "Data Source=./Auth.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### 2. 添加身份验证服务

Program.cs 文件设置

```csharp
var configuration = builder.Configuration;

#region Set configuration
var connectionString = configuration.GetConnectionString("DefaultConnectionString")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnectionString' not found.");
var issuer = configuration["JWT:Issuer"]?.ToString()
    ?? throw new InvalidOperationException("Configuration 'Issuer' not found.");
var audience = configuration["JWT:Audience"]?.ToString()
    ?? throw new InvalidOperationException("Configuration 'Audience' not found.");
var key = configuration["JWT:Key"]?.ToString()
    ?? throw new InvalidOperationException("Configuration 'Key' not found.");
var encryptKey = Encoding.UTF8.GetBytes(key);
#endregion

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        // 这个是由加密密钥生成的解密密钥
        IssuerSigningKey = new SymmetricSecurityKey(encryptKey)
    };
});

var app = builder.Build();

app.UseAuthentication(); // 添加验证, 验证必须在授权前面
app.UseAuthorization();
```

> [!NOTE]
>
> 如果是使用 WebApi 程序, 我们可以对 SwaggerGen 框架添加身份验证的窗口

> [!TIP]
>
> 需预先加入包 Swashbuckle.AspNetCore.Filters

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
```

### 编写 Api

#### 1. 编写登录, 注册功能

```csharp
[HttpPost("register"), AllowAnonymous]
public async Task<IActionResult> Register([FromBody] UserDto request)
{
    var result = CheckUserAlready(request.UserName);
    if (result)
        return BadRequest("User already");

    CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

    // 这里只是在思考数据库是直接存放 byte还是转成 base64
    // 不过最后还是存放了原本的 byte
    string hash = Convert.ToBase64String(passwordHash);
    string salt = Convert.ToBase64String(passwordSalt);
    User user = new()
    {
        UserName = request.UserName,
        PasswordHash = passwordHash,
        PasswordSalt = passwordSalt,
        Role = nameof(Role.User)
    };
    await _context.Users.AddAsync(user);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
}

[HttpPost("Login")]
public IActionResult Login([FromBody] UserDto request)
{
    var user = _userService.GetUser(request.UserName);

    if (user is null)
        return NotFound("User is not registered");
    bool isPass = CheckUserPassword(request.Password, user);

    if (!isPass)
        return BadRequest("Password is incorrect");

    string jwt = CreateToken(user);
    return Ok(jwt);
}

private bool CheckUserPassword(string password, User user)
    => VerifyPasswordHash(password, user.PasswordHash!, user.PasswordSalt!);
```

#### 2. 完善 token 的生成

```csharp
private string CreateToken(User user)
{
    var issuer = _configuration["JWT:Issuer"]?.ToString()
        ?? throw new InvalidOperationException("Configuration 'Issuer' not found.");
    var audience = _configuration["JWT:Audience"]?.ToString()
        ?? throw new InvalidOperationException("Configuration 'Audience' not found.");
    var key = _configuration["JWT:Key"]?.ToString()
        ?? throw new InvalidOperationException("Configuration 'Key' not found.");
    // Header 自动生成
    // Payload
    List<Claim> claims = new()
    {
        new(ClaimTypes.Name,user.UserName),
        new(ClaimTypes.Role,user.Role),
        new(ClaimTypes.Email,user.Email ?? "")
    };
    // VERIFY SigNature
    var enkey = Encoding.UTF8.GetBytes(key);
    var securityKey = new SymmetricSecurityKey(enkey);
    var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    // Generate Token
    var token = new JwtSecurityToken(
        issuer: issuer,
        audience: audience,
        claims: claims,
        expires: DateTime.Now.AddMinutes(5),
        signingCredentials: credential
    );
    string jwt = new JwtSecurityTokenHandler().WriteToken(token);
    return jwt;
}

private bool CheckUserAlready(string name)
{
    var result = _context.Users.Any(u => u.UserName == name);
    return result;
}

private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
{
    using var hmac = new HMACSHA256();
    passwordSalt = hmac.Key;// 不必担心 key 存储数据库会导致密码逆向破解, hash过程会损失一部分信息.理论上不可逆
    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
}

private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
{
    using var hmac = new HMACSHA256(passwordSalt);
    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    return computedHash.SequenceEqual(passwordHash);
}
```
