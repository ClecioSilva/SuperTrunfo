using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUPER_TRUNFO
{
    public class Jogador
    {

        public string Nome { get; private set; }
        public int Pontuacao { get; private set; } // Adicionando pontuação ao jogador
        private int pontosRodada;
        private List<CartaMarvel> mao;
        private List<CartaMarvel> cartas;

        public Jogador(string nome)
        {
            // Inicialize os pontos da rodada como zero ao criar um novo jogador
            pontosRodada = 0;

            cartas = new List<CartaMarvel>();
            Nome = nome;
            Pontuacao = 0; // Inicializa a pontuação do jogador como 0
            mao = new List<CartaMarvel>(); // Inicializa a lista de cartas
            
        }

        // Implemente outras propriedades e métodos necessários para o jogador aqui

        public bool TemCartas()
        {
            // Lógica para verificar se o jogador ainda tem cartas
            return true; // Exemplo simples, sempre retorna verdadeiro
        }

        public CartaMarvel JogarCarta()
        {
            if (!TemCartas())
            {
                throw new InvalidOperationException("O jogador não tem cartas para jogar.");
            }

            // Aqui você pode implementar a lógica para permitir ao jogador escolher uma carta para jogar
            // Por exemplo, você pode exibir as cartas na mão do jogador e permitir que ele selecione uma delas
            // Neste exemplo, simplesmente removemos a primeira carta da lista de cartas do jogador
            CartaMarvel cartaJogada = mao[0];
            mao.RemoveAt(0); // Remove a carta jogada da mão do jogador
            return cartaJogada;
        }

        public void ReceberCarta(CartaMarvel carta)
        {
            mao.Add(carta); // Adiciona a carta à mão do jogador
        }

        // Método para adicionar pontos ao jogador na rodada atual
        public void AdicionarPontosRodada(int pontos)
        {
            pontosRodada += pontos;
        }

        // Método para obter os pontos do jogador na rodada atual
        public int ObterPontosRodada()
        {
            return pontosRodada;
        }

        // Método para reiniciar os pontos da rodada do jogador
        public void ReiniciarPontosRodada()
        {
            pontosRodada = 0;
        }

        public void AtualizarPontuacao(int pontos)
        {
            Pontuacao += pontos; // Atualiza a pontuação do jogador
        }

        public int EscolherAtributo()
        {
            Console.WriteLine("Escolha o atributo para comparar:");
            Console.WriteLine("1. Força");
            Console.WriteLine("2. Inteligência");
            Console.WriteLine("3. Durabilidade");
            Console.WriteLine("4. HabilidadesEspeciais");
            int escolha;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out escolha) || escolha < 1 || escolha > 4)
                {
                    Console.WriteLine("Escolha inválida. Tente novamente.");
                    continue;
                }
                break;
            }
            return escolha;
        }

        public bool PossuiCarta(CartaMarvel carta)
        {
            return mao.Contains(carta);
        }

        public int CalcularPontuacao(int atributoEscolhido)
        {
            if (mao.Count == 0)
            {
                return 0; // O jogador não tem cartas para calcular pontuação
            }

            // Obter o valor do atributo escolhido da primeira carta na mão do jogador
            CartaMarvel cartaJogada = mao[0];
            int valorAtributo = 0;
            switch (atributoEscolhido)
            {
                case 1:
                    valorAtributo = cartaJogada.Forca;
                    break;
                case 2:
                    valorAtributo = cartaJogada.Inteligencia;
                    break;
                case 3:
                    valorAtributo = cartaJogada.Durabilidade;
                    break;
                case 4:
                    // Se o atributo escolhido for "HabilidadesEspeciais", você precisa calcular um valor com base na string de habilidades
                    // Aqui estou apenas retornando o comprimento da string como exemplo
                    valorAtributo = cartaJogada.HabilidadesEspeciais;
                    break;
                default:
                    throw new ArgumentException("Número de atributo inválido.");
            }

            return valorAtributo;
        }

        public void RemoverCarta(CartaMarvel carta)
        {
            cartas.Remove(carta);
        }
    }
}