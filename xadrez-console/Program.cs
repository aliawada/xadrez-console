using tabuleiro;
using xadrez_console;
using xadrez;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

PosicaoXadrez pos = new PosicaoXadrez('a', 1);
Console.WriteLine(pos);

Console.WriteLine(pos.toPosicao());

Console.ReadLine();



