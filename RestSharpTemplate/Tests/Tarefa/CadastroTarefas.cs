using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefas;
using NUnit.Framework;
using RestSharp;


namespace DesafioAPI.Tests.Tarefas
{
    [TestFixture]
    public class CadastroTarefas : TestBase
    {
        CadastroProjetoRequests cadastroProjetoRequests = new CadastroProjetoRequests();
        CadastroTarefaMinimalRequest cadastroTarefaMinimalRequest = new CadastroTarefaMinimalRequest();
        CadastroTarefaRequest cadastroTarefaRequest = new CadastroTarefaRequest();

        [Test]
        public void CadastrarTarefas_Minimal()
        {
            #region Parameters
            string resumo = "This is a test issue";
            string descricao = "This is a test description";
            string categoria = "General";
            string projeto = "projeto geral";
            string statusCodeEsperado = "Created";
            #endregion
            VerificaProjetoExiste(projeto);
            cadastroTarefaMinimalRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            IRestResponse<dynamic> response = cadastroTarefaMinimalRequest.ExecuteRequest();
            string retornoSummary = response.Data["issue"]["summary"];
            string retornoDescription = response.Data["issue"]["description"];
            string retornoProject = response.Data["issue"]["project"]["name"];
            string retornoCategory = response.Data["issue"]["category"]["name"];
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(resumo, retornoSummary);
                Assert.AreEqual(descricao, retornoDescription);
                Assert.AreEqual(projeto, retornoProject);
                Assert.AreEqual(categoria, retornoCategory);
            });
        }

        [Test]
        public void CadastrarTarefas()
        {
            //Criar tag 
            #region Parameters
            string resumo = "Tarefa resumo completa";
            string descricao = "Tarefa resumo completa";
            string informacao = "informacao";
            string projeto = "projeto geral";
            string categoria = "General";
            string visibilidade = "private";
            string prioridade = "high";
            string severidade = "small";
            string reprodutibilidade = "sometimes";
            string nomeTarefa = "nome tarefa";
          //  string tag = "tag tarefa";
             string tag = "e";
            string statusCodeEsperado = "Created";
            #endregion
            cadastroTarefaRequest.SetJsonBody(resumo, descricao, informacao, projeto, categoria, visibilidade, prioridade, severidade, reprodutibilidade, nomeTarefa,tag);
            IRestResponse<dynamic> response = cadastroTarefaRequest.ExecuteRequest();

            Assert.Multiple(() =>
           {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
               // Assert.AreEqual(projeto, response.Data["summary"]);               
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
