using System;
using tabuleiro;

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro tabuleiro { get; set; }
        private int turno;
        private Cor jogadorAtual;
        public bool terminada { get; private set; }

        public PartidaDeXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tabuleiro.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(p, destino);
        }

        private void colocarPecas()
        {
            tabuleiro.colocarPeca(new Torre(Cor.Branca, tabuleiro), new PosicaoXadrez('c', 1).toPosicao());
            tabuleiro.colocarPeca(new Torre(Cor.Branca, tabuleiro), new PosicaoXadrez('c', 2).toPosicao());
            tabuleiro.colocarPeca(new Torre(Cor.Branca, tabuleiro), new PosicaoXadrez('d', 2).toPosicao());
            tabuleiro.colocarPeca(new Torre(Cor.Branca, tabuleiro), new PosicaoXadrez('e', 2).toPosicao());
            tabuleiro.colocarPeca(new Torre(Cor.Branca, tabuleiro), new PosicaoXadrez('e', 1).toPosicao());
            tabuleiro.colocarPeca(new Rei(Cor.Branca, tabuleiro), new PosicaoXadrez('d', 1).toPosicao());

            tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new PosicaoXadrez('c', 7).toPosicao());
            tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new PosicaoXadrez('c', 8).toPosicao());
            tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new PosicaoXadrez('d', 7).toPosicao());
            tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new PosicaoXadrez('e', 7).toPosicao());
            tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new PosicaoXadrez('e', 8).toPosicao());
            tabuleiro.colocarPeca(new Rei(Cor.Preta, tabuleiro), new PosicaoXadrez('d', 8).toPosicao());
        }
    }
}
