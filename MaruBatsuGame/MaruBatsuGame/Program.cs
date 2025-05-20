// See https://aka.ms/new-console-template for more information

using MaruBatsuGame;

public class Program
{
    static void Main()
    {
        // CPUのレベル選択
        while (true)
        {
            Console.WriteLine("CPUプレイヤーのレベルを選択してください");
            Console.WriteLine("1.普通(Level 1)");
            Console.WriteLine("2.上級(Level 2)");

            if (Cpu.InputLevel())
            {
                break;
            }
            else
            {
                Console.WriteLine("無効な入力です。もう一度入力してください。");
            }
        }

        //ゲームのメイン操作
        GameManager.PlayGame();

    }
}

