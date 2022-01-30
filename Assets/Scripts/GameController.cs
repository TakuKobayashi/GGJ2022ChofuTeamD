using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : SingletonBehaviour<GameController>
{
    private Resut _resut = Resut.Lose;

    // 自分の手番
    [SerializeField] private Player myPlayer;
    // 相手の手番
    [SerializeField] private Player opponentPlayer;

    // ターンは交互にやってくるので今のターンのPlayerの情報を保持する
    private Player currentTurnPlayer;
    // 現在のターンのPlayerの情報
    public Player CurrentTurnPlayer { get { return currentTurnPlayer; } }

    // 現在のターンとは逆のプレイヤーの情報
    public Player OppositeTurnPlayer
    {
        get
        {
            if (isMyTurn())
            {
                return opponentPlayer;
            }
            else
            {
                return myPlayer;
            }
        }
    }

    // 対戦相手となるPlayerの情報を取得できるようにする
	public Player OppositePlayer(Player player)
	{
		if(player == myPlayer)
		{
			return opponentPlayer;
		}
		else
		{
			return myPlayer;
		}
	}


	// 最初のPlayerのターンを抽選する
	public void LotFirstPlayerTurn()
    {
        if (Random.value < 0.5f)
        {
            currentTurnPlayer = myPlayer;
        }
        else
        {
            currentTurnPlayer = opponentPlayer;
        }
    }

    public void SpawnPieaces(List<Square> allGridSquares)
	{
		myPlayer.SpawnPieaces(allGridSquares, false);
		opponentPlayer.SpawnPieaces(allGridSquares, true);
	}


	public bool isMyTurn()
    {
        return currentTurnPlayer == myPlayer;
    }

    public void Judge()
	{
		// 自分の持ち駒がなくなった
		if (myPlayer.CurrentPieces.Count <= 0)
		{
			// 負け
			SetResu(Resut.Lose);
			return;
		}
		// 相手の持ち駒がなくなった
		if (opponentPlayer.CurrentPieces.Count <= 0)
		{
			// 勝ち
			SetResu(Resut.Win);
			return;
		}
        // 自分のコマが相手の陣地にたどり着いた
		foreach (Piece myPiece in myPlayer.CurrentPieces)
		{
            if (opponentPlayer.CheckTerritoryPosition(myPiece.CurrentPosition.x, myPiece.CurrentPosition.y))
			{
				// 勝ち
				SetResu(Resut.Win);
				return;
			}
		}
		// 相手のコマが自分の陣地にたどり着いた
		foreach (Piece opponentPiece in opponentPlayer.CurrentPieces)
		{
			if (myPlayer.CheckTerritoryPosition(opponentPiece.CurrentPosition.x, opponentPiece.CurrentPosition.y))
			{
				// 負け
				SetResu(Resut.Lose);
				return;
			}
		}
		this.TurnChange();
		SePlayManager.PlaySeSound(SePlayManager.SE.walking_se);
	}

    private void TurnChange()
    {
        if (isMyTurn())
        {
            currentTurnPlayer = opponentPlayer;
        }
        else
        {
            currentTurnPlayer = myPlayer;
        }
    }

	public Resut resut
	{ get { return _resut;  } }


	public void SetResu(Resut re)
	{
		if(SoundManager.Instance)
		SoundManager.Instance.StopBgm();

		_resut = re;
		StartCoroutine("Go");
	}

	

	private IEnumerator Go()
	{
		yield return new WaitForSeconds(1.0f);

		SceneManager.LoadScene("result");

	}
}
