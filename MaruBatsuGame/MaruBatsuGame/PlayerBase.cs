namespace MaruBatsuGame
{
    internal class PlayerBase

    {
        // ゲーム内のプレイヤーの役割を表す列挙型
        // 先攻・後攻を識別し、ボードへの入力時に使用される
        public enum PlayerType
        {
            None = 0,// 空欄
            FirstPlayer = 1,// 先攻
            SecondPlayer = 2// 後攻
        }

        // ゲーム開始時に、先攻プレイヤーを現行プレイヤーとしてセット
        // プレイヤーのターン管理に使用され、交代時に更新される
        public static int currentPlayer = 0;

        //対戦相手
        public static int opponent = 0;

        //先攻後攻選択
        public static int firstMover = 0;

        //PvPかPvEを選択
        public static int SelectOpponent()
        {
            while (true)
            {
                Console.WriteLine("対戦相手を選んでください");
                Console.WriteLine("【１】２Pプレイヤー");
                Console.WriteLine("【２】CPU");
                Console.Write("番号を入力してください: ");

                string selectOpponent = Console.ReadLine();

                if (!string.IsNullOrEmpty(selectOpponent) && int.TryParse(selectOpponent, out int selectOpponentNum)
                        && selectOpponentNum >= 1 && selectOpponentNum <= 2)
                {
                    Console.Clear();

                    if (selectOpponentNum == 2)
                    {
                        opponent = selectOpponentNum;

                        // CPUのレベル選択
                        Cpu.InputLevel();

                    }

                    opponent = selectOpponentNum;

                    return selectOpponentNum;

                }
                else
                {
                    Console.WriteLine("無効な入力です。もう一度選択してください。");
                }
            }
        }

        // 先攻・後攻の選択
        //[1] 先攻
        //[2] 後攻
        public static int startingTurnChoice()

        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("先攻・後攻を選んでください。");
                Console.WriteLine("【１】 先攻を選ぶ");
                Console.WriteLine("【２】 後攻を選ぶ");
                Console.Write("番号を入力してください: ");

                string startingPlayer = Console.ReadLine();

                if (!string.IsNullOrEmpty(startingPlayer) && int.TryParse(startingPlayer, out int startingPlayerNum)
                        && startingPlayerNum >= 1 && startingPlayerNum <= 2)
                {
                    Console.Clear();
                    if (startingPlayerNum == 1)
                    {
                        firstMover = startingPlayerNum;
                    }

                    firstMover = startingPlayerNum;

                    Console.WriteLine(startingPlayerNum == 1 ? "先攻でゲームを開始します。" : "後攻でゲームを開始します。");

                    // currentPlayerに先攻・後攻をセットするための値を渡す
                    return startingPlayerNum;
                }

                Console.WriteLine("無効な入力です。もう一度選択してください。");
            }
        }

        //currentPlayerに先攻・後攻時に選択した値をセット
        public static void setCurrentPlayer(int firstMover)
        {
            currentPlayer = (firstMover == 1) ? (int)PlayerType.FirstPlayer : (int)PlayerType.SecondPlayer;
        }
    }
}