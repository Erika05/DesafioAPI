using DesafioAPI.Helpers;
using System;
using System.IO;
using System.Text;

namespace DesafioAPI.DBSteps
{
    public class ProjetoDBSteps
    {
        public static int VerificaProjetoExiste(string nomeProjeto)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projeto/VerificaProjetoExiste.sql", Encoding.UTF8).Replace("$nomeProjeto", nomeProjeto);

            return Int32.Parse(DBHelpers.RetornaDadosQuery(query)[0]);
        }

        public static string RetornaIDProjetoNome(string nomeProjeto)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projeto/VerificaIdProjetoNome.sql", Encoding.UTF8).Replace("$nomeProjeto", nomeProjeto);

            return DBHelpers.RetornaDadosQuery(query)[0];
        }

        public static string RetornaIDProjeto()
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projeto/VerificaIdProjetos.sql", Encoding.UTF8);

            return DBHelpers.RetornaDadosQuery(query)[0];
        }

        public static void DeletaProjetos(string nomeProjeto)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projeto/DeletaProjetos.sql", Encoding.UTF8).Replace("$nomeProjeto", nomeProjeto);

            DBHelpers.ExecuteQuery(query);
        }
    }
}
