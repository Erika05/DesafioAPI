using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefas;
using NUnit.Framework;
using RestSharp;

namespace DesafioAPI.Tests.Tarefas
{
    [TestFixture]
    public class AdicionaMonitoramentoTarefasTests : TestBase
    {
        CadastraProjetoRequests cadastroProjetoRequests = new CadastraProjetoRequests();
        CadastroTarefaRequest cadastroTarefaRequest = new CadastroTarefaRequest();
        AdicionaMonitoramentoTarefaRequest adicionaMonitoramentoTarefaRequest = new AdicionaMonitoramentoTarefaRequest();

        [Test]
        public void AdicionarMonitoramentoTarefa()
        {
            //Criar tag 
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa adicionar monitoramento nota";
            string descricao = "Descricao tarefa monitoramento nota";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion           
            string statusCodeEsperado = "Created";
            string idMonitoramento = "1";
            cadastroTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastroTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            adicionaMonitoramentoTarefaRequest.SetParameters(idTarefa);            
            IRestResponse<dynamic> response = adicionaMonitoramentoTarefaRequest.ExecuteRequest();

           string retornoIdMonitoramento = response.Data["issues"][0]["monitors"][0]["id"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(idMonitoramento, retornoIdMonitoramento);
            });
        }

        [Test]
        public void AdicionarMonitoramentoUsuarioTarefa()
        {
            //Criar tag 
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa adicionar nota com duracao";
            string descricao = "Descricao tarefa nota com duracao";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion
            string statusCodeEsperado = "Created";
            string userName = "administrator";
            string userRealName = "administrator";
            string idMonitoramento = "1";
            cadastroTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastroTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            adicionaMonitoramentoTarefaRequest.SetParameters(idTarefa);
            adicionaMonitoramentoTarefaRequest.SetJsonBody(userName, userRealName);
            IRestResponse<dynamic> response = adicionaMonitoramentoTarefaRequest.ExecuteRequest();

            string retornoIdMonitoramento = response.Data["issues"][0]["monitors"][0]["id"];
            string retornoUserMonitoramento = response.Data["issues"][0]["monitors"][0]["name"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(idMonitoramento, retornoIdMonitoramento);
                Assert.AreEqual(userRealName, retornoUserMonitoramento);
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