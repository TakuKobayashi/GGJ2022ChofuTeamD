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

    public bool isMyTurn()
    {
        return currentTurnPlayer == myPlayer;
    }

    public void TurnChange()
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
