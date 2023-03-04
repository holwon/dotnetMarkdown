# 异步控制

## 异步函数的顺序控制

参考 E:\a2230\Documents\projects\.NET Core\Y-异步async await\多线程顺序控制.等待_netFK版本2

> [!NOTE]
>
> 即使开发时通过延迟几毫秒来控制了线程的启动顺序, 即使可能很多时候都是正确控制了顺序, 但总有几次会失败的.
>
> 随着我们的程序运行数据量的累积或用户访问量的增加,
> 服务器性能的瓶颈, 会越来越明显.
>
> 🌟如果总是靠延迟来控制顺序, 程序的性能会很**低下**
>
> 我们应该使用 "*回调/状态等待/信号量*" 来控制顺序

在 .net framework 版本中可以使用委托进行异步

```csharp
    // net core 版本已经不支持这种 BeginInvoke 异步写法, 微软推荐 TAP 模式, 即: Task
    IAsyncResult result = action.BeginInvoke("1", null, null);
```

### 使用回调进行顺序控制

对于顺序控制, 可以使用回调函数

> [!TIP]
>
> 委托 BeginInvoke 用法: (方法参数, 回调函数, 状态参数)
>
> 状态参数: 想传递给回调函数的变量/信息 -> 其可以由 `AsyncState` 进行获取

> [!NOTE]
>
> 此处委托指向的函数和回调函数都是由子线程一并完成的

```csharp
Action<string> action = DoSth;
AsyncCallback callback = new AsyncCallback(ia =>
{
    Console.WriteLine($"计算结束");
    Console.WriteLine("线程Id: " + Thread.CurrentThread.ManagedThreadId);

    Console.WriteLine(ia.AsyncState); // The call executed on thread
});
// net core 版本已经不支持这种 BeginInvoke 异步写法, 微软推荐 TAP 模式
IAsyncResult result = action.BeginInvoke("1", callback, "The call executed on thread");
```

### 使用等待进行顺序控制

详情参考: E:\a2230\Documents\projects\.NET Core\Y-异步async await\多线程顺序控制.等待_netFK版本2

> [!NOTE]
>
> 使用委托进行的异步, 其子线程需要释放吗?
>
> 答: 其实很很多时候咱们用这个委托一步调呢, 我们就没考虑上释放. 这不是代表不释放啊, 其实我们把它执行结束后呢, 这个线程呢它本身就算执行完了的啊, 他其实自己呢就会慢慢释放**回归**的.
>
> 但是可能没那么快. 如果我们希望能够快速释放这个委托的异步调用线程呢, 那我们就使用这个 EndInvoke. 你如果使用EndInvoke 的话呢, 他把 EndInvoke 执行完了, 就会线程回归.
> 但是你不使用也没关系, 他最后还是要回调, 还是要回收的啊
