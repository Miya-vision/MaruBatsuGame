namespace MaruBatsuGame
{
    internal class Player : PlayerBase
    {

        //プレイヤー側の入力
        public static void PlayerChoiceNumber(int[,] state)
        {

            if (0 == Board.emptyCells.Count)
            {
                Console.WriteLine("ゲーム終了です。");
                return;
            }

            Console.WriteLine("どこに置きますか？（１～９）");

            while (true)
            {
                //置きたい場所の選択
                string inputN = Console.ReadLine();

                //入力値確認（空欄不可、数字のみ）
                if (string.IsNullOrEmpty(inputN) || !int.TryParse(inputN, out int num))
                {
                    Console.WriteLine("無効な入力です。１～９の値を入力してください。");
                    continue;
                }

                //１～９の数字か確認
                if (num < 1 || num > 9)
                {
                    Console.WriteLine("１～９の値を入力してください。");
                    continue;
                }

                //座標の位置決め
                int row = (num - 1) / 3;
                int col = (num - 1) % 3;

                //空欄の確認　0なら入力できる
                if (state[row, col] == (int)PlayerType.None)
                {
                    state[row, col] = (int)PlayerType.FirstPlayer;
                    break;
                }
                else
                {
                    Console.WriteLine("別の場所を選んでください");
                }
            }
        }
    }
}
