using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aquaivy.Core.Common
{
    /// <summary>
    /// Base class for Singleton pattern.
    /// </summary>
    public class Singleton<T> where T : class
    {
        private static volatile T _instance;
        private static object _syncRoot = new Object();

        protected Singleton() { }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            Type t = typeof(T);
                            ConstructorInfo[] constructor = t.GetConstructors();
                            if (constructor.Length > 0)
                            {
                                throw new InvalidOperationException(String.Format("{0} has accesible constructor, can't enforce singleton behaviour", t.Name));
                            }

                            _instance = (T)Activator.CreateInstance(t, true);
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
