using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            Terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tabuleiro.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = Tabuleiro.retirarPeca(destino);
            Tabuleiro.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if (x.Cor  == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (Tabuleiro.peca(pos) == null)
            {
                throw new TabuleiroException("Nao existe peca na posicao de origem escolhida");
            }

            if (jogadorAtual != Tabuleiro.peca(pos).Cor)
            {
                throw new TabuleiroException("A peca de origem escolhida nao e sua");
            }

            if (!Tabuleiro.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Nao ha movimentos possiveis para a peca de origem escolhida");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.peca(origem).podeMoverPara(destino))
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

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('c', 1, new Torre(Cor.Branca, Tabuleiro));
            colocarNovaPeca('c', 2, new Torre(Cor.Branca, Tabuleiro));
            colocarNovaPeca('d', 2, new Torre(Cor.Branca, Tabuleiro));
            colocarNovaPeca('e', 2, new Torre(Cor.Branca, Tabuleiro));
            colocarNovaPeca('e', 1, new Torre(Cor.Branca, Tabuleiro));
            colocarNovaPeca('d', 1, new Rei(Cor.Branca, Tabuleiro));

            colocarNovaPeca('c', 7, new Torre(Cor.Preta, Tabuleiro));
            colocarNovaPeca('c', 8, new Torre(Cor.Preta, Tabuleiro));
            colocarNovaPeca('d', 7, new Torre(Cor.Preta, Tabuleiro));
            colocarNovaPeca('e', 7, new Torre(Cor.Preta, Tabuleiro));
            colocarNovaPeca('e', 8, new Torre(Cor.Preta, Tabuleiro));
            colocarNovaPeca('d', 8, new Rei(Cor.Preta, Tabuleiro));
        }
    }
}
