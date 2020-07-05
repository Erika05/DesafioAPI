using DesafioAPI.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Requests.Usuario
{
    public class DeletaUsuarioRequest : RequestBase
    {
        public DeletaUsuarioRequest(string idUsuario)
        {
            requestService = $"/api/rest/users/"+idUsuario;
            method = Method.DELETE;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }
    }
}
