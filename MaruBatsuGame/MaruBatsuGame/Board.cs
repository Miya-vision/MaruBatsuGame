using static MaruBatsuGame.PlayerBase;

namespace MaruBatsuGame
{
    internal class Board
    {
        //空欄の情報を入れるリスト
        public static List<(int, int)> emptyCells = new List<(int, int)>();

        //〇×ゲームのボード作成
        public static int[,] state = new int[,]
        {
            {0, 0, 0},
            {0, 0, 0},
            {0, 0, 0}
        };

        //〇×ゲームのボードの状態を表示
        public static void WriteBoard(int[,] state)
        {
            //配列の要素を順番に調べて値を取得
            Console.WriteLine("+---+---+---+");
            for (int i = 0; i < state.GetLength(0); i++)
            {
                Console.Write("|");

                for (int j = 0; j < state.GetLength(1); j++)
                {
                    //stateの数字を確認して○、×、＿へ変換
                    Console.Write(state[i, j] == (int)PlayerType.None ? "　 " : state[i, j] == (int)PlayerType.FirstPlayer ? " ○ " : " × ");
                    Console.Write("|");
                }
                Console.WriteLine("");
                Console.WriteLine("+---+---+---+");
            }
        }

        //勝ちパターンの8個の配列を作成して勝敗チェック
        public static bool CheckWinner(int[,] state, int target)
        {
            int[][] winPatterns =
            {
            new [] {0, 1, 2},
            new [] {3, 4, 5},
            new [] {6, 7, 8},
            new [] {0, 3, 6},
            new [] {1, 4, 7},
            new [] {2, 5, 8},
            new [] {0, 4, 8},
            new [] {2, 4, 6}
            };

            int[] conversionState = state.Cast<int>().ToArray();

            return winPatterns.Any(pattern => pattern.All(Index => conversionState[Index] == target));
        }

        //勝ち確チェック
        public static void CheckWinPattern(int[,] state, int playerType)
        {
            //対象のプレイヤーが置いた位置を確認したのちに、勝ちパターンと照らし合わせて呼び出し元に返す

            return;
        }
    }
}
