namespace MaruBatsuGame
{
    internal class Cpu : PlayerBase
    {
        private static int cpuLevel = 0;

        public static void InputLevel()//難易度の選択
        {
            while (true)
            {
                Console.WriteLine("CPUプレイヤーのレベルを選択してください");
                Console.WriteLine("1.普通(Level 1)");
                Console.WriteLine("2.上級(Level 2)");
                Console.Write("番号を入力してください: ");

                string level = Console.ReadLine();

                if (!string.IsNullOrEmpty(level) && int.TryParse(level, out int levelNum) && levelNum >= 1 && levelNum <= 2)
                {
                    //現在の難易度をセット
                    cpuLevel = levelNum;

                    Console.Clear();
                    
                    Console.WriteLine("Level" + level + "を選択しました");

                    break;
                }
                else
                {
                    Console.WriteLine("無効な入力です。もう一度選択してください。");
                }
            }
        }

        //CPU側の入力
        public static void ChoiceCpuNumber(int[,] state)
        {
            //4隅のリスト
            var cornerList = new List<(int, int)> { (0, 0), (0, 2), (2, 0), (2, 2) };

            //4隅の中から空欄箇所をチェック
            var validCorners = cornerList.Where(c => state[c.Item1, c.Item2] == (int)PlayerType.None).ToList();

            //空欄チェック
            PlayerBase.UpdateEmptyCells(state);

            if (Board.emptyCells.Count > 0)
            {
                //難易度【普通】：空いてる個所にランダム入力
                if (cpuLevel == 1)
                {
                    Random n = new Random();
                    var (row, col) = Board.emptyCells[n.Next(Board.emptyCells.Count)];
                    state[row, col] = (int)PlayerType.SecondPlayer;
                }

                //難易度【上級】：優先順位に応じた入力
                if (cpuLevel == 2)
                {
                    //５の位置優先
                    if (state[1, 1] == (int)PlayerType.None)
                    {
                        state[1, 1] = (int)PlayerType.SecondPlayer;
                    }
                    /*相手を妨害
                    else if(blockingMove != null)
                    {   
                        相手の勝ち確を妨害する
                        var blockingMove　= Board.CheckWinPattern(state, int playerType();
                    }
                    else if(doubleThreatMove != null)
                    {
                        ダブルリーチの作成など自分に有利な動きをする
                        var blockingMove　= Board.CheckWinPattern(state, int playerType();
                    }
                    */
                    //４隅優先
                    else if (cornerList.Count > 0)
                    {
                        //4隅の空いてる部分にランダム入力
                        Random n = new Random();
                        var (row, col) = validCorners[n.Next(validCorners.Count)];
                        state[row, col] = (int)PlayerType.SecondPlayer;
                    }
                    else
                    {
                        Random n = new Random();
                        var (row, col) = Board.emptyCells[n.Next(Board.emptyCells.Count)];
                        state[row, col] = (int)PlayerType.SecondPlayer;

                    }
                }
            }
        }
    }
}
