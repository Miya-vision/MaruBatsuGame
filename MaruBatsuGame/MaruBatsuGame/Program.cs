// See https://aka.ms/new-console-template for more information
public class Program
{
    static void Main() {

        Console.WriteLine("CPUプレイヤーのレベルを選択してください");
        Console.WriteLine("1.普通(Level 1)");
        Console.WriteLine("2.上級(Level 2)");
        string level = Console.ReadLine();
        Console.WriteLine(level);

        Console.Clear();

        //string? input = null;
        string[,] input = new string[,]
        {
            {null,null,null},
            {null,null,null},
            {null,null,null}
        };

        WriteBoard(input);

        Console.WriteLine("どこに置きますか？（１～９）");
        string inputN = Console.ReadLine();
        input[0, 0]  = "〇";

        Console.Clear();

        WriteBoard(input);
        
        Console.WriteLine("ゲーム終了です");
    }

    private static void WriteBoard(string[,] input)
    {
        if(input == null){
            input[0,0] = string.Empty;
        }
        Console.WriteLine("+----+----+----+");
        Console.WriteLine($"|{input[0,0]}   |input[0,1]   |input[0,2]   |");
        Console.WriteLine("+----+----+----+");
        Console.WriteLine($"|input[1,0]   |input[1,1]   |input[1,2]   |");
        Console.WriteLine("+----+----+----+");
        Console.WriteLine($"|input[2,0]   |input[2,1]   |input[2,2]   |");
        Console.WriteLine("+----+----+----+");

    }
}
