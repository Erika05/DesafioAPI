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
    public class AtualizaTarefasTests : TestBase
    {
        CadastraProjetoRequests cadastroProjetoRequests = new CadastraProjetoRequests();
        CadastraTarefaRequest cadastraTarefaRequest = new CadastraTarefaRequest();
        AtualizaTarefaRequest atualizaTarefaRequest = new AtualizaTarefaRequest();

       [Test]
        public void AtualizarTarefaMinimal()
        {
            #region Parameters Cadastro Tarefa
            string resumo = "Nova Tarefa Minimal";
            string descricao = "Descricao nova tarefa minimal";
            string categoria = "General";
            string projeto = "projeto geral";
            #endregion
            #region Parameters Atualizar Tarefa
            string statusTarefa = "resolved";
            string statusCodeEsperado = "OK";
            #endregion
            VerificaProjetoExiste(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
         
            atualizaTarefaRequest.SetParameters(idTarefa);
            atualizaTarefaRequest.SetJsonBody(statusTarefa);
            IRestResponse<dynamic> response = atualizaTarefaRequest.ExecuteRequest();

            string retornoStatusTarefa = response.Data["issues"][0]["status"]["name"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(statusTarefa, retornoStatusTarefa);
            });
        }

        [Test]
        public void AtualizarTarefa()
        {
            //Criar tag 
            #region Parameters Cadastro Tarefa
            string resumo = "Nova Tarefa";
            string descricao = "Descricao nova tarefa";
            string informacao = "informacao";
            string projeto = "projeto geral";
            string categoria = "General";
            string visibilidade = "private";
            string prioridade = "high";
            string severidade = "trivial";
            string reprodutibilidade = "sometimes";
            //  string tag = "tag tarefa";
            string tag = "e";
            #endregion
            #region Atualizar Tarefa
            string atualizacaoResumo = "Tarefa completa atualizada";
            string atualizaoPrioridade = "high";
            string atualizacaoStatusTarefa = "resolved";
            string statusCodeEsperado = "OK";
            #endregion
            VerificaProjetoExiste(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, informacao, projeto, categoria, visibilidade, prioridade, severidade, reprodutibilidade, tag);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            atualizaTarefaRequest.SetParameters(idTarefa);
            atualizaTarefaRequest.SetJsonBody(atualizacaoResumo, atualizaoPrioridade, atualizacaoStatusTarefa);
            IRestResponse<dynamic> response = atualizaTarefaRequest.ExecuteRequest();

            string retornoResumoTarefa = response.Data["issues"][0]["summary"];
            string retornoPrioridade = response.Data["issues"][0]["priority"]["name"];
            string retornoStatusTarefa = response.Data["issues"][0]["status"]["name"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(atualizacaoResumo, retornoResumoTarefa);
                Assert.AreEqual(atualizaoPrioridade, retornoPrioridade);
                Assert.AreEqual(atualizacaoStatusTarefa, retornoStatusTarefa);
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