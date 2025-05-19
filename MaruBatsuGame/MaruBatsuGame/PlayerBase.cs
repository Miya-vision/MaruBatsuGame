namespace MaruBatsuGame
{
    internal class PlayerBase
    {   //0が０個ならtrueを返す
        public static bool IsBoardFull(int[,] state)
        {
            MakeEmptycellsList(state);
            return Board.emptyCells.Count == 0;
        }

        //空欄を確認し、格納するリストを作成
        public static void MakeEmptycellsList(int[,] state)
        {
            //リストのリセット
            Board.emptyCells.Clear();

            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    if (state[i, j] == 0)
                    {
                        //0の数をカウントしてリストに加える
                        Board.emptyCells.Add((i, j));
                    }
                }
            }
        }
    }
}