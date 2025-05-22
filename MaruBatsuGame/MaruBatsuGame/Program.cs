// See https://aka.ms/new-console-template for more information

using System.ComponentModel;

namespace MaruBatsuGame
{
    public class Program
    {
        static void Main()
        {
            //対戦相手の選択
            PlayerBase.selectOpponent();

            // 先攻・後攻の選択を行い、GameManagerで startingPlayerNumに選択時の値を代入
            //【1】 先攻: FirstPlayer　【2】 後攻: SecondPlayer
            int startingPlayerNum = PlayerBase.startingTurnChoice();

            // ゲームのメイン操作
            GameManager.PlayGame();
        }
    }
}

