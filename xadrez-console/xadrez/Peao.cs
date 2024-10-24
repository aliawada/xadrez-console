using tabuleiro;

namespace xadrez
{

    class Peao : Peca
    {

        private PartidaDeXadrez partida;

        public Peao(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "♙";
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = Tabuleiro.peca(pos);
            return p != null && p.Cor != Cor;
        }

        private bool livre(Posicao pos)
        {
            return Tabuleiro.peca(pos) == null;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];

            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                pos.definirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Tabuleiro.posicaoValida(pos) && livre(pos) && QtdMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // #jogadaespecial en passant
                if (Posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.posicaoValida(esquerda) && existeInimigo(esquerda) && Tabuleiro.peca(esquerda) == partida.vulneravelEnPassant)
                    {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.posicaoValida(direita) && existeInimigo(direita) && Tabuleiro.peca(direita) == partida.vulneravelEnPassant)
                    {
                        mat[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }
            else
            {
                pos.definirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tabuleiro.posicaoValida(pos) && livre(pos) && QtdMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // #jogadaespecial en passant
                if (Posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.posicaoValida(esquerda) && existeInimigo(esquerda) && Tabuleiro.peca(esquerda) == partida.vulneravelEnPassant)
                    {
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.posicaoValida(direita) && existeInimigo(direita) && Tabuleiro.peca(direita) == partida.vulneravelEnPassant)
                    {
                        mat[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return mat;
        }
    }
}