using NHibernate;
using NHibernate.Cfg;

namespace Sample.Controllers
{
    internal class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        public static ISessionFactory SessionFactory{
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddAssembly(typeof(Usuario).Assembly);
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}