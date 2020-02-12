using DesafioAPI.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DesafioAPI.DBSteps
{
    public class UsuarioDBSteps
    {   
        public static List<string> VerificaUsuarioExiste(string nomeUsuario)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Usuario/VerificaUsuarioExiste.sql", Encoding.UTF8).Replace("$nomeUsuario", nomeUsuario);

            return DBHelpers.RetornaDadosQuery(query);
        }

        public static void DeletaUsuario(string nomeUsuario)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Usuario/DeletaUsuario.sql", Encoding.UTF8).Replace("$nomeUsuario", nomeUsuario);

            DBHelpers.ExecuteQuery(query);
        }
    }
}
