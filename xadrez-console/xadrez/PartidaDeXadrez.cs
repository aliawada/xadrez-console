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
        public bool Xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            vulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tabuleiro.retirarPeca(destino);
            p.decrementarQtdMovimentos();
            if (pecaCapturada != null)
            {
                Tabuleiro.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            Tabuleiro.colocarPeca(p, origem);

            // #jogadaespecial roque peq
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca Torre = Tabuleiro.retirarPeca(destinoTorre);
                Torre.decrementarQtdMovimentos();
                Tabuleiro.colocarPeca(Torre, origemTorre);
            }

            // #jogadaespecial roque gran
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca Torre = Tabuleiro.retirarPeca(destinoTorre);
                Torre.decrementarQtdMovimentos();
                Tabuleiro.colocarPeca(Torre, origemTorre);
            }

            // #jogadaespecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao = Tabuleiro.retirarPeca(destino);
                    Posicao posPeao;
                    if (p.Cor == Cor.Branca)
                    {
                        posPeao = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posPeao = new Posicao(4, destino.Coluna);
                    }
                    Tabuleiro.colocarPeca(peao, posPeao);
                }
            }
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tabuleiro.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = Tabuleiro.retirarPeca(destino);
            Tabuleiro.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            // #jogadaespecial roque peq
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca Torre = Tabuleiro.retirarPeca(origemTorre);
                Torre.incrementarQtdMovimentos();
                Tabuleiro.colocarPeca(Torre, destinoTorre);
            }

            // #jogadaespecial roque gran
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca Torre = Tabuleiro.retirarPeca(origemTorre);
                Torre.incrementarQtdMovimentos();
                Tabuleiro.colocarPeca(Torre, destinoTorre);
            }

            // #jogadaespecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posPeao;
                    if (p.Cor == Cor.Branca)
                    {
                        posPeao = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posPeao = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = Tabuleiro.retirarPeca(posPeao);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Voce nao pode se colocar em xeque");
            }

            Peca p = Tabuleiro.peca(destino);

            // #jogadaespecial promocao
            if (p is Peao)
            {
                if ((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7))
                {
                    p = Tabuleiro.retirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(p.Cor, Tabuleiro);
                    Tabuleiro.colocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }

            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (testeXequemate(adversaria(jogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }



            // #jogadaespecial en passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
            }
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
            if (!Tabuleiro.peca(origem).movimentoPossivel(destino))
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

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);

            if (R == null)
            {
                throw new TabuleiroException("Nao tem rei da cor " + cor + " no tabuleiro");
            }

            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequemate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < Tabuleiro.Linha; i++)
                {
                    for (int j = 0; j < Tabuleiro.Coluna; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(Cor.Branca, Tabuleiro));
            colocarNovaPeca('b', 1, new Cavalo(Cor.Branca, Tabuleiro));
            colocarNovaPeca('c', 1, new Bispo(Cor.Branca, Tabuleiro));
            colocarNovaPeca('d', 1, new Dama(Cor.Branca, Tabuleiro));
            colocarNovaPeca('e', 1, new Rei(Cor.Branca, Tabuleiro, this));
            colocarNovaPeca('f', 1, new Bispo(Cor.Branca, Tabuleiro));
            colocarNovaPeca('g', 1, new Cavalo(Cor.Branca, Tabuleiro));
            colocarNovaPeca('h', 1, new Torre(Cor.Branca, Tabuleiro));
            colocarNovaPeca('a', 2, new Peao(Cor.Branca, Tabuleiro, this));
            colocarNovaPeca('b', 2, new Peao(Cor.Branca, Tabuleiro, this));
            colocarNovaPeca('c', 2, new Peao(Cor.Branca, Tabuleiro, this));
            colocarNovaPeca('d', 2, new Peao(Cor.Branca, Tabuleiro, this));
            colocarNovaPeca('e', 2, new Peao(Cor.Branca, Tabuleiro, this));
            colocarNovaPeca('f', 2, new Peao(Cor.Branca, Tabuleiro, this));
            colocarNovaPeca('g', 2, new Peao(Cor.Branca, Tabuleiro, this));
            colocarNovaPeca('h', 2, new Peao(Cor.Branca, Tabuleiro, this));

            colocarNovaPeca('a', 8, new Torre(Cor.Preta, Tabuleiro));
            colocarNovaPeca('b', 8, new Cavalo(Cor.Preta, Tabuleiro));
            colocarNovaPeca('c', 8, new Bispo(Cor.Preta, Tabuleiro));
            colocarNovaPeca('d', 8, new Dama(Cor.Preta, Tabuleiro));
            colocarNovaPeca('e', 8, new Rei(Cor.Preta, Tabuleiro, this));
            colocarNovaPeca('f', 8, new Bispo(Cor.Preta, Tabuleiro));
            colocarNovaPeca('g', 8, new Cavalo(Cor.Preta, Tabuleiro));
            colocarNovaPeca('h', 8, new Torre(Cor.Preta, Tabuleiro));
            colocarNovaPeca('a', 7, new Peao(Cor.Preta, Tabuleiro, this));
            colocarNovaPeca('b', 7, new Peao(Cor.Preta, Tabuleiro, this));
            colocarNovaPeca('c', 7, new Peao(Cor.Preta, Tabuleiro, this));
            colocarNovaPeca('d', 7, new Peao(Cor.Preta, Tabuleiro, this));
            colocarNovaPeca('e', 7, new Peao(Cor.Preta, Tabuleiro, this));
            colocarNovaPeca('f', 7, new Peao(Cor.Preta, Tabuleiro, this));
            colocarNovaPeca('g', 7, new Peao(Cor.Preta, Tabuleiro, this));
            colocarNovaPeca('h', 7, new Peao(Cor.Preta, Tabuleiro, this));
        }
    }
}
