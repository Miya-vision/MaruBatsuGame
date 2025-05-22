using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace MaruBatsuGame
{
    internal class Cpu : PlayerBase
    {
        private static int cpuLevel = 0;

        public static void InputLevel()// 難易度の選択
        {
            while (true)
            {
                Console.WriteLine("CPUプレイヤーのレベルを選択してください");
                Console.WriteLine("【１】普通(Level 1)");
                Console.WriteLine("【２】上級(Level 2)");
                Console.Write("番号を入力してください: ");

                string level = Console.ReadLine();

                if (!string.IsNullOrEmpty(level) && int.TryParse(level, out int levelNum) && levelNum >= 1 && levelNum <= 2)
                {
                    // 現在の難易度をセット
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

        // CPU側の入力
        public static void ChoiceCpuNumber(int[,] state)
        {
            // ランダムインスタンス（使い回し）
            Random n = new Random();

            // 空欄チェック
            Board.UpdateEmptyCells(state);

            if (Board.emptyCells.Count > 0)
            {
                // 難易度【普通】：空いてる個所にランダム入力
                if (cpuLevel == 1)
                {
                    var (row, col) = Board.emptyCells[n.Next(Board.emptyCells.Count)];
                    state[row, col] = (int)PlayerType.SecondPlayer;
                }

                // 難易度【上級】：優先順位に応じた入力
                if (cpuLevel == 2)
                {
                    // 現在のプレイヤー（手番）を取得
                    PlayerType currentPlayer = (PlayerType)PlayerBase.currentPlayer;

                    // 対戦相手（手番を交代するプレイヤー）を取得
                    PlayerType opponentPlayer = (currentPlayer == PlayerType.FirstPlayer) ? PlayerType.SecondPlayer : PlayerType.FirstPlayer;

                    // 自分が勝てる手があるかをチェック（勝利手があれば優先）
                    var winningMove = Board.GetStrategicMove(Board.state, currentPlayer);

                    // 相手が次の手で勝つ可能性があるかをチェック（勝ち筋を防ぐ）
                    var blockingMove = Board.GetStrategicMove(Board.state, opponentPlayer);

                    // 4隅のリスト
                    var cornerList = new List<(int, int)> { (0, 0), (0, 2), (2, 0), (2, 2) };

                    // 4隅の中から空欄箇所をチェック
                    var validCorners = cornerList.Where(c => state[c.Item1, c.Item2] == (int)PlayerType.None).ToList();

                    // ５の位置優先
                    if (state[1, 1] == (int)PlayerType.None)
                    {
                        state[1, 1] = (int)PlayerType.SecondPlayer;
                    }

                    // 勝てる手があれば優先
                    else if　(winningMove.HasValue)
                    {
                        var (row, col) = winningMove.Value;
                        state[row, col] = (int)PlayerType.SecondPlayer;
                    }

                    // 相手が勝ちそうならブロック
                    else if (blockingMove.HasValue)
                    {
                        var (row, col) = blockingMove.Value;
                        state[row, col] = (int)PlayerType.SecondPlayer;
                    }

                    // ４隅から選択
                    else if (validCorners.Count > 0)
                    {
                        var (row, col) = validCorners[n.Next(validCorners.Count)];
                        state[row, col] = (int)PlayerType.SecondPlayer;
                    }

                    // 優先順位がない場合（ランダム入力）
                    else
                    {
                        var (row, col) = Board.emptyCells[n.Next(Board.emptyCells.Count)];
                        state[row, col] = (int)PlayerType.SecondPlayer;

                    }
                }
            }
        }
    }
}
