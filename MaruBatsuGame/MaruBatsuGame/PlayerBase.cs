namespace MaruBatsuGame
{
    internal class PlayerBase

    {
        /// <summary>
        /// ゲーム内のプレイヤーの役割を表す列挙型
        /// 先攻・後攻を識別し、ボードへの入力時に使用される
        /// </summary>
        public enum PlayerType
        {
            None = 0,//空欄
            FirstPlayer = 1,//先攻
            SecondPlayer = 2//後攻
        }

        //空欄を確認し、格納するリストを作成
        public static void UpdateEmptyCells(int[,] state)
        {
            //リストのリセット
            Board.emptyCells.Clear();

            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    if (state[i, j] == 0)
                    {
                        //空欄の数をカウントしてリストに加える
                        Board.emptyCells.Add((i, j));
                    }
                }
            }
        }

        //空欄なしの場合trueを返す
        public static bool IsBoardFull(int[,] state)
        {
            UpdateEmptyCells(state);
            return Board.emptyCells.Count == 0;
        }
    }
}