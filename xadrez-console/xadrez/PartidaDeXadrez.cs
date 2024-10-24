using System;
using tabuleiro;

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            Terminada = false;
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tabuleiro.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = Tabuleiro.retirarPeca(destino);
            Tabuleiro.colocarPeca(p, destino);
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if(Tabuleiro.peca(pos) == null)
            {
                throw new TabuleiroException("Nao existe peca na posicao de origem escolhida");
            }

            if(jogadorAtual != Tabuleiro.peca(pos).Cor)
            {
                throw new TabuleiroException("A peca de origem escolhida nao e sua");
            }

            if(!Tabuleiro.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Nao ha movimentos possiveis para a peca de origem escolhida");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if(!Tabuleiro.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posicao de destino invalida");
            }
        }

        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        private void colocarPecas()
        {
            Tabuleiro.colocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('c', 1).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('c', 2).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('d', 2).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('e', 2).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('e', 1).toPosicao());
            Tabuleiro.colocarPeca(new Rei(Cor.Branca, Tabuleiro), new PosicaoXadrez('d', 1).toPosicao());

            Tabuleiro.colocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('c', 7).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('c', 8).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('d', 7).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('e', 7).toPosicao());
            Tabuleiro.colocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('e', 8).toPosicao());
            Tabuleiro.colocarPeca(new Rei(Cor.Preta, Tabuleiro), new PosicaoXadrez('d', 8).toPosicao());
        }
    }
}
