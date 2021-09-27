using System;
using System.Collections.Generic;

namespace Voice.Picking.Test.Pages.Extensions
{
    public class Converter
    {
        internal static string AddressToText(string address, string addressOld)
        {
            var aux = address.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            var auxOld = new string[0];
            if (!string.IsNullOrEmpty(addressOld))
            {
                auxOld = addressOld.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (auxOld.Length > 1 && aux.Length > 1 && address != addressOld)
            {
                if (aux[0] == auxOld[0])
                {
                    aux[1] = aux[1].Insert(0, "Prédio ");
                    aux[0] = "";
                }
                else
                {
                    aux[0] = aux[0].Insert(0, "Rua ");
                }
            }
            else
            {
                aux[0] = aux[0].Insert(0, "Rua ");
            }
            string ret = string.Empty;
            foreach (var item in aux)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var number = Extensions.NumberInString(item);
                  
                    if (string.IsNullOrEmpty(number))
                    {
                        ret += $" {item} ,";
                    }
                    else
                    {
                        string ex = EscreverExtenso(Convert.ToInt32(number));
                        ret += $" {item.Replace(number.ToString(), "")} {ex},";
                    }
                }
            }

            return ret;
        }


        private static string EscreverExtenso(decimal valor)
        {
            if (valor < 0)
            {
                return string.Empty;
            }
            else if (valor == 0)
            {
                return "ZERO";
            }
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));
                if (a == 1) montagem += (b + c == 0) ? "CEM" : "CENTO";
                else if (a == 2) montagem += "DUZENTOS";
                else if (a == 3) montagem += "TREZENTOS";
                else if (a == 4) montagem += "QUATROCENTOS";
                else if (a == 5) montagem += "QUINHENTOS";
                else if (a == 6) montagem += "SEISCENTOS";
                else if (a == 7) montagem += "SETECENTOS";
                else if (a == 8) montagem += "OITOCENTOS";
                else if (a == 9) montagem += "NOVECENTOS";
                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " E " : string.Empty) + "DEZ";
                    else if (c == 1) montagem += ((a > 0) ? " E " : string.Empty) + "ONZE";
                    else if (c == 2) montagem += ((a > 0) ? " E " : string.Empty) + "DOZE";
                    else if (c == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TREZE";
                    else if (c == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUATORZE";
                    else if (c == 5) montagem += ((a > 0) ? " E " : string.Empty) + "QUINZE";
                    else if (c == 6) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSEIS";
                    else if (c == 7) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSETE";
                    else if (c == 8) montagem += ((a > 0) ? " E " : string.Empty) + "DEZOITO";
                    else if (c == 9) montagem += ((a > 0) ? " E " : string.Empty) + "DEZENOVE";
                }
                else if (b == 2) montagem += ((a > 0) ? " E " : string.Empty) + "VINTE";
                else if (b == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TRINTA";
                else if (b == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUARENTA";
                else if (b == 5) montagem += ((a > 0) ? " E " : string.Empty) + "CINQUENTA";
                else if (b == 6) montagem += ((a > 0) ? " E " : string.Empty) + "SESSENTA";
                else if (b == 7) montagem += ((a > 0) ? " E " : string.Empty) + "SETENTA";
                else if (b == 8) montagem += ((a > 0) ? " E " : string.Empty) + "OITENTA";
                else if (b == 9) montagem += ((a > 0) ? " E " : string.Empty) + "NOVENTA";
                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " E ";
                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "UM";
                    else if (c == 2) montagem += "DOIS";
                    else if (c == 3) montagem += "TRÊS";
                    else if (c == 4) montagem += "QUATRO";
                    else if (c == 5) montagem += "CINCO";
                    else if (c == 6) montagem += "SEIS";
                    else if (c == 7) montagem += "SETE";
                    else if (c == 8) montagem += "OITO";
                    else if (c == 9) montagem += "NOVE";
                return montagem;
            }
        }
    }
}
