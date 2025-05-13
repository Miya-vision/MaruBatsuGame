// See https://aka.ms/new-console-template for more information

public class Program
{
    private static List<(int, int)> emptyCells = new List<(int, int)>();//空欄の情報を入れるリスト

    static void Main()
    {
        // CPUのレベル選択
        bool isLevelInputSuccessful = false;
        while (!isLevelInputSuccessful)
        {
            isLevelInputSuccessful = InputLevel();
            if (!isLevelInputSuccessful)
            {
                Console.WriteLine("無効な入力です。もう一度入力してください。");
            }
        }

        //〇×ゲームのボード作成
        string[,] input = new string[,]
        {
            {"   ","   ","   "},
            {"   ","   ","   "},
            {"   ","   ","   "}
        };

        //ボードの初期状態の表示
        WriteBoard(input);

        //難易度普通

        //int turn = 0;
        //入力回数が９未満で"　"が０個ではない時ゲーム終了
        while (/*turn < 9*/ !IsBoardFull(input))
        {
            //ユーザー側の入力
            bool isPlayerTurnSuccessful = InputNumber(input);

            //勝敗チェック
            if (CheckWinner(input, " ○ "))
            {
                Console.Clear();

                WriteBoard(input);

                GameOver(" ○ ");

                return;
            }
            else
            {
                WriteBoard(input);
            }

            //入力成功時　CPUのターン
            if (isPlayerTurnSuccessful)
            {
                Console.Clear();

                //CPU側の入力
                ChoiceCpuNumber(input);

                //勝敗チェック
                if (CheckWinner(input, " × "))
                {
                    Console.Clear();

                    WriteBoard(input);

                    GameOver(" × ");

                    return;
                }
                else
                {
                    WriteBoard(input);
                }
            }
        }
        //turn ++;

        //ゲーム終了メッセージの表示
        Console.WriteLine("引き分けです");
    }

    private static bool InputLevel()//難易度の選択
    {
        Console.WriteLine("CPUプレイヤーのレベルを選択してください");
        Console.WriteLine("1.普通(Level 1)");
        Console.WriteLine("2.上級(Level 2)");

        string level = Console.ReadLine();
        if (!string.IsNullOrEmpty(level) && int.TryParse(level, out int levelNum) && levelNum >= 1 && levelNum <= 2)
        {
            Console.Clear();
            Console.WriteLine("Level" + level + "を選択しました");

            return true;
        }
        else
        {
            return false;
        }
    }
    private static void WriteBoard(string[,] input)//〇×ゲームのボード
    {
        Console.WriteLine("+---+---+---+");
        Console.WriteLine($"|{input[0, 0]}|{input[0, 1]}|{input[0, 2]}|");
        Console.WriteLine("+---+---+---+");
        Console.WriteLine($"|{input[1, 0]}|{input[1, 1]}|{input[1, 2]}|");
        Console.WriteLine("+---+---+---+");
        Console.WriteLine($"|{input[2, 0]}|{input[2, 1]}|{input[2, 2]}|");
        Console.WriteLine("+---+---+---+");
    }

    private static bool InputNumber(string[,] input)//ユーザー側の入力
    {
        Console.WriteLine("どこに置きますか？（１～９）");

        MakeEmptycellsList(input);

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

                    //空欄の確認　"　"なら〇が入力できる
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

    private static void ChoiceCpuNumber(string[,] input)//CPU側の入力
    {
        MakeEmptycellsList(input);

        //空欄がある場合
        if (emptyCells.Count > 0)
        {
            Random n = new Random();
            var (row, col) = emptyCells[n.Next(emptyCells.Count)];
            input[row, col] = " × ";
        }
    }

    private static void MakeEmptycellsList(string[,] input)//空欄を確認し、格納するリストを作成
    {
        //リストのリセット
        emptyCells.Clear();

        for (int i = 0; i < input.GetLength(0); i++)
        {
            for (int j = 0; j < input.GetLength(1); j++)
            {
                if (input[i, j] == "   ")
                {
                    emptyCells.Add((i, j));//"　"の数をカウントしてリストに加える
                }
            }
        }
    }

    private static bool IsBoardFull(string[,] input)//"　"が０個ならtrueを返す
    {
        MakeEmptycellsList(input);
        return emptyCells.Count == 0;
    }

    private static bool CheckWinner(string[,] input, string target)//縦横対角線の入力チェック
    {
        //横列のチェック
        for (int i = 0; i < 3; i++)
        {
            if (input[i, 0] == target && input[i, 1] == target && input[i, 2] == target)
            {
                return true;
            }
        }

        //縦列のチェック
        for (int j = 0; j < 3; j++)
        {
            if (input[0, j] == target && input[1, j] == target && input[2, j] == target)
            {
                return true;
            }
        }

        //対角線のチェック
        if ((input[0, 0] == target && input[1, 1] == target && input[2, 2] == target)
                || (input[0, 2] == target && input[1, 1] == target && input[2, 0] == target))
        {
            return true;
        }

        return false;
    }

    private static void GameOver(string result)//「ゲーム終了です」と表示
    {
        Console.WriteLine("ゲーム終了、" + result + "の勝ちです");
    }
}

