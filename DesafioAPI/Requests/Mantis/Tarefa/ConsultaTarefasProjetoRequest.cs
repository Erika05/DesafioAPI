using DesafioAPI.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Requests.Tarefas
{
    public class ConsultaTarefasProjetoRequest : RequestBase
    {
        public ConsultaTarefasProjetoRequest()
        {
            requestService = "/api/rest/issues/{project_id}";
            method = Method.GET;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }

        public void SetParameters(string idProjeto)
        {
            parameters.Clear();
            parameters.Add("project_id", idProjeto);
        }
    }
}
