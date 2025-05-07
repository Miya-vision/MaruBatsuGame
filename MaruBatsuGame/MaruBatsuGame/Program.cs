// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
        //※修正事項　9マス埋めるではなく9回入力できる状態(10回目にゲーム終了が表示される)
        int turn = 0;
        while (turn < 9)
        {
            //プレイヤー側の入力
            InputNumber(input);

            //現在のボード状態を表示
            WriteBoard(input);

            //入力履歴の削除
            Console.Clear();

            //CPU側の入力
            ChoiceCpuNumber(input);

            //現在のボード状態を表示
            WriteBoard(input);

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

    private static void InputNumber(string[,] input)
    {
        //〇を置く場所を入力
        Console.WriteLine("どこに置きますか？（１～９）");
        {
            string inputN = Console.ReadLine();
            int num = int.Parse(inputN);
            int row = (num - 1) / 3;
            int col = (num - 1) % 3;
            input[row, col] = " ○ ";
        }
        //入力済みの場所を選択した場合
        //Console.WriteLine("選択済みです。別の場所を選択してください。");
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
        if (emptyCells.Count> 0)
        {
            Random n = new Random();
            var (row, col) = emptyCells[n.Next(emptyCells.Count)];
            input[row, col] = " × ";
        }
    }
}