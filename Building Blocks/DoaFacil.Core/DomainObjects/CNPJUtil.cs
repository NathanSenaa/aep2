using System.Text.RegularExpressions;

namespace DoaFacil.Core.DomainObjects
{
    public static class CNPJUtil
    {
        public static bool ValidarCNPJ(string cnpj)
        {
            // Remove caracteres não numéricos
            cnpj = Regex.Replace(cnpj, "[^0-9]", "");

            // Verifica se tem 14 dígitos
            if (cnpj.Length != 14)
            {
                return false;
            }

            // Calcula o primeiro dígito verificador
            int soma = 0;
            int[] multiplicadoresDigito1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(cnpj[i].ToString()) * multiplicadoresDigito1[i];
            }
            int digito1 = soma % 11 < 2 ? 0 : 11 - soma % 11;

            // Calcula o segundo dígito verificador
            soma = 0;
            int[] multiplicadoresDigito2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(cnpj[i].ToString()) * multiplicadoresDigito2[i];
            }
            int digito2 = soma % 11 < 2 ? 0 : 11 - soma % 11;

            // Verifica se os dígitos verificadores calculados são iguais aos fornecidos
            var valido = int.Parse(cnpj[12].ToString()) == digito1 && int.Parse(cnpj[13].ToString()) == digito2;

            return valido;
        }
    }
}
