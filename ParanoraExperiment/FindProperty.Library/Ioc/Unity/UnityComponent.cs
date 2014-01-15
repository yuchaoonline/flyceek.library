using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Configuration;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using FindProperty.Lib.Ioc;
using FindProperty.Lib.Common;

namespace FindProperty.Lib.Ioc.Unity
{
    public class UnityComponent : DisposableBase, IIoCComponent
    {
        #region Disposable
        
        [DebuggerStepThrough]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _container.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion

        private readonly IUnityContainer _container;

        public IUnityContainer Container
        {
            get { return _container; }
        }

        [DebuggerStepThrough]
        public UnityComponent()
        {
            try
            {
                _container = new UnityContext().Kernel<IUnityContainer>();
            }
            catch(Exception ex)
            {
                throw ex;                
            }
        }
                
        public void Register<T>(T instance)
        {

            _container.RegisterInstance(instance);
        }

        public void Inject<T>(T existing)
        {

            _container.BuildUp(existing);
        }

        public T Resolve<T>(Type type)
        {

            return (T)_container.Resolve(type);
        }

        public T Resolve<T>(Type type, string name)
        {

            return (T)_container.Resolve(type, name);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {

            return _container.Resolve<T>(name);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            IEnumerable<T> namedInstances = _container.ResolveAll<T>();
            T unnamedInstance = default(T);

            try
            {
                unnamedInstance = _container.Resolve<T>();
            }
            catch (ResolutionFailedException rfEx)
            {
                throw rfEx;
            }

            if (Equals(unnamedInstance, default(T)))
            {
                return namedInstances;
            }

            return new ReadOnlyCollection<T>(new List<T>(namedInstances) { unnamedInstance });
        }

        public IIoCContext Context
        {
            get { return new UnityContext(); }
        }
    }
}
