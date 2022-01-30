using UnityEngine;
using System.Collections;

// 斧の駒
public class Axe : Piece
{
    public override BattleJudges judgeOppositePiece(Piece oppositePiece)
    {
        if(oppositePiece is Axe)
        {
            return BattleJudges.Draw;
        }
        else if(oppositePiece is Spear)
        {
            return BattleJudges.Win;
        }
        else
        {
            return BattleJudges.Lose;
        }
    }
}
