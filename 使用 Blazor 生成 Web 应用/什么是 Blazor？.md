什么是 Blazor？
已完成 100 XP

# 什么是 Blazor？

    ****已完成**
    			**
    			**100 XP**

    * 6 分钟

    生成 Web 应用的公司通常为聘请开发人员担任不同角色。 某些开发人员创建后端服务器端逻辑。 其他开发人员生成客户端 Web 应用。 这些开发人员通常使用不同的开发语言和技术。

C# 和 Microsoft .NET 是生成服务器端逻辑的常用选项。 但客户端应用通常是使用 Web UI 框架生成的，该框架通常使用
JavaScript。 使用多种语言和工具集需要掌握多种技能，并且通常需要两个单独的团队。
此外，用于传输和表示数据的代码必须使用两种语言生成并保持同步。

在本单元中，首先介绍 Blazor，然后探讨 Razor 组件。

## 什么是 Blazor？

Blazor 应用包含使用 C#、HTML 和 CSS 生成的可重复使用的 Web UI 组件。 借助 Blazor，开发人员可以使用
C# 生成客户端和服务器代码。 他们还可以与前端客户端代码和后端逻辑共享代码和库。 使用 C#
生成所有代码可简化在前端和后端之间共享数据，重复使用代码以加速开发和维护。

可以使用 Blazor 生成：

- 通过 WebSocket 连接处理 UI 交互的服务器端代码。
- 通过 WebAssembly 直接在浏览器中运行的客户端 Web 应用。

## 什么是 WebAssembly？

WebAssembly (WASM) 是一种开放的二进制标准。 它用于定义旨在 Web 浏览器中运行的程序的可移植代码格式。 WebAssembly 是一种文本程序集语言，具有专用于实现快速下载和近乎本机性能的精简二进制格式。

WebAssembly 为 C、C++ 和 Rust 等语言提供了编译目标。 它设计为与 JavaScript 一起运行，因此两者可协同工作。 WebAssembly 还可生成可下载和脱机运行的渐进式 Web 应用程序。

## 什么是 Blazor WebAssembly？

使用 Blazor WebAssembly，开发人员可以在浏览器中运行 .NET 代码。 它是一种单页应用框架，使用的是 WebAssembly 开放标准，无需插件或代码生成。

在浏览器中通过 WebAssembly 执行的 .NET 代码在浏览器的 JavaScript 沙盒中运行。 该代码具有沙盒提供的所有安全和保护特性。 这有助于防止客户端计算机上的恶意操作。

