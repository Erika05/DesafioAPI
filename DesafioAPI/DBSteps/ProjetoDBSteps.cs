using DesafioAPI.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DesafioAPI.DBSteps
{
    public class ProjetoDBSteps
    {
        public static List<string> VerificaDadosProjeto(string nomeProjeto)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projeto/VerificaDadosProjeto.sql", Encoding.UTF8).Replace("$nomeProjeto", nomeProjeto);
            return DBHelpers.RetornaDadosQuery(query);
        }

        public static int VerificaProjetoExiste(string nomeProjeto)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projeto/VerificaProjetoExiste.sql", Encoding.UTF8).Replace("$nomeProjeto", nomeProjeto);

            return Int32.Parse(DBHelpers.RetornaDadosQuery(query)[0]);
        }

        public static int VerificaProjetoPeloId(string id)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projeto/VerificaProjetoPeloId.sql", Encoding.UTF8).Replace("$id", id);

            return Int32.Parse(DBHelpers.RetornaDadosQuery(query)[0]);
        }

        //public static string RetornaIDProjetoNome(string nomeProjeto)
        //{
        //    string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projeto/VerificaIdProjetoNome.sql", Encoding.UTF8).Replace("$nomeProjeto", nomeProjeto);

        //    return DBHelpers.RetornaDadosQuery(query)[0];
        //}

        //public static string RetornaIDProjeto()
        //{
        //    string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projeto/VerificaIdProjetos.sql", Encoding.UTF8);

        //    return DBHelpers.RetornaDadosQuery(query)[0];
        //}

        public static void DeletaProjetos()
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projeto/DeletaProjetos.sql", Encoding.UTF8);

            DBHelpers.ExecuteQuery(query);
        }
    }
}
