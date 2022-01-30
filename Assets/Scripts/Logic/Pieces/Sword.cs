using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 剣の駒
public class Sword : Piece
{
    public override BattleJudges judgeOppositePiece(Piece oppositePiece)
    {
        if (oppositePiece is Sword)
        {
            return BattleJudges.Draw;
        }
        else if (oppositePiece is Axe)
        {
            return BattleJudges.Win;
        }
        else
        {
            return BattleJudges.Lose;
        }
    }
}
