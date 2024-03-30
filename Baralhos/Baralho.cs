namespace SUPER_TRUNFO
{
    public class Baralho
    {
        private List<CartaMarvel> cartas;
        private int indiceProximaCarta; // Declaração da variável indiceProximaCarta

        public Baralho()
        {
            cartas = new List<CartaMarvel>();
            AdicionarCartas();
            indiceProximaCarta = 0; // Inicialização da variável indiceProximaCarta
        }

        public int NumCartas => cartas.Count; // Propriedade para retornar o número de cartas no baralho

        // Retorna a próxima carta do baralho
        public CartaMarvel ProximaCarta()
        {
            if (indiceProximaCarta >= cartas.Count)
            {
                throw new InvalidOperationException("Não há mais cartas no baralho.");
            }

            CartaMarvel proximaCarta = cartas[indiceProximaCarta];
            indiceProximaCarta++; // Atualiza o índice da próxima carta
            return proximaCarta;
        }

        public void Embaralhar()
        {
            // Lógica para embaralhar o baralho
        }

        public void DistribuirCartas(List<Jogador> jogadores)
        {
            Embaralhar(); // Embaralha as cartas antes de distribuir
            int numCartasPorJogador = NumCartas / jogadores.Count;
            for (int i = 0; i < jogadores.Count; i++)
            {
                for (int j = 0; j < numCartasPorJogador; j++)
                {
                    jogadores[i].ReceberCarta(ProximaCarta());
                }
            }
        }

        public void AdicionarCartas()
        {
            // Grupo 1
            cartas.Add(new CartaMarvel("Thanos", 10, 8, 9, 100,false, "A"));
            cartas.Add(new CartaMarvel("Magneto", 7, 9, 6, 70,false, "B"));
            cartas.Add(new CartaMarvel("Loki", 5, 9, 6, 30,false, "C"));
            cartas.Add(new CartaMarvel("Galactus", 10, 10, 10, 85,false,"D"));

            // Grupo 2
            cartas.Add(new CartaMarvel("Hulk", 10, 6, 7, 200,true, "A"));
            cartas.Add(new CartaMarvel("Thor", 8, 7, 8, 90,false, "B"));
            cartas.Add(new CartaMarvel("Homem de Ferro", 7, 10, 6, 79,false, "C"));
            cartas.Add(new CartaMarvel("Capitão América", 8, 7, 9, 92,false, "D"));

            // Grupo 3
            cartas.Add(new CartaMarvel("Wolverine", 8, 6, 8, 76,false, "A"));
            cartas.Add(new CartaMarvel("Viuva Negra", 7, 9, 6, 59,false, "B"));
            cartas.Add(new CartaMarvel("Gavião Arqueiro", 6, 7, 8, 55,false, "C"));
            cartas.Add(new CartaMarvel("Pantera Negra", 8, 8, 8, 78,false, "D"));

            // Grupo 4
            cartas.Add(new CartaMarvel("Doutor Estranho", 7, 9, 7, 80,false, "A"));
            cartas.Add(new CartaMarvel("Feiticeira Escarlate", 7, 9, 7, 67,false, "B"));
            cartas.Add(new CartaMarvel("Homem-Aranha", 6, 7, 6, 85,false, "C"));
            cartas.Add(new CartaMarvel("Capitã Marvel", 9, 9, 9, 88,false, "D"));

            // Grupo 5
            cartas.Add(new CartaMarvel("Nick Fury", 7, 8, 7, 50,false, "A"));
            cartas.Add(new CartaMarvel("Falcão", 6, 6, 7, 76,false, "B"));
            cartas.Add(new CartaMarvel("Máquina de Combate", 8, 7, 8, 80,false, "C"));
            cartas.Add(new CartaMarvel("Visão", 9, 10, 8, 89,false, "D"));

            // Grupo 6
            cartas.Add(new CartaMarvel("Senhor das Estrelas", 7, 7, 7, 98,false, "A"));
            cartas.Add(new CartaMarvel("Gamora", 8, 8, 7, 77,false, "B"));
            cartas.Add(new CartaMarvel("Drax", 9, 6, 8, 70,false, "C"));
            cartas.Add(new CartaMarvel("Rocket", 6, 9, 6, 68,false, "D"));

            // Grupo 7
            cartas.Add(new CartaMarvel("Vampira", 8, 7, 8, 57,false, "A"));
            cartas.Add(new CartaMarvel("Gambit", 7, 8, 7, 56,false, "B"));
            cartas.Add(new CartaMarvel("Tempestade", 8, 9, 7, 69,false, "C"));
            cartas.Add(new CartaMarvel("Noturno", 7, 7, 8, 87,false, "D"));

            // Grupo 8
            cartas.Add(new CartaMarvel("Homem de Gelo", 7, 7, 7, 67,false, "A"));
            cartas.Add(new CartaMarvel("Ciclope", 7, 7, 7, 89,false, "B"));
            cartas.Add(new CartaMarvel("Jean Grey", 8, 9, 7, 99,false, "C"));
            cartas.Add(new CartaMarvel("Professor Xavier", 6, 10, 6, 115,false, "D"));

        }

        static Jogador DeterminarVencedorRodada(List<CartaMarvel> cartasJogadas)
        {
            // Verifica se há a carta Super Trunfo entre as cartas jogadas
            foreach (CartaMarvel carta in cartasJogadas)
            {
                if (carta.SuperTrunfo)
                {
                    return carta.Jogador; // A carta Super Trunfo vence automaticamente
                }
            }

            // Verifica se há cartas com sufixo "A" entre as cartas jogadas
            foreach (CartaMarvel carta in cartasJogadas)
            {
                if (carta.Nome.EndsWith("A"))
                {
                    // A carta com sufixo "A" vence todas as outras cartas, exceto a Super Trunfo
                    return carta.Jogador;
                }
            }

            // Se não houver carta Super Trunfo nem cartas com sufixo "A", determinar vencedor com base nas características
            CartaMarvel vencedora = cartasJogadas[0]; // Suponha que a primeira carta seja a vencedora inicialmente
            foreach (CartaMarvel carta in cartasJogadas)
            {
                // Implemente a lógica para comparar as características das cartas e determinar a vencedora
                // Compare Força, Inteligência, Durabilidade, etc.
                // Se uma carta tiver uma característica maior do que as outras, ela se torna a carta vencedora
            }

            return vencedora.Jogador; // Retorna o jogador associado à carta vencedora
        }
    }
}