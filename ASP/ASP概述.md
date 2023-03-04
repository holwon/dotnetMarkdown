# ASP.NET Core概述

> [!TIP]
>
> Asp.net Core平台的目的是接收 http 请求并发送响应.
>
> 对他们来说，哪些 asp 委托给中间件组件
> 中间件组件被安排在一个称为请求管道(`Pipe`)的链中，当一个新的 http 请求到达时，它就需要通过管道(`Pipe`)
>
> 一旦请求通过管道(asp平台负责响应)，链中的中间件组件将根据需要以某种方式检查和修改它
>
> 一些组件专注于生成支持请求的响应，但其他组件提供一些特性，如格式化特定数据类型或读写cookie
>
> 如果中间件组件没有生成响应，那么asp将返回一个带有HTTP 404未找到状态码的响应。

## Service

> services are objects that provide features in a web application

> [!NOTE]
> **服务**是在 Web 应用程序中提供任何功能的对象
>
> 任何**类**可以用作服务，并且对服务提供的功能没有限制
