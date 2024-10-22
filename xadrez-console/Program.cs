using tabuleiro;
using xadrez_console;
using xadrez;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
try
{
    Tabuleiro tabuleiro = new Tabuleiro(8, 8);

    tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0, 0));
    tabuleiro.colocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(1, 3));
    tabuleiro.colocarPeca(new Rei(Cor.Branca, tabuleiro), new Posicao(0, 2));

    Tela.imprimirTabuleiro(tabuleiro);

    Console.ReadLine();

}
catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}

