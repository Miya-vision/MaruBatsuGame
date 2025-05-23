// See https://aka.ms/new-console-template for more information

namespace MaruBatsuGame
{
    public class Program
    {
        static void Main()
        {
            //対戦相手の選択
            PlayerBase.SelectOpponent();

            // 先攻・後攻の選択
            PlayerBase.startingTurnChoice();

            // ゲームのメイン操作
            GameManager.PlayGame();
        }
    }
}

