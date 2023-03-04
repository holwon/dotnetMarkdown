# 按位比较和Flags特性

> 程序路径: E:\a2230\Documents\projects\.NET Core\F-Flags特性\自己尝试--未使用 Flags

## 为什么使用

我们在进行属性赋予时, 可能会想要使一个对象的某个属性拥有多种情况

> 如: 一本小说的书籍类型(Format)只能有 `Hardback(精装书)` 或 `Paperback(简装书)`, 不可两种同时拥有.
>
> 单本的实体书确实如此, 但我们这里试图表示的不是单本书, 而是书店书籍中的小说.
>
> 书店的小说是可以有 `Hardback` 款和 `Paperback` 款的.

```csharp
public enum Formats
{
    // 一开始未使用 Flags 特性, 导致一本书只能有一个枚举
    // 也就是说, 这种情况下, 一本书只能拥有一种书籍类型. 
    // 即: 一本小说只能有 Hardback(精装书) 或 Paperback(简装书), 不可两种同时拥有
    // 单本的实体书确实如此, 但我们这里试图表示的不是单本书, 而是书店书籍中的小说
    // 小说可以有 Hardback 款和 Paperback 款.
    // None, Hardback, Paperback, Ebook, AudioBook

    // Caution: 此处必须赋值为2的倍数, 为了方便, 可以以移位的方式赋值
    // 如果是默认的 0,1,2,3 的数, 会导致 按位或 操作时的数据错误
    // 如: Hardback=1, Paperback=2, Ebook=3
    // Ebook 会 = Hardback | Paperback 的结果, 即: 1 | 2 = 3 (Ebook = Hardback | Paperback)
    // 是明显的数据错误
    None = 0, Hardback = 1, Paperback = 2, Ebook = 4, AudioBook = 8
}

public class Books
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    // public Formats Format { get; set; } // 我们表示的是小说可拥有的书籍类型
    public Formats AvailableFormats { get; set; } // 所以应该使用 Available 命名

    public override string ToString() => $"{Title} by {Author} ({AvailableFormats})";
}
```

```csharp
// 参考 https://youtu.be/Pp7T-O3dIrs
var book = new Books()
{
    Title = "book1",
    Author = "author1",
    // AvailableFormats = Formats.AudioBook | Formats.Paperback

    // Caution: Hardbacks 和 AudioBook 相或 = 9, Format 没有此值. 
    // 将直接输出 book1 by author1(9)
    AvailableFormats = Formats.Hardback | Formats.AudioBook
};
System.Console.WriteLine(book);

// 如果两者相 "与" 不为0, 则代表 AvailableFormats 拥有此属性
// 例子 Hardback = 1 = 0b0001, Paperback = 2 = 0b0010
// 二者相 "或", AvailableFormats = 3 = 0b0011
// AvailableFormats 和 Paperback 相 "与" 后, AvailableFormats = 2 = 0b0010
// 如果相与失败, 即: 二者二进制数没有对上, 相与得数将 = 0
// 所以以下代码可写成
if ((book.AvailableFormats & Formats.AudioBook) != 0)
    Console.WriteLine($"It's Available is AudioBook");

// 我们可以使用 HasFlag 方法, 其与上方代码等效. 此方法在 dotnetFK 4 版本中就已添加
if (book.AvailableFormats.HasFlag(Formats.AudioBook))
{
    Console.WriteLine($"It's Available is AudioBook");
}
```

## 使用 Flags 特性

[Flags]的微软解释是“指示可以将枚举作为位域（即一组标志）处理。”其实就是在编写枚举类型时，上面附上Flags特性后，用该枚举变量是既可以象整数一样进行按位的“|”或者按位的“&”操作了。

修改上方代码, 添加 `[Flags]` 特性.

```csharp
[Flags]
public enum Formats
{
    None = 0, Hardback = 1, Paperback = 2, Ebook = 4, AudioBook = 8
}

var book = new Books()
{
    Title = "book1",
    Author = "author1",
    // AvailableFormats = Formats.AudioBook | Formats.Paperback

    // Caution: Hardbacks 和 AudioBook 相或 = 9, Format 没有此值. 
    // 但使用了 Flags 特性, 将输出 book1 by author1 (Hardback, AudioBook)
    AvailableFormats = Formats.Hardback | Formats.AudioBook
};
System.Console.WriteLine(book);
```
