using System.Web;
using FindProperty.Lib.Cache;
using FindProperty.Lib.Ioc;
using Microsoft.Practices.Unity;

namespace FindProperty.Lib.Ioc.Unity
{
    public class UnityContext : IIoCContext
    {
        private static UnityContext _current;
        private static UnityComponent _componet;
        private static UnityCreator _creator;
        private static IUnityContainer _kernel;

        public UnityContext() {

        }

        public IIoCContext Context
        {
            get
            {
                if (_current == null)
                {
                    _current = new UnityContext();
                }
                return _current;
            }
        }

        public IIoCComponent Componet
        {
            get 
            {
                if (_componet == null)
                {
                    _componet = new UnityComponent();
                }
                return _componet;
            }
        }

        public IIoCCreator Creator
        {
            get
            {
                if (_creator == null)
                {
                    _creator = new UnityCreator();
                }
                return _creator;
            }
        }

        public T Kernel<T>()
        {
            if (_kernel == null)
            {
                _kernel = Creator.Create<IUnityContainer>();
            }
            return (T)_kernel;
        }
    }
}
