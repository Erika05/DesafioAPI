using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Tests.Projeto
{
    public class HelpersProjetos : TestBase
    {
        CadastraProjetoRequests cadastraProjetoRequests = new CadastraProjetoRequests();
        DeletaProjetoRequests deleteProjetoRequests = new DeletaProjetoRequests();

        public string PreparaBaseCadastradoProjeto(string nomeProjeto)
        {
            string idProjeto = "";
            List<string> dadosProjeto = (ProjetoDBSteps.VerificaDadosProjeto(nomeProjeto));

            if (dadosProjeto.Count.Equals(0))
            {
                cadastraProjetoRequests.SetJsonBody(nomeProjeto, "");
                idProjeto = cadastraProjetoRequests.ExecuteRequest().Data["project"]["id"];
            }
            else
            {

                idProjeto = dadosProjeto[0];
            }

            return idProjeto;
        }
        public void PreparaBaseDeletadoProjeto(string nomeProjeto)
        {
            List<string> dadosProjeto = (ProjetoDBSteps.VerificaDadosProjeto(nomeProjeto));

            if (!dadosProjeto.Count.Equals(0))
            {
                deleteProjetoRequests.SetParameters(dadosProjeto[0]);
                deleteProjetoRequests.ExecuteRequest();
            }
        }
    }
}