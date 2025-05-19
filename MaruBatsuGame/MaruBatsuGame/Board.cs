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
                    Console.Write(state[i, j] == 0 ? "　 " : state[i, j] == 1 ? " ○ " : " × ");
                    Console.Write("|");
                }
                Console.WriteLine("");
                Console.WriteLine("+---+---+---+");
            }
        }
    }

}
