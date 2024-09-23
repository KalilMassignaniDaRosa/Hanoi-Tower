using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HanoiTower
{
    class Program
    {
        private const int DISCS_COUNT = 10;
        private const int DELAY_MS = 250;
        private static int _columnSize = 30;

        static void Main(string[] args){
            //Math.Max retorna o maior valor
            //Ajusta a torre para o tamanho dos discos
            _columnSize = Math.Max(6, GetDiscWidth(DISCS_COUNT) + 2);
            HanoiTower algorithm = new HanoiTower(DISCS_COUNT);
            //Sempre que um movimento for completado ele ira mostrar o resultado
            algorithm.MoveCompleted += Algorithm_Visualize;
            //Mostra o estado inicial das torre sem discos
            Algorithm_Visualize(algorithm, EventArgs.Empty);
            algorithm.Start();
        }

        private static void Algorithm_Visualize(object sender, EventArgs e){
            Console.Clear();
            //Acessa propriedades e metodos da classe
            HanoiTower algorithm = (HanoiTower)sender;
            if (algorithm.DiscsCount <= 0){
                return;
            }
            char[][] visualization = InitializeVisualization(algorithm);
            PrepareColumn(visualization, 1, algorithm.DiscsCount, algorithm.From);
            PrepareColumn(visualization, 2, algorithm.DiscsCount, algorithm.To);
            PrepareColumn(visualization, 3, algorithm.DiscsCount, algorithm.Auxiliary);
            //Exibe o cabeçalho
            Console.WriteLine(Center("FROM") + Center("TO") + Center("AUXILIARY"));
            DrawVisualization(visualization);
            Console.WriteLine();
            Console.WriteLine($"Number of moves: {algorithm.MovesCount}");
            Console.WriteLine($"Number of discs: {algorithm.DiscsCount}");
            //Pequeno delay
            Thread.Sleep(DELAY_MS);
        }

        private static char[][] InitializeVisualization(HanoiTower algorithm){
            //Criando matriz da visualização
            char[][] visualization = new char[algorithm.DiscsCount][];
            //Iniciando cada linha da matriz
            for (int y = 0; y < visualization.Length; y++){
                visualization[y] = new char[_columnSize * 3];
                for (int x = 0; x < _columnSize * 3; x++){
                    visualization[y][x] = ' ';
                }
            }
            return visualization;
        }

        private static void PrepareColumn(char[][] visualization, int column, int discsCount, Stack<int> stack){
            int margin = _columnSize * (column - 1);    
            for (int y = 0; y < stack.Count; y++){
                int size = stack.ElementAt(y);
                //Ajusta para que o discos maiores estajam na base e o menores no topo
                int row = discsCount - (stack.Count - y);
                //Calcula posição inical horizontal para colocar o disco
                int columnStart = margin + discsCount - size;
                //Calcula posição final horizontal, com base na largura
                int columnEnd = columnStart + GetDiscWidth(size);
                //Preenche a linha
                for (int x = columnStart; x <= columnEnd; x++){
                    visualization[row][x] = '\u2580';
                }
            }
        }

        //Coloca em uma matriz na posição certa
        /*
            { ' ', ' ', '=', '=', ' ', ' ' },
            { ' ', '=', ' ', ' ', '=', ' ' },
            { '=', ' ', ' ', ' ', ' ', '=' }

        */
        private static void DrawVisualization(char[][] visualization){
            for (int y = 0; y < visualization.Length; y++){
                Console.WriteLine(new string(visualization[y]));
            }
        }

        //Retorna o texto identado
        private static string Center(string text){
            int margin = (_columnSize - text.Length) / 2;
            return text.PadLeft(margin + text.Length).PadRight(_columnSize);
        }
        
        //Garante que cada disco seja um espaço para cada lado maior que o anterior
        private static int GetDiscWidth(int size){
            return 2 * size - 1;
        }
    }
}
