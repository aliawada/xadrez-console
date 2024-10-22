using tabuleiro;
using xadrez_console;
using xadrez;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
try
{
    PartidaDeXadrez partida = new PartidaDeXadrez();

    while (!partida.terminada)
    {
        Console.Clear();
        Tela.imprimirTabuleiro(partida.tabuleiro);

        Console.WriteLine();
        Console.Write("Origem: ");
        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
        Console.Write("Destino: ");
        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

        partida.executaMovimento(origem, destino);
    }


    Console.ReadLine();

}
catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}

