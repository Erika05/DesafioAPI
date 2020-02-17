using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefas;
using DesafioAPI.Tests.Projeto;
using NUnit.Framework;
using RestSharp;

namespace DesafioAPI.Tests.Tarefas
{
    [TestFixture]
    public class AdicionaMonitoramentoTarefasTests : TestBase
    {
        CadastraTarefaRequest cadastraTarefaRequest = new CadastraTarefaRequest();
        AdicionaMonitoramentoTarefaRequest adicionaMonitoramentoTarefaRequest = new AdicionaMonitoramentoTarefaRequest();
        HelpersProjetos helpersProjetos = new HelpersProjetos();

        [Test]
        public void AdicionarMonitoramentoTarefa()
        {
            //Criar tag 
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa adicionar monitoramento tarefa";
            string descricao = "Descricao tarefa monitoramento";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            string statusCodeEsperado = "Created";
            string idMonitoramento = "1";
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
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
            string resumo = "Tarefa adicionar monitoramento usuário tarefa";
            string descricao = "Descricao tarefa monitoramente usuário tarefa";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            string statusCodeEsperado = "Created";
            string userName = "administrator";
            string userRealName = "administrator";
            string idMonitoramento = "1";
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
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
    }
}