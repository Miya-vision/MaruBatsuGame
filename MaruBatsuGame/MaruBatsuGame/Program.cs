// See https://aka.ms/new-console-template for more information

public class Program
{
    //playerクラス　CPUクラス（強弱2パターン）を作成
    //空欄の場所を選択するインターフェースを作成

    private static List<(int, int)> emptyCells = new List<(int, int)>();//空欄の情報を入れるリスト
    private static int cpuLevel = 0;//CPUの難易度を保存

    static void Main()
    {
        // CPUのレベル選択
        while (true)
        {
            if (InputLevel())
            {
                break;
            }
            else
            {
                Console.WriteLine("無効な入力です。もう一度入力してください。");
            }
        }

        //〇×ゲームのボード作成

        //盤面の状態を空白付きで保持しているが、0,1,2などで持った方がよい→0:初期値,1:1Pの置き場所,2:2Pの置き場所
        int[,] state = new int[,]
        {
            {0, 0, 0},
            {0, 0, 0},
            {0, 0, 0}
        };

        //ボードの初期状態の表示
        WriteBoard(state);

        //int turn = 0;
        //入力が1Pと2Pが置いたことで1ループとなっているので、概念が少し歪になっている。１コマで１ループがよい
        while (/*turn < 9*/ !IsBoardFull(state))
        {
            //プレイヤー側の入力
            bool isPlayerTurnSuccessful = InputNumber(state);

            //勝敗チェック
            if (CheckWinner(state, 1))
            {
                Console.Clear();

                WriteBoard(state);

                GameOver(1);

                return;
            }
            else
            {
                WriteBoard(state);
            }

            //入力成功時　CPUのターン
            if (isPlayerTurnSuccessful)
            {
                Console.Clear();

                //CPU側の入力
                ChoiceCpuNumber(state);

                //勝敗チェック
                if (CheckWinner(state, 2))
                {
                    Console.Clear();

                    WriteBoard(state);

                    GameOver(2);

                    return;
                }
                else
                {
                    WriteBoard(state);
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
            cpuLevel = levelNum;
            Console.Clear();
            Console.WriteLine("Level" + level + "を選択しました");

            return true;
        }
        else
        {
            return false;
        }
    }

    private static void WriteBoard(int[,] state)//〇×ゲームのボード
    {
        Console.WriteLine("+---+---+---+");
        Console.WriteLine($"|{state[0, 0]}|{state[0, 1]}|{state[0, 2]}|");
        Console.WriteLine("+---+---+---+");
        Console.WriteLine($"|{state[1, 0]}|{state[1, 1]}|{state[1, 2]}|");
        Console.WriteLine("+---+---+---+");
        Console.WriteLine($"|{state[2, 0]}|{state[2, 1]}|{state[2, 2]}|");
        Console.WriteLine("+---+---+---+");
    }

    private static bool InputNumber(int[,] state)//プレイヤー側の入力
    {
        Console.WriteLine("どこに置きますか？（１～９）");

        MakeEmptycellsList(state);

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

    private static void ChoiceCpuNumber(int[,] state)//CPU側の入力
                                                     //→優先順位　５のマスをとる＞４隅をとる＞相手の勝ちを阻止する>ダブルリーチの状態を作る
    {
        MakeEmptycellsList(state);
        var cornerList = new List<(int, int)> { (0, 0), (0, 2), (2, 0), (2, 2) };

        if (emptyCells.Count > 0)
        {
            if (cpuLevel == 1)//空いてる個所にランダム入力
            {
                Random n = new Random();
                var (row, col) = emptyCells[n.Next(emptyCells.Count)];
                state[row, col] = 2;
            }

            if (cpuLevel == 2)//勝つために優先順位に応じて入力
            {
                if (state[1, 1] == 0)//５のマス優先的に入力
                {
                    state[1, 1] = 2;
                }
                else if (cornerList.Count > 0)//４隅を優先的に入力
                {
                    Random n = new Random();
                    var (row, col) = cornerList[n.Next(cornerList.Count)];
                    state[row, col] = 2;
                }
                else
                {
                    Random n = new Random();
                    var (row, col) = emptyCells[n.Next(emptyCells.Count)];
                    state[row, col] = 2;

                }
            }
        }
    }

    private static void MakeEmptycellsList(int[,] state)//空欄を確認し、格納するリストを作成
    {
        //リストのリセット
        emptyCells.Clear();

        for (int i = 0; i < state.GetLength(0); i++)
        {
            for (int j = 0; j < state.GetLength(1); j++)
            {
                if (state[i, j] == 0)
                {
                    emptyCells.Add((i, j));//0の数をカウントしてリストに加える
                }
            }
        }
    }

    private static bool IsBoardFull(int[,] state)//0が０個ならtrueを返す
    {
        MakeEmptycellsList(state);
        return emptyCells.Count == 0;
    }

    private static bool CheckWinner(int[,] state, int target)//縦横対角線の入力チェック

    //→勝ちパターンのチェックに変更【123,456,789,147,258,369,159,257】の８パターン

    {
        //横列のチェック
        for (int i = 0; i < 3; i++)
        {
            if (state[i, 0] == target && state[i, 1] == target && state[i, 2] == target)
            {
                return true;
            }
        }

        //縦列のチェック
        for (int j = 0; j < 3; j++)
        {
            if (state[0, j] == target && state[1, j] == target && state[2, j] == target)
            {
                return true;
            }
        }

        //対角線のチェック
        if ((state[0, 0] == target && state[1, 1] == target && state[2, 2] == target)
                || (state[0, 2] == target && state[1, 1] == target && state[2, 0] == target))
        {
            return true;
        }

        return false;
    }

    private static void GameOver(int result)//「ゲーム終了です」と表示
    {
        Console.WriteLine("ゲーム終了、" + result + "の勝ちです");
    }
}

