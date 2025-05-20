using static MaruBatsuGame.PlayerBase;

namespace MaruBatsuGame
{
    public class GameManager
    {
        public static void PlayGame()
        {
            /*------------------------------------------------------------------
            ※入力が1Pと2Pが置いたことで1ループとなっているので、概念が少し歪になっている。１コマで１ループがよい

            空欄部分チェック（共通項目）
            ↓                
            入力（個別項目有り）
            入力までの手順は違えど結果は同じように空欄部分への入力
            ターン制入力にすればプレイヤー側の入力成功チェックがいらなくなる？
            今誰のターンなのかステータスを保持してそれを受け渡す？　P＝１　C＝２左←ダメ
            先攻後攻を選ぶ仕様にするのならプレイヤーだからCPUだからと固定せずに
            単純に先攻は１後攻は２などのようにするべき？
            数字はわかりにくいFirstPlayer,SecondPlayerにする？
            定数作ると管理が楽？const?enum?
            ↓
            勝敗チェック（共通項目）
            「～の勝ちです」と表示
            ステータスの受け渡しで勝敗のメッセージに対応できる？
            ↓
            勝負がつかなかった場合は引き分けの表示
            ------------------------------------------------------------------
            */

            //PlayerクラスとCpuクラスのインスタンス生成
            Player player = new Player();
            Cpu cpu = new Cpu();

            /// <summary>
            /// ゲーム開始時に、先攻プレイヤーを現行プレイヤーとしてセット
            /// プレイヤーのターン管理に使用され、交代時に更新される
            /// </summary>
            int CurrentPlayer = (int)PlayerType.FirstPlayer;

            //初期状態のボードの表示
            Board.WriteBoard(Board.state);

            //空欄がなくなるまで繰り返す
            while (!PlayerBase.IsBoardFull(Board.state))
            {
                /// <summary>いらない？　なくても動く　消す
                /// ボードの状態をもとに、空欄セルのリストを更新
                /// 空きマスの管理に使用され、プレイヤーの選択可能な位置を決定
                /// </summary>
                //PlayerBase.UpdateEmptyCells(Board.state);

                //入力
                if (CurrentPlayer == (int)PlayerType.FirstPlayer)
                {
                    Player.PlayerChoiceNumber(Board.state);
                }
                else
                {
                    Cpu.ChoiceCpuNumber(Board.state);
                }

                //勝敗判定　勝利決定時即終了
                if (Board.CheckWinner(Board.state, CurrentPlayer))
                {
                    Console.Clear();

                    Board.WriteBoard(Board.state);

                    //勝敗決定時には現行プレイヤーの名前
                    GameOver(CurrentPlayer);

                    return;
                }

                Console.Clear();

                Board.WriteBoard(Board.state);

                //ターン交代　現行プレイヤーがFirstならSecondへ
                if (CurrentPlayer == (int)PlayerType.FirstPlayer)
                {
                    CurrentPlayer = (int)PlayerType.SecondPlayer;
                }
                else
                {
                    CurrentPlayer = (int)PlayerType.FirstPlayer;
                }

            }

            //【引き分け】
            Console.WriteLine("ゲーム終了、引き分けです");

        }

        //「ゲーム終了です」と表示
        private static void GameOver(int result)
        {
            Console.WriteLine("ゲーム終了、" + result + "の勝ちです");
        }
    }
}
