using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefa;
using DesafioAPI.Requests.Tarefas;
using NUnit.Framework;
using RestSharp;
using System.Threading;

namespace DesafioAPI.Tests.Tarefas
{
    [TestFixture]
    public class DeletaTarefasTests : TestBase
    {
        CadastraProjetoRequests cadastroProjetoRequests = new CadastraProjetoRequests();
        CadastroTarefaRequest cadastroTarefaRequest = new CadastroTarefaRequest();
        DeletaTarefaRequest deletaTarefaRequest = new DeletaTarefaRequest();

       [Test]
        public void DeletarTarefa()
        {
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa Delete";
            string descricao = "Descricao tarefa";
            string categoria = "General";
            string projeto = "projeto geral";
            string statusCodeEsperado = "NoContent";
            #endregion
            VerificaProjetoExiste(projeto);
            cadastroTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastroTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            deletaTarefaRequest.SetParameters(idTarefa);
            IRestResponse<dynamic> response = deletaTarefaRequest.ExecuteRequest();
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());                

            });
        }        

        public void VerificaProjetoExiste(string nomeProjeto)
        {
            if (ProjetoDBSteps.VerificaProjetoExiste(nomeProjeto).Equals(0))
            {
                cadastroProjetoRequests.SetJsonBody(nomeProjeto, "");
                cadastroProjetoRequests.ExecuteRequest();
            }
        }
    }
}