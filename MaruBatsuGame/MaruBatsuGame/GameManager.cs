using static MaruBatsuGame.PlayerBase;

namespace MaruBatsuGame
{
    public class GameManager
    {
        public static void PlayGame()
        {
            //currentPlayerに先攻側をセット
            PlayerBase.setCurrentPlayer(firstMover);

            // 初期状態のボードの表示
            Board.WriteBoard(Board.state);

            // 空欄がなくなるまで繰り返す
            while (!Board.IsBoardFull(Board.state))
            {
                Console.WriteLine($"セット後の currentPlayer: {currentPlayer}, opponent: {opponent}"); // デバッグ用
                // 入力
                if (currentPlayer == (int)PlayerType.FirstPlayer)
                {
                    Player.PlayerChoiceNumber(Board.state);
                }
                else
                {
                    Cpu.ChoiceCpuNumber(Board.state);
                }

                // 勝敗判定　勝利決定時即終了
                if (Board.CheckWinner(Board.state, currentPlayer))
                {
                    Console.Clear();

                    Board.WriteBoard(Board.state);

                    // 勝敗決定時には現行プレイヤーの名前
                    GameOver(currentPlayer);

                    return;
                }

                Console.Clear();

                Board.WriteBoard(Board.state);

                // ターン交代　現行プレイヤーがFirstならSecondへ
                if (currentPlayer == (int)PlayerType.FirstPlayer)
                {
                    currentPlayer = (int)PlayerType.SecondPlayer;
                }
                else
                {
                    currentPlayer = (int)PlayerType.FirstPlayer;
                }
            }
            //【引き分け】
            Console.WriteLine("ゲーム終了、引き分けです");
        }

        // 「ゲーム終了です」と表示
        private static void GameOver(int result)
        {
            Console.WriteLine("ゲーム終了、" + result + "の勝ちです");
        }
    }
}

