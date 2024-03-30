using SUPER_TRUNFO;


namespace SuperTrunfo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao Super Trunfo!");

            // Criando um baralho e adicionando cartas
            Baralho baralho = new Baralho();
            AdicionarCartas(baralho);

            // Definindo os jogadores
            List<Jogador> jogadores = CriarJogadores(2); // Definindo 2 jogadores

            // Verificando se o número de jogadores está dentro do intervalo permitido
            if (jogadores.Count < 2 || jogadores.Count > 6)
            {
                Console.WriteLine("Número de jogadores inválido. A partida não pode ser iniciada.");
                return;
            }

            // Distribuindo cartas igualmente entre os jogadores
            //baralho.DistribuirCartas(jogadores);// Passando os dois primeiros jogadores da lista
            DistribuirCartas(baralho, jogadores);

            // Iniciando o jogo
            Jogador vencedor = JogarPartida(jogadores);

            // Exibindo o vencedor
            Console.WriteLine($"O vencedor é {vencedor.Nome} com {vencedor.Pontuacao} pontos!");

            Console.ReadLine();
        }
        static List<Jogador> CriarJogadores(int numeroJogadores)
        {

            Console.WriteLine("Digite o número de jogadores (entre 2 e 6):");

            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out numeroJogadores))
                {
                    Console.WriteLine("Número inválido. Tente novamente.");
                    continue;
                }

                if (numeroJogadores < 2 || numeroJogadores > 6)
                {
                    Console.WriteLine("Número de jogadores inválido. Tente novamente.");
                    continue;
                }

                break;
            }

            List<Jogador> jogadores = new List<Jogador>();
            for (int i = 1; i <= numeroJogadores; i++)
            {
                Console.WriteLine($"Nome do jogador {i}:");
                string nomeJogador = Console.ReadLine();
                jogadores.Add(new Jogador(nomeJogador));
            }

            return jogadores;
        }

        static void DistribuirCartas(Baralho baralho, List<Jogador> jogadores)
        {
            baralho.Embaralhar(); // Embaralha o baralho antes de distribuir
            List<int> indicesEmbaralhados = Enumerable.Range(0, baralho.NumCartas).OrderBy(x => Guid.NewGuid()).ToList(); // Cria uma lista de índices embaralhados

            int jogadorIndex = 0;
            foreach (var indice in indicesEmbaralhados)
            {
                var carta = baralho.ProximaCarta(); // Obtém a carta correspondente ao índice atual
                jogadores[jogadorIndex].ReceberCarta(carta); // Dá ao jogador atual a carta correspondente
                jogadorIndex = (jogadorIndex + 1) % jogadores.Count; // Avança para o próximo jogador, garantindo que o índice permaneça dentro dos limites da lista de jogadores
            }
        }

        static Jogador JogarPartida(List<Jogador> jogadores)
        {
            Console.WriteLine("O primeiro jogador deve escolher o atributo para comparar:");
            int atributoEscolhido = jogadores[0].EscolherAtributo();

            int rodada = 1;
            while (true)
            {
                Console.WriteLine($"Rodada {rodada}");

                // Obter as cartas dos jogadores para a rodada atual
                List<CartaMarvel> cartasJogadas = new List<CartaMarvel>();
                foreach (Jogador jogador in jogadores)
                {
                    CartaMarvel cartaJogada = jogador.JogarCarta();
                    if (cartaJogada == null)
                    {
                        return null; // Todos os jogadores estão sem cartas, empate
                    }
                    cartasJogadas.Add(cartaJogada);
                    Console.WriteLine($"{jogador.Nome} jogou a carta {cartaJogada.Nome}");

                    // Verificar se a carta jogada é um super trunfo
                    if (cartaJogada.SuperTrunfo)
                    {
                        Console.WriteLine($"O jogador {jogador.Nome} jogou um Super Trunfo! Ele vence automaticamente a partida!");
                        return jogador;
                    }
                }

                // Verificar se todas as outras cartas na rodada têm sufixo "A"
                bool todasComSufixoA = true;
                foreach (var carta in cartasJogadas)
                {
                    if (!carta.EhCartaComSufixoA())
                    {
                        todasComSufixoA = false;
                        break;
                    }
                }

                if (todasComSufixoA)
                {
                    // Se todas as outras cartas têm sufixo "A", o jogador atual vence
                    Console.WriteLine($"Todas as outras cartas na rodada têm sufixo 'A'. O jogador {jogadores[0].Nome} vence!");
                    return jogadores[0];
                }

                // Determinar o vencedor da rodada
                Jogador vencedorRodada = DeterminarVencedorRodada(cartasJogadas, atributoEscolhido, jogadores);
                if (vencedorRodada != null)
                {
                    // Calcular a pontuação total de cada jogador
                    foreach (Jogador jogador in jogadores)
                    {
                        jogador.AtualizarPontuacao(jogador.CalcularPontuacao(atributoEscolhido));

                    }
                    return vencedorRodada; // Temos um vencedor
                }

                rodada++;
            }
        }

        public static Jogador DeterminarVencedorRodada(List<CartaMarvel> cartasJogadas, int atributoEscolhido, List<Jogador> jogadores)
        {
            CartaMarvel cartaVencedora = null;
            Jogador jogadorVencedor = null;
            bool superTrunfoNaRodada = false;

            // Calcular a pontuação total de cada jogador na rodada
            foreach (Jogador jogador in jogadores)
            {
                jogador.AtualizarPontuacao(jogador.CalcularPontuacao(atributoEscolhido));
            }

            // Exibir a soma dos pontos de cada jogador no console
            foreach (Jogador jogador in jogadores)
            {
                Console.WriteLine($"Pontuação total de {jogador.Nome}: {jogador.Pontuacao}");
            }

            foreach (var carta in cartasJogadas)
            {
                if (carta.SuperTrunfo)
                {
                    superTrunfoNaRodada = true;
                    cartaVencedora = carta;
                    break;
                }
                // Verifica se a carta atual possui sufixo "A"
                if (carta.EhCartaComSufixoA())
                {
                    continue; // Cartas com sufixo "A" não são afetadas pelo SUPER TRUNFO
                }

                // Obter o nome do atributo correspondente ao número escolhido
                string nomeAtributo = ObterNomeAtributo(atributoEscolhido);

                if (cartaVencedora == null || carta.ObterValorAtributo(nomeAtributo)> cartaVencedora.ObterValorAtributo(nomeAtributo))
                {
                    cartaVencedora = carta;
                }
            }

            if (superTrunfoNaRodada)
            {
                foreach (var jogador in jogadores)
                {
                    if (jogador.PossuiCarta(cartaVencedora))
                    {
                        jogadorVencedor = jogador;
                        break;
                    }
                }
            }
            return jogadorVencedor;
        }
                
        private static string ObterNomeAtributo(int atributoEscolhido)
        {
            return atributoEscolhido switch
            {
                1 => "Forca",
                2 => "Inteligencia",
                3 => "Durabilidade",
                4 => "HabilidadesEspeciais",
                _ => throw new ArgumentException("Número de atributo inválido."),
            };
        }
        
        static void AdicionarCartas(Baralho baralho)
        {

            // Adicione as cartas ao baralho conforme as regras de negócio
            // Certifique-se de incluir a carta Super Trunfo e ajustar as propriedades conforme necessário
        }
    }
}