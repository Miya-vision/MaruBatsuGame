namespace MaruBatsuGame
{
    internal class Player : PlayerBase
    {
        Board board = new Board();

        //プレイヤー側の入力
        public static bool InputNumber(int[,] state)
        {
            Console.WriteLine("どこに置きますか？（１～９）");

            MakeEmptycellsList(state);

            if (Board.emptyCells.Count > 0)
            {
                string inputN = Console.ReadLine();

                //入力値が有効な値か確認
                if (!string.IsNullOrEmpty(inputN) && int.TryParse(inputN, out int num))
                {
                    //１～９の数字か確認
                    if (num >= 1 && num <= 9)
                    {
                        //座標の位置決め
                        int row = (num - 1) / 3;
                        int col = (num - 1) % 3;

                        //空欄の確認　0なら入力できる
                        if (state[row, col] == 0)
                        {
                            state[row, col] = 1;
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("別の場所を選んでください");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("１～９の値を入力してください。");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("無効な入力です。");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("ゲーム終了です。");
                return false;
            }
        }
    }
}