![Blazor Web Assembly diagram.](https://docs.microsoft.com/zh-cn/learn/modules/build-blazor-webassembly-visual-studio-code/media/blazor-webassembly.png)

Blazor 使用编译为 WebAssembly 模块的 .NET 运行时，该模块随应用一起下载。 该模块可执行 Blazor 应用中包含的 .NET Standard 代码。

Blazor WebAssembly 应用仅限于执行应用的浏览器的功能。 但该应用可以通过 JavaScript 互操作访问完整的浏览器功能。

### Blazor WebAssembly 支持的浏览器

Blazor WebAssembly 需要新式桌面或移动浏览器。 当前支持以下浏览器：

- Microsoft Edge
- Mozilla Firefox
- Google Chrome
- Apple Safari

### 什么是 Blazor Server？

Blazor 服务器在 ASP.NET Core 应用中添加了对在服务器上托管 Razor 组件的支持。 可通过 SignalR 连接处理 UI 更新。

运行时停留在服务器上并处理：

- 执行应用的 C# 代码。
- 将 UI 事件从浏览器发送到服务器。
- 将 UI 更新应用于服务器发送回的呈现组件。

Blazor 服务器用于与浏览器通信的连接还用于处理 JavaScript 互操作调用。

![Blazor Server diagram.](https://docs.microsoft.com/zh-cn/learn/modules/build-blazor-webassembly-visual-studio-code/media/blazor-server.png)

## Blazor 开发要求

可使用最新版本的 Visual Studio 2019、Visual Studio for Mac 或 Visual Studio Code 来生成 Blazor 应用。 在本模块中，我们将使用 Visual Studio Code。

无论使用哪种开发环境，都需要安装 .NET 6.0 SDK。 安装后，即可开始生成 Blazor 应用。 在下一个练习中，将安装所有必要的工具，以使用 Visual Studio Code 生成 Blazor WebAssembly 应用

    6 分钟

生成 Web 应用的公司通常为聘请开发人员担任不同角色。 某些开发人员创建后端服务器端逻辑。 其他开发人员生成客户端 Web 应用。 这些开发人员通常使用不同的开发语言和技术。

C# 和 Microsoft .NET 是生成服务器端逻辑的常用选项。 但客户端应用通常是使用 Web UI 框架生成的，该框架通常使用 JavaScript。 使用多种语言和工具集需要掌握多种技能，并且通常需要两个单独的团队。 此外，用于传输和表示数据的代码必须使用两种语言生成并保持同步。

在本单元中，首先介绍 Blazor，然后探讨 Razor 组件。
什么是 Blazor？

Blazor 应用包含使用 C#、HTML 和 CSS 生成的可重复使用的 Web UI 组件。 借助 Blazor，开发人员可以使用 C# 生成客户端和服务器代码。 他们还可以与前端客户端代码和后端逻辑共享代码和库。 使用 C# 生成所有代码可简化在前端和后端之间共享数据，重复使用代码以加速开发和维护。

可以使用 Blazor 生成：

    通过 WebSocket 连接处理 UI 交互的服务器端代码。
    通过 WebAssembly 直接在浏览器中运行的客户端 Web 应用。

什么是 WebAssembly？

WebAssembly (WASM) 是一种开放的二进制标准。 它用于定义旨在 Web 浏览器中运行的程序的可移植代码格式。 WebAssembly 是一种文本程序集语言，具有专用于实现快速下载和近乎本机性能的精简二进制格式。

WebAssembly 为 C、C++ 和 Rust 等语言提供了编译目标。 它设计为与 JavaScript 一起运行，因此两者可协同工作。 WebAssembly 还可生成可下载和脱机运行的渐进式 Web 应用程序。
什么是 Blazor WebAssembly？

使用 Blazor WebAssembly，开发人员可以在浏览器中运行 .NET 代码。 它是一种单页应用框架，使用的是 WebAssembly 开放标准，无需插件或代码生成。

在浏览器中通过 WebAssembly 执行的 .NET 代码在浏览器的 JavaScript 沙盒中运行。 该代码具有沙盒提供的所有安全和保护特性。 这有助于防止客户端计算机上的恶意操作。

Blazor Web Assembly diagram.

Blazor 使用编译为 WebAssembly 模块的 .NET 运行时，该模块随应用一起下载。 该模块可执行 Blazor 应用中包含的 .NET Standard 代码。

Blazor WebAssembly 应用仅限于执行应用的浏览器的功能。 但该应用可以通过 JavaScript 互操作访问完整的浏览器功能。
Blazor WebAssembly 支持的浏览器

Blazor WebAssembly 需要新式桌面或移动浏览器。 当前支持以下浏览器：

    a. Microsoft Edge
    b. Mozilla Firefox
    c. Google Chrome
    d. Apple Safari

什么是 Blazor Server？

Blazor 服务器在 ASP.NET Core 应用中添加了对在服务器上托管 Razor 组件的支持。 可通过 SignalR 连接处理 UI 更新。

运行时停留在服务器上并处理：

    a. 执行应用的 C# 代码。
    b. 将 UI 事件从浏览器发送到服务器。
    c. 将 UI 更新应用于服务器发送回的呈现组件。

Blazor 服务器用于与浏览器通信的连接还用于处理 JavaScript 互操作调用。

Blazor Server diagram.
Blazor 开发要求

可使用最新版本的 Visual Studio 2019、Visual Studio for Mac 或 Visual Studio Code 来生成 Blazor 应用。 在本模块中，我们将使用 Visual Studio Code。

无论使用哪种开发环境，都需要安装 .NET 6.0 SDK。 安装后，即可开始生成 Blazor 应用。 在下一个练习中，将安装所有必要的工具，以使用 Visual Studio Code 生成 Blazor WebAssembly 应用。
