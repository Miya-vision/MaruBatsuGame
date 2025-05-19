namespace MaruBatsuGame
{
    internal class Cpu : PlayerBase
    {
        private static int cpuLevel = 0;

        //CPU側の入力
        //→優先順位　５のマスをとる＞４隅をとる＞相手の勝ちを阻止する>ダブルリーチの状態を作る
        public static void ChoiceCpuNumber(int[,] state)
        {
            MakeEmptycellsList(state);
            //var cornerList = new List<(int, int)> { (0, 0), (0, 2), (2, 0), (2, 2) };

            if (Board.emptyCells.Count > 0)
            {
                if (cpuLevel == 1)
                {
                    //空いてる個所にランダム入力
                    Random n = new Random();
                    var (row, col) = Board.emptyCells[n.Next(Board.emptyCells.Count)];
                    state[row, col] = 2;
                }

                if (cpuLevel == 2)
                {
                    //勝つために優先順位に応じて入力
                    //５のマス優先的に入力
                    if (state[1, 1] == 0)
                    {
                        state[1, 1] = 2;
                    }
                    /*else if (cornerList.Count > 0)//４隅を優先的に入力
                    {
                        Random n = new Random();
                        var (row, col) = cornerList[n.Next(cornerList.Count)];
                        state[row, col] = 2;
                    }*/
                    else
                    {
                        Random n = new Random();
                        var (row, col) = Board.emptyCells[n.Next(Board.emptyCells.Count)];
                        state[row, col] = 2;

                    }
                }
            }
        }
    }
}
