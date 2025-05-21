// See https://aka.ms/new-console-template for more information

namespace MaruBatsuGame
{
    public class Program
    {
        static void Main()
        {
            // CPUのレベル選択
            Cpu.InputLevel();

            ///<summary>
            ///先攻・後攻の選択を行い、GameManagerで startingPlayerNumに選択時の値を代入
            ///【1】 先攻: FirstPlayer
            ///【2】 後攻: SecondPlayer
            /// </summary>
            int startingPlayerNum = PlayerBase.startingTurnChoice();

            //ゲームのメイン操作
            GameManager.PlayGame(startingPlayerNum);

        }
    }
}

