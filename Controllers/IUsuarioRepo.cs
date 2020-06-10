using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Controllers
{
    public interface IUsuarioRepo
    {
        void Add(Usuario usuario);
        void Update(Usuario usuario);
        void Remove(Usuario usuario);
        Usuario GetUser(int id);
    }
}
