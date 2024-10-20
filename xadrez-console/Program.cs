using tabuleiro;
using xadrez_console;
using xadrez;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Tabuleiro tabuleiro = new Tabuleiro(8, 8);

tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0, 0));
tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(1, 3));
tabuleiro.colocarPeca(new Rei(Cor.Preta, tabuleiro), new Posicao(2, 4));

Tela.imprimirTabuleiro(tabuleiro);

Console.ReadLine();

