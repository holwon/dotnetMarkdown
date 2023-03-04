namespace 泛型
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Show(123);
            objShow(123);
            Console.WriteLine(typeof(List<>));
            Console.WriteLine(typeof(List<int>));
            Console.WriteLine(typeof(Dictionary<,>));
        }
        /// <summary>
        /// dotnet 2.0推出的新语法
        /// 
        /// 延迟声明: 把参数类型的声明延迟到调用
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        static void Show<T>(T value)
        {
            Console.WriteLine($"{typeof(Program)} + {value.GetType().Name} + {value.ToString()}");
        }

        static void objShow(object value)
        {
            Console.WriteLine($"{typeof(Program)} + {value.GetType().Name} + {value.ToString()}");
        }
    }
}