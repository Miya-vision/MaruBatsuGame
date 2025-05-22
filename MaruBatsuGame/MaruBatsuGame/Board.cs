using System.ComponentModel.Design;
using static MaruBatsuGame.PlayerBase;

namespace MaruBatsuGame
{
    internal class Board
    {
        // 空欄の情報を入れるリスト
        public static List<(int, int)> emptyCells = new List<(int, int)>();

        // 〇×ゲームのボード作成
        public static int[,] state = new int[,]
        {
            {0, 0, 0},
            {0, 0, 0},
            {0, 0, 0}
        };

        // 〇×ゲームのボードの状態を表示
        public static void WriteBoard(int[,] state)
        {
            // 配列の要素を順番に調べて値を取得
            Console.WriteLine("+---+---+---+");
            for (int i = 0; i < state.GetLength(0); i++)
            {
                Console.Write("|");

                for (int j = 0; j < state.GetLength(1); j++)
                {
                    // stateの数字を確認して○、×、＿へ変換
                    Console.Write(state[i, j] == (int)PlayerType.None ? "　 " : state[i, j] == (int)PlayerType.FirstPlayer ? " ○ " : " × ");
                    Console.Write("|");
                }
                Console.WriteLine("");// 改行
                Console.WriteLine("+---+---+---+");
            }
        }

        // 空欄を確認し、格納するリストを作成
        public static void UpdateEmptyCells(int[,] state)
        {
            // リストのリセット
            Board.emptyCells.Clear();

            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    if (state[i, j] == 0)
                    {
                        // 空欄の数をカウントしてリストに加える
                        Board.emptyCells.Add((i, j));
                    }
                }
            }
        }

        // 空欄なしの場合trueを返す
        public static bool IsBoardFull(int[,] state)
        {
            UpdateEmptyCells(state);
            return Board.emptyCells.Count == 0;
        }

        //勝ちパターンリストの作成
        public static List<List<(int, int)>> GetWinPatterns()
        {
            return new List<List<(int, int)>>
            {
                new List<(int, int)> { (0, 0), (0, 1), (0, 2) },
                new List<(int, int)> { (1, 0), (1, 1), (1, 2) },
                new List<(int, int)> { (2, 0), (2, 1), (2, 2) },
                new List<(int, int)> { (0, 0), (1, 0), (2, 0) },
                new List<(int, int)> { (0, 1), (1, 1), (2, 1) },
                new List<(int, int)> { (0, 2), (1, 2), (2, 2) },
                new List<(int, int)> { (0, 0), (1, 1), (2, 2) },
                new List<(int, int)> { (0, 2), (1, 1), (2, 0) }
            };
        }

        // 勝者を判定するメソッド
        public static bool CheckWinner(int[,] state, int target)
        {
            // 勝ちパターンの取得
            var winPatterns = GetWinPatterns();

            // 各勝ちパターンと現在の盤面を比較
            foreach (var pattern in winPatterns)
            {
                if (pattern.All(cell => state[cell.Item1, cell.Item2] == target))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 盤面の状態を確認し、次の一手で勝てる場合の座標を返す。
        /// もし勝てる手がない場合は、null を返す。
        /// </summary>
        public static (int, int)? GetStrategicMove(int[,] state, PlayerType targetPlayer)
        {
            //勝ちパターンを取得
            var winPatterns = GetWinPatterns();

            // ○,×が置いてある位置の確認
            // 勝ちパターンに該当するか確認
            // →優先的に置くか判断する
            foreach (var pattern in winPatterns)
            {
                // 勝ちパターン内の自分のマークの数
                int countTargetPlayer = 0;

                // 空欄の位置（最後の一手で勝てる場所）
                (int, int)? emptyCell = null;

                // ○,×が置いてある位置から次の手が勝ちパターンに当てはまるかを確認
                foreach (var (row, col) in pattern)
                {
                    //勝ちパターン内に自分のマークがある場合、カウントを増やす
                    if (state[row, col] == (int)targetPlayer)
                    {
                        countTargetPlayer++;
                    }
                    // 空欄の位置の取得
                    else if (state[row, col] == (int)PlayerType.None)
                    {
                        emptyCell = (row, col);
                    }
                }

                // ２手以上
                // 空欄が値を持っている場合（＝次の手で勝てる状態）
                if (countTargetPlayer == 2 && emptyCell.HasValue)
                {
                    // 勝利するための最適な位置を返す
                    return emptyCell;
                }
            }
            // 勝ち手がない場合は null を返す
            return null;
        }
    }
}
