using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BLL;

namespace Security
{
    public class RBACManager
    {
        BLLPermiso bllperm = new BLLPermiso();
        BLLPerfil bllperf = new BLLPerfil();

        public IList<BEPermiso> ListarTodoPermisosExcepto(IList<RBAC> permisos)
        {
            return bllperm.ListarTodoExcepto(permisos);
        }

        public bool GuardarPerfil(BEUsuario autor, BEPerfil perfil)
        {
            return bllperf.Guardar(autor, perfil);
        }
        public bool Baja(BEUsuario autor, BEPerfil perfil)
        {
            return bllperf.Baja(autor, perfil);
        }
        public IList<BEPerfil> ListarRoles()
        {
            return bllperf.ListarNombresRoles();
        }

        public BEPerfil ListarRol(int Codigo)
        {
            return bllperf.ListarPerfil(Codigo);
        }
        public BEPerfil ListarRol(string nombre)
        {
            return bllperf.ListarPerfil(nombre);
        }
        public BEPerfil ListarRolFull(BEPerfil perfil)
        {
            return bllperf.ListarRol2(perfil);
        }
        public List<BEPermiso> Conjugar(BEPerfil perfil)
        {
            return bllperf.ConjugarRol(perfil);
        }
        public List<BEPermiso> ListarPermisos()
        {
            return bllperm.ListarTodo();
        }
        public bool RolContieneRol(BEPerfil padre, BEPerfil hijo)
        {
            return bllperf.RolContieneRol(padre, hijo);
        }

    }
}
