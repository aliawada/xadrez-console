
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

        public Peca peca(Posicao pos)
        {
            return Peca[pos.Linha, pos.Coluna];
        }

        public void colocarPeca(Peca p, Posicao posicao)
        {
            if(existePeca(posicao))
            {
                throw new TabuleiroException("Ja existe uma peca nessa posicao");
            }
            Peca[posicao.Linha, posicao.Coluna] = p;
            p.Posicao = posicao;
        }

        public Peca retirarPeca(Posicao posicao)
        {
            if(peca(posicao) == null)
            {
                return null;
            }
            Peca aux = peca(posicao);
            aux.Posicao = null;
            Peca[posicao.Linha, posicao.Coluna] = null;
            return aux;
        }

        public bool existePeca(Posicao pos)
        {
            validarPosicao(pos);
            return peca(pos) != null;
        }

        public bool posicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linha || pos.Coluna < 0 || pos.Coluna >= Coluna)
            {
                return false;
            }
            return true;
        }

        public void validarPosicao(Posicao pos)
        {
            if(!posicaoValida(pos))
            {
                throw new TabuleiroException("Posicao invalida");
            }
        }
    }
}
