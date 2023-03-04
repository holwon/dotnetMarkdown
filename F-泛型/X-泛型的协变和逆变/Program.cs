using F泛型的协变和逆变.Model;
using System;

Bird bird1 = new Bird();
Bird bird2 = new Sparrow();

List<Bird> birds1 = new List<Bird>();

// 按照常理来讲, 一堆的 麻雀Sparrow 应当是 =一堆的 Bird, 但是这里却会报错. 为什么呢?
// 明明在前面一个一个来的时候是可以让 麻雀 等于 Bird 的

// List<Bird> birds2 = new List<Sparrow>(); // error

// 回答: 因为他们并没有父子关系
// 此处我们用的是 List<T>, List<T> 是一个泛型类型, 会随着 <T> 的变化而变化
// 所以 List<Bird> 和 List<Sparrow> 是不同的类型, 他们并没有父子关系

// 虽然可以这样强制转换
List<Bird> birds3 = new List<Sparrow>().Select(x => (Bird)x).ToList();

// 协变只能用在接口或者委托
// out 修饰 covariant 协变
// in 修饰 contravariant 逆变
// 其实协变逆变都很好理解, 他本质只是修复泛型的缺陷.
// 修复泛型, 如: "List<Bird>" 和 "List<Sparrow>" 是不同的类型的缺陷
// 通过预先规定泛型是作为"输出类型" `out` 或者作为"输入类型" `in` 来修复这个缺陷
// 协变 .NET 4.0的升级
#region 协变
IEnumerable<Bird> birds4 = new List<Bird>();
IEnumerable<Bird> birds5 = new List<Sparrow>();// ok.

// 源码中多了个 out 关键字, `out T` 以后, 这个T必须是返回值的类型, 它是不能作为参数的. this is a covariant return type
// public interface IEnumerable<out T> : IEnumerable{
//     IEnumerator<T> GetEnumerator();
// }

// public delegate TResult Func<out TResult>();
Func<IEnumerable<Bird>> func1 = () => new List<Bird>();
Func<Bird> func2 = () => new Sparrow();
Func<Bird> func3 = new Func<Bird>(() => new Sparrow());
#endregion

// 自己的协变 demo 一堆的麻雀 Sparrow 可以被 =一堆的 Bird
ICustomerList<Bird> customerList1 = new CustomerListOut<Bird>();
ICustomerList<Bird> customerList2 = new CustomerListOut<Sparrow>();

// 自己的逆变 demo 一堆的 Bird 可以被 =一堆的麻雀 Sparrow
ICustomerListIn<Sparrow> customerList3 = new CustomerListIn<Bird>();
// ICustomerListIn<Bird> customerList4 = new CustomerListIn<Sparrow>();// error


#region 协变 out demo
public interface ICustomerList<out T>
{
    // T this[int index] { get; }
    T GetCustomer(int index);

    public T GetCustomer();
    // void Add(T item);// compiler error :如果加了 out 关键字, 就只能作为返回值类型, 不能作为输入参数类型
}

// 实例化
public class CustomerListOut<T> : ICustomerList<T>
{
    public T GetCustomer(int index)
    {
        return default(T);
    }

    public T GetCustomer()
    {
        return default(T);
    }
}
#endregion


// 逆变
#region 逆变
public interface ICustomerListIn<in T>
{
    // T this[int index] { get; }
    void Add(T item);
    void Show(T item);
}

// 实例化
public class CustomerListIn<T> : ICustomerListIn<T>
{
    public void Add(T item)
    {
    }

    public void Show(T item)
    {
        throw new NotImplementedException();
    }
}

#endregion
