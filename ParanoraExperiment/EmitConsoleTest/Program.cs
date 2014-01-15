using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using System.Diagnostics;
using System.Linq.Expressions;

namespace EmitConsoleTest
{
    class Program
    {
        public class User
        {
            private string _name;

            public string Name
            {
                get { return this._name; } 
                set { this._name = value; } 
            }

            public int Age { get; set; }
        }


        public interface IMemberAccessor
        {
            object GetValue(object instance, string memberName);
            void SetValue(object instance, string memberName, object newValue);
        }

        public class ReflectionMemberAccessor : IMemberAccessor
        {
            public object GetValue(object instance, string memberName)
            {
                var propertyInfo = instance.GetType().GetProperty(memberName);
                if (propertyInfo != null)
                {
                    return propertyInfo.GetValue(instance, null);
                }

                return null;
            }

            public void SetValue(object instance, string memberName, object newValue)
            {
                var propertyInfo = instance.GetType().GetProperty(memberName);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(instance, newValue, null);
                }
            }
        }


        internal class PropertyAccessor<T, P> : IMemberAccessor
        {
            private Func<T, P> GetValueDelegate;
            private Action<T, P> SetValueDelegate;

            public PropertyAccessor(Type type, string propertyName)
            {
                var propertyInfo = type.GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    GetValueDelegate = (Func<T, P>)Delegate.CreateDelegate(typeof(Func<T, P>), propertyInfo.GetGetMethod());
                    SetValueDelegate = (Action<T, P>)Delegate.CreateDelegate(typeof(Action<T, P>), propertyInfo.GetSetMethod());
                }
            }

            public object GetValue(object instance)
            {
                return GetValueDelegate((T)instance);
            }

            public void SetValue(object instance, object newValue)
            {
                SetValueDelegate((T)instance, (P)newValue);
            }
        }


        public class DynamicMethod<T> : IMemberAccessor
        {
            internal static Func<object, string, object> GetValueDelegate;

            public object GetValue(object instance, string memberName)
            {
                return GetValueDelegate(instance, memberName);
            }

            static DynamicMethod()
            {
                GetValueDelegate = GenerateGetValue();
            }

            private static Func<object, string, object> GenerateGetValue()
            {
                var type = typeof(T);
                var instance = Expression.Parameter(typeof(object), "instance");
                var memberName = Expression.Parameter(typeof(string), "memberName");
                var nameHash = Expression.Variable(typeof(int), "nameHash");
                var calHash = Expression.Assign(nameHash, Expression.Call(memberName, typeof(object).GetMethod("GetHashCode")));
                var cases = new List<SwitchCase>();
                foreach (var propertyInfo in type.GetProperties())
                {
                    var property = Expression.Property(Expression.Convert(instance, typeof(T)), propertyInfo.Name);
                    var propertyHash = Expression.Constant(propertyInfo.Name.GetHashCode(), typeof(int));

                    cases.Add(Expression.SwitchCase(Expression.Convert(property, typeof(object)), propertyHash));
                }
                var switchEx = Expression.Switch(nameHash, Expression.Constant(null), cases.ToArray());
                var methodBody = Expression.Block(typeof(object), new[] { nameHash }, calHash, switchEx);

                return Expression.Lambda<Func<object, string, object>>(methodBody, instance, memberName).Compile();
            }
        }

        private delegate void HelloWorldDelegate();

        static void Main(string[] args)
        {
            EmitHelloWorldTest();

            
            

            Console.ReadKey();
        }

        static void EmitHelloWorldTest()
        {
            AssemblyName asmName = new AssemblyName("Test");

            AssemblyBuilder asmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                asmName,
                AssemblyBuilderAccess.RunAndSave);

            ModuleBuilder mdlBldr = asmBuilder.DefineDynamicModule("Main", "Main.exe");
            TypeBuilder typeBldr = mdlBldr.DefineType("Hello", TypeAttributes.Public);
            MethodBuilder methodBldr = typeBldr.DefineMethod(
                "SayHello",
                MethodAttributes.Public | MethodAttributes.Static,
                null,//return type
                null//parameter type
                );

            var il = methodBldr.GetILGenerator();//获取il生成器
            il.Emit(OpCodes.Ldstr, "Hello, World");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("ReadLine"));
            il.Emit(OpCodes.Pop);//读入的值会被推送至evaluation stack，而本方法是没有返回值的，因此，需要将栈上的值抛弃
            il.Emit(OpCodes.Ret);

            var t = typeBldr.CreateType();
            asmBuilder.SetEntryPoint(t.GetMethod("SayHello"));
            asmBuilder.Save("Main.exe");

            
        }

        static void EmitHelloWorldTest1()
        {
            //定义一个名为HelloWorld的动态方法，没有返回值，没有参数
            DynamicMethod helloWorldMethod = new DynamicMethod("HelloWorld", null, null);

            //创建一个MSIL生成器，为动态方法生成代码
            ILGenerator helloWorldIL = helloWorldMethod.GetILGenerator();

            //将要输出的Hello World!字符创加载到堆栈上
            helloWorldIL.Emit(OpCodes.Ldstr, "Hello World!");
            //调用Console.WriteLine(string)方法输出Hello World!
            helloWorldIL.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            //方法结束，返回
            helloWorldIL.Emit(OpCodes.Ret);

            //完成动态方法的创建，并且获取一个可以执行该动态方法的委托
            HelloWorldDelegate HelloWorld = (HelloWorldDelegate)helloWorldMethod.CreateDelegate(typeof(HelloWorldDelegate));

            //执行动态方法，将在屏幕上打印Hello World!
            HelloWorld();
        }
    }
}
