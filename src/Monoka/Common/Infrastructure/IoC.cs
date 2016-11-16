using System;
using Autofac;
using Monoka.Common.Infrastructure.Logging;

namespace Monoka.Common.Infrastructure
{
    public class IoC : IIoC
    {
        private static IContainer _container;

        public static IContainer AutofacContainer => _container;
        public static IIoC Instance => _container.Resolve<IIoC>();
        
        public void RegisterContainer(IContainer container) => _container = container;
        
        public T Resolve<T>()
        {
            Log.Msg(this, l => l.Debug($"Attempting to resolve {typeof(T).FullName}"));

            try
            {
                return _container.Resolve<T>();
            }
            catch (Exception ex)
            {
                Log.Msg(this, l => l.Error($"Exception while attempting to resolve {typeof(T).FullName}", ex));
                throw;
            }
        }

        public object Resolve(Type type)
        {
            try
            {
                Log.Msg(this, l => l.Debug($"Attempting to resolve {type.FullName}"));

                return _container.Resolve(type);
            }
            catch (Exception ex)
            {
                Log.Msg(this, l => l.Error($"Exception while attempting to resolve {type.FullName}", ex));
                throw;
            }
        }

        public T Resolve<T>(string name)
        {
            try
            {
                Log.Msg(this, l => l.Debug($"Attempting to resolve {typeof(T).FullName} named {name}"));

                return _container.ResolveNamed<T>(name);
            }
            catch (Exception ex)
            {
                Log.Msg(this, l => l.Error($"Exception while attempting to resolve {typeof(T).FullName} named {name}", ex));
                throw;
            }
        }
    }
}
