
namespace tabuleiro
{
    internal class Tabuleiro
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }
        private Peca[,] Peca;

        public Tabuleiro(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
            Peca = new Peca[Linha, Coluna];
        }

        public Peca peca(int linha, int coluna)
        {
            return Peca[linha, coluna];
        }

        public void colocarPeca(Peca p, Posicao posicao)
        {
            Peca[posicao.Linha, posicao.Coluna] = p;
            p.Posicao = posicao;
        }
    }
}
