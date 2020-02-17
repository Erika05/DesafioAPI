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
    public class AdicionaTagCopiaTarefasTests : TestBase
    {
        HelpersProjetos helpersProjetos = new HelpersProjetos(); 
        CadastraTarefaRequest cadastraTarefaRequest = new CadastraTarefaRequest();
        AdicionaTagCopiaTarefaRequest adicionaTagCopiaTarefaRequest = new AdicionaTagCopiaTarefaRequest();

        [Test]
        public void AdicionarTagCopiaTarefa()
        {
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa tag copia tarefas";
            string descricao = "Descricao tag tarefa";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            string statusCodeEsperado = "Created";
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            string idTarefaRelacionada = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];

            adicionaTagCopiaTarefaRequest.SetParameters(idTarefa);
            adicionaTagCopiaTarefaRequest.SetJsonBody(idTarefaRelacionada);
            IRestResponse<dynamic> response = adicionaTagCopiaTarefaRequest.ExecuteRequest();

           string retornoIdTarefaRelacionada = response.Data["issue"]["relationships"][0]["issue"]["id"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(idTarefaRelacionada, retornoIdTarefaRelacionada);
            });
        } 
    }
}