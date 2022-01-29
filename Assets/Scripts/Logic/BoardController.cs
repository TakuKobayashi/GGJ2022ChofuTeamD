using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    // 盤面の横幅のマス数(とりあえず8マス)
    [SerializeField] private int boardWidth;
    // 盤面の縦幅のマス数(とりあえず10マス)
    [SerializeField] private int boardHeight;

    [SerializeField] private GridLayoutGroup boardGridLayoutGroup;
    [SerializeField] private GameObject gridSquareObject;
    [SerializeField] private GameObject territoryGridSquareObject;

    // 自分の手番
    [SerializeField] private Player myPlayer;
	// 相手の手番
	[SerializeField] private Player opponentPlayer;

    // ターンは交互にやってくるので今のターンのPlayerの情報を保持する
    private Player currentTurnPlayer;

    private List<Square> allGridSquares = new List<Square>();

    // 最初のPlayerのターンを抽選する
    private void LotFirstPlayerTurn()
    {
        if(Random.value < 0.5f)
        {
            currentTurnPlayer = myPlayer;
        }
        else
        {
            currentTurnPlayer = opponentPlayer;
        }
    }

    private void GenerateBoardGrid()
    {
        for(int i = 0;i < boardHeight; ++i)
        {
            for (int j = 0; j < boardWidth; ++j)
            {
                Square gridSquare;
                if (myPlayer.CheckTerritoryPosition(j, i) || opponentPlayer.CheckTerritoryPosition(j, i))
                {
                    gridSquare = ComponentUtil.InstantiateTo<Square>(boardGridLayoutGroup.gameObject, territoryGridSquareObject);
                }
                else
                {
                    gridSquare = ComponentUtil.InstantiateTo<Square>(boardGridLayoutGroup.gameObject, gridSquareObject);
                }
                gridSquare.Initialize(j, i, OnSquareClicked);
                allGridSquares.Add(gridSquare);
            }
        }
    }

    // マス目を全て初期状態にする
	private void ChangeAllSquareNormalState()
	{
		GameController.Instance.SelectingSquare = null;
		foreach (Square movableSquare in allGridSquares)
		{
			movableSquare.ChangeStateWithGraphic(SquareState.Normal);
		}
	}

	private void OnSquareClicked(Square clickedSquare)
    {
        // 移動させようとした駒を選択したら、選択解除してクリアにする
        if(clickedSquare.CurrentState == SquareState.Selecting)
		{
			this.ChangeAllSquareNormalState();
			return;
		}
        if(GameController.Instance.SelectingSquare == null)
		{
            Piece clickedPiece = currentTurnPlayer.CurrentPieces.Find(p => p.CurrentPosition.x == clickedSquare.boardPosition.x && p.CurrentPosition.y == clickedSquare.boardPosition.y);
			if (clickedPiece != null)
			{
				clickedSquare.ChangeStateWithGraphic(SquareState.Selecting);
                GameController.Instance.SelectingSquare = clickedSquare;
				List<Square> movableSquares = clickedPiece.FilterMovableSquares(allGridSquares, currentTurnPlayer.CurrentPieces);
				foreach (Square movableSquare in movableSquares)
				{
					movableSquare.ChangeStateWithGraphic(SquareState.Movable);
				}
			}
		}
		else
		{
            if(clickedSquare.CurrentState == SquareState.Movable || clickedSquare.CurrentState == SquareState.MovableHovering)
            {
                BoardPosition selectingSquareBoardPosition = GameController.Instance.SelectingSquare.boardPosition;
                Piece movePiece = currentTurnPlayer.CurrentPieces.Find(p => p.CurrentPosition.x == selectingSquareBoardPosition.x && p.CurrentPosition.y == selectingSquareBoardPosition.y);
				movePiece.Move(clickedSquare);
				this.ChangeAllSquareNormalState();
                //TODO 勝利などの判定処理
                TurnChange();
                SePlayManager.PlaySeSound(SePlayManager.SE.walking_se);
            }
		}
	}

    private void TurnChange()
    {
        if(currentTurnPlayer == myPlayer)
        {
            currentTurnPlayer = opponentPlayer;
        }
        else
        {
            currentTurnPlayer = myPlayer;
        }
    }

    private void Awake()
    {
        GenerateBoardGrid();
		myPlayer.SpawnPieaces(allGridSquares);
		opponentPlayer.SpawnPieaces(allGridSquares);
		ChangeAllSquareNormalState();
        LotFirstPlayerTurn();
    }
}
