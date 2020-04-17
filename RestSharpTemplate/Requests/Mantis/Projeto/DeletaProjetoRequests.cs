using DesafioAPI.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Requests.Projeto
{
    public class DeletaProjetoRequests : RequestBase
    {
        public DeletaProjetoRequests()
        {
            requestService = "/api/rest/projects/{project_id}";
            method = Method.DELETE;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }

        public void SetParameters(string idProjeto)
        {
            parameters.Add("project_id", idProjeto);
        }
    }
}
