using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUPER_TRUNFO
{
    public class CartaMarvel
    {
        public string Nome { get; set; }
        public int Forca { get; set; }
        public int Inteligencia { get; set; }
        public int Durabilidade { get; set; }
        public int HabilidadesEspeciais { get; set; }
        public bool SuperTrunfo { get; set; }
        public Jogador Jogador { get; set; } // Referência ao jogador que jogou a carta
        public string Sufixo {get; set;} // Novo atributo para o sufixo da carta

        public CartaMarvel(string nome, int forca, int inteligencia, int durabilidade, int habilidadesEspeciais, bool superTrunfo, string sufixo)
        {
            Nome = nome;
            Forca = forca;
            Inteligencia = inteligencia;
            Durabilidade = durabilidade;
            HabilidadesEspeciais = habilidadesEspeciais;
            SuperTrunfo = superTrunfo;
            Sufixo = sufixo;
        }

        public int ObterValorAtributo(string atributo)
        {
            switch (atributo)
            {
                case "Forca":
                    return Forca;
                case "Inteligencia":
                    return Inteligencia;
                case "Durabilidade":
                    return Durabilidade;
                // adicione casos para outros atributos, se necessário
                default:
                    throw new ArgumentException("Atributo inválido");
            }
        }

        public bool EhCartaComSufixoA()
        {
            // Verifica se alguma propriedade possui sufixo "A"
            if (Nome.EndsWith("A") || Forca.ToString().EndsWith("A") || Inteligencia.ToString().EndsWith("A") || Durabilidade.ToString().EndsWith("A"))
            {
                return true;
            }
            return false;
        }

        public bool EhSuperTrunfo()
        {
            return SuperTrunfo;
        }
    }
}