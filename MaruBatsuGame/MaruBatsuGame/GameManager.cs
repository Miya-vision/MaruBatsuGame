namespace MaruBatsuGame
{
    internal class GameManager
    {
        public static void PlayGame()
        {
            while (/*turn < 9*/ !PlayerBase.IsBoardFull(Board.state))
            {
                /*入力が1Pと2Pが置いたことで1ループとなっているので、概念が少し歪になっている。１コマで１ループがよい
                空欄部分チェック
                ↓                
                入力　入力までの手順は違えど結果は同じように空欄部分への入力
                ターン制入力にすればプレイヤー側の入力成功チェックがいらなくなる？
                ↓
                勝敗チェック　「～の勝ちです」と表示
                ↓
                勝負がつかなかった場合は引き分けの表示
                 */

                //プレイヤー側の入力
                bool isPlayerTurnSuccessful = Player.InputNumber(Board.state);

                //勝敗チェック
                if (CheckWinner(Board.state, 1))
                {
                    Console.Clear();

                    Board.WriteBoard(Board.state);

                    GameOver(1);

                    return;
                }
                else
                {
                    Board.WriteBoard(Board.state);
                }

                //入力成功時　CPUのターン
                if (isPlayerTurnSuccessful)
                {
                    Console.Clear();

                    //CPU側の入力
                    Cpu.ChoiceCpuNumber(Board.state);

                    //勝敗チェック
                    if (CheckWinner(Board.state, 2))
                    {
                        Console.Clear();

                        Board.WriteBoard(Board.state);

                        GameOver(2);

                        return;
                    }
                    else
                    {
                        Board.WriteBoard(Board.state);
                    }
                }
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

        //「ゲーム終了です」と表示
        private static void GameOver(int result)
        {
            Console.WriteLine("ゲーム終了、" + result + "の勝ちです");
        }
    }
}
