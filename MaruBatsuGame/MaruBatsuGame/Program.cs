// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;

public class Program
{
    static void Main()
    {
        //CPUのレベル選択
        Console.WriteLine("CPUプレイヤーのレベルを選択してください");
        Console.WriteLine("1.普通(Level 1)");
        Console.WriteLine("2.上級(Level 2)");
        string level = Console.ReadLine();
        Console.WriteLine(level);

        //コンソールクリア
        Console.Clear();

        //〇×ゲームのボード作成
        string[,] input = new string[,]
        {
            {"   ","   ","   "},
            {"   ","   ","   "},
            {"   ","   ","   "}
        };

        //ボードの初期状態の表示
        WriteBoard(input);

        //規定回数まで入力を繰り返す
        //※修正事項　whileを消すと入力一回でゲーム終了になってしまう(優先順位：低）
        //※修正事項　ゲーム終了画面が残っているターンの数だけ表示されてしまう
        int turn = 0;
        while (turn < 9)
        {
            //ユーザー側の入力
            bool isPlayerTurnSuccessful = InputNumber(input);
            
            //現在のボード状態を表示
             WriteBoard(input);
            
            //入力成功時　CPUのターン
            if (isPlayerTurnSuccessful)
            {   
                //入力履歴の削除
                Console.Clear();

                //CPU側の入力
                ChoiceCpuNumber(input);

                //現在のボード状態を表示
                WriteBoard(input);
            }
        turn++;
        }

        //ゲーム終了メッセージの表示
        Console.WriteLine("ゲーム終了です");
    }

    private static void WriteBoard(string[,] input)
    {
        //〇×ゲームのボード
        Console.WriteLine("+---+---+---+");
        Console.WriteLine($"|{input[0, 0]}|{input[0, 1]}|{input[0, 2]}|");
        Console.WriteLine("+---+---+---+");
        Console.WriteLine($"|{input[1, 0]}|{input[1, 1]}|{input[1, 2]}|");
        Console.WriteLine("+---+---+---+");
        Console.WriteLine($"|{input[2, 0]}|{input[2, 1]}|{input[2, 2]}|");
        Console.WriteLine("+---+---+---+");
    }

    private static bool InputNumber(string[,] input)
    {
        //〇を置く場所を入力
        Console.WriteLine("どこに置きますか？（１～９）");

        //空欄の確認
        //格納するリストを作成
        List<(int, int)> emptyCells = new List<(int, int)>();
        for (int i = 0; i < input.GetLength(0); i++)
        {
            for (int j = 0; j < input.GetLength(1); j++)
            {
                if (input[i, j] == "   ")
                {
                    emptyCells.Add((i, j));
                }
            }
        }

        //空欄の確認
        if (emptyCells.Count > 0)
        {
            string inputN = Console.ReadLine();

            //入力値が有効な値か確認
            if (!string.IsNullOrEmpty(inputN) && int.TryParse(inputN, out int num))
            {
                //１～９の数字か確認
                if (num >= 1 && num <= 9)
                {
                    int row = (num - 1) / 3;
                    int col = (num - 1) % 3;

                    //空欄の確認
                    if (input[row, col] == "   ")
                    {
                        input[row, col] = " ○ ";
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

    private static void ChoiceCpuNumber(string[,] input)
    {
        //CPU選択

        //空欄の確認
        //格納するリストを作成
        List<(int, int)> emptyCells = new List<(int, int)>();
        for (int i = 0; i < input.GetLength(0); i++)
        {
            for (int j = 0; j < input.GetLength(1); j++)
            {
                if (input[i, j] == "   ")
                {
                    emptyCells.Add((i, j));
                }
            }
        }

        //空欄がある場合
        if (emptyCells.Count > 0)
        {
            Random n = new Random();
            var (row, col) = emptyCells[n.Next(emptyCells.Count)];
            input[row, col] = " × ";
        }
    }
}

