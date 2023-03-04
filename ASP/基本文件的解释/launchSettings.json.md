# [launchSettings.json 的概述](./launchSettings.json.md)

文件相对路径 *`\Properties\launchSettings.json`*

文件本体

```json
{
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:19100",
      "sslPort": 44327
    }
  },
  "profiles": {
    "LearnCore.WebSite": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:7084;http://localhost:5084",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}

```

## 字段解释

### "profiles" 字段

> [!NOTE]
> **LearnCore.WebSite** 是项目的**名称**

```json
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:7084;http://localhost:5084",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
```

> [!NOTE]
> 其定义了 `dotnet run` 命令使用的配置，这是启动时的默认配置. 定义了该应用程序可以通过控制台命令，也可以通过简单地在有或没有调试的情况下启动它
