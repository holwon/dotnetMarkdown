
// Console.WriteLine(test.TValue);
// test t = new test();


for (var i = 0; i < 5; i++)
{
    Console.WriteLine(GenericCache2<int>.GetCacheTypeTime());
    Thread.Sleep(100);
    Console.WriteLine(GenericCache2<long>.GetCacheTypeTime());
    Thread.Sleep(100);
    Console.WriteLine(GenericCache2<string>.GetCacheTypeTime());
    Thread.Sleep(100);
}


public class test
{
    // 静态构造函数只有第一次调用时才会被调用
    static test()
    {
        Console.WriteLine("test call static constructor");
        TValue = string.Format("{0}_{1}", typeof(test).FullName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        _value = string.Format("{0}_{1}", typeof(test).FullName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        Console.WriteLine(TValue + "\n" + _value);
    }

    public test()
    {
        Console.WriteLine("test1");
    }
    public static string TValue = "";
    private static string _value = "";
}


#region Dictionary 做缓存的本质
// 字典是一种哈希分布的, 找数据时是先需要把 Key 进行 hash, 然后找内存地址, 再找数据,
// 而且数据如果放多了, 那么范围会很大. 导致 hash 查找有很大的开销.
public class DictionaryCache<T>
{
    private Dictionary<string, T> _cache = new Dictionary<string, T>();

    public T Get(string key)
    {
        if (_cache.ContainsKey(key))
        {
            return _cache[key];
        }
        // default value 表达式生成类型的默认值。 
        // Console.WriteLine(default(int));  // output: 0
        return default(T);
    }

    public void Set(string key, T value)
    {
        _cache[key] = value;
    }
}
#endregion


/// <summary>
/// 对于每一个不同的 T，都会生成一个不同的类型的 GenericCache。
/// 明明有字典用,为什么用泛型缓存？ 答: 泛型缓存比字典要快很多很多倍
/// <para>泛型缓存的缺点:</para>
/// 泛型缓存是不能主动释放的, 是没办法清理的, 只能在程序结束时自动清理。
/// 因为是静态的, 他在内存里面是不会丢的
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericCache2<T>
{
    static GenericCache2()
    {
        Console.WriteLine("Static ctor called for type {0}", typeof(T));
        _typeTime = string.Format("{0}_{1}", typeof(T).FullName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    private static string _typeTime = "";

    public static string GetCacheTypeTime()
    {
        return _typeTime;
    }
}