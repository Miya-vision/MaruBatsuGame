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

        //先攻・後攻の選択
        public static int startingTurnChoice()

        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("先攻・後攻を選んでください。");
                Console.WriteLine("[1] 先攻を選ぶ");
                Console.WriteLine("[2] 後攻を選ぶ");
                Console.Write("番号を入力してください: ");


                string startingPlayer = Console.ReadLine();

                if (!string.IsNullOrEmpty(startingPlayer) && int.TryParse(startingPlayer, out int startingPlayerNum)
                        && startingPlayerNum >= 1 && startingPlayerNum <= 2)
                {
                    Console.Clear();

                    if (startingPlayerNum == 1)
                    {
                        Console.WriteLine("先攻でゲームを開始します。");
                    }
                    else
                    {
                        Console.WriteLine("後攻でゲームを開始します。");
                    }

                    //currentPlayerに先攻・後攻をセットするための値を渡す
                    return startingPlayerNum;
                }

                Console.WriteLine("無効な入力です。もう一度選択してください。");
            }
        }

        //現行プレイヤーに先攻・後攻をセットする
        public static void setCurrentPlayer(int startingPlayer)
        {
            //先攻を選択していた場合
            if (startingPlayer == 1)
            {
                GameManager.currentPlayer = (int)PlayerType.FirstPlayer;

            }
            //後攻を選択していた場合
            else if (startingPlayer == 2)
            {
                GameManager.currentPlayer = (int)PlayerType.SecondPlayer;
            }

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