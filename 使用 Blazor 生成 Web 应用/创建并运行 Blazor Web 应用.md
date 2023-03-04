## 创建

```.NET
dotnet new blazorserver -f net6.0
```

- 如果 Visual Studio Code 提示你安装所需的资产，请选择“是”。
  ![Screenshot showing Visual Studio Code prompting to install assets required to build and debug.](https://docs.microsoft.com/zh-cn/learn/modules/build-blazor-webassembly-visual-studio-code/media/missing-assets-visual-studio-code.png)

## 运行应用程序

在终端窗口中复制粘贴以下命令，在监视模式中运行应用：

```.NET
dotnet watch run
```

这将生成并启动应用，然后在你每次更改代码时重新生成并重启应用。 浏览器会在地址 https://localhost:5000. 处自动打开 浏览器可能会警告你站点不安全；此时可以继续安全操作。

![Screenshot showing the default Blazor WebAssembly client app running in a browser.](https://docs.microsoft.com/zh-cn/learn/modules/build-blazor-webassembly-visual-studio-code/media/hello-blazor.png)

- 准备停止运行时，在 Visual Studio Code 中返回到终端并按 Ctrl+C 来停止应
