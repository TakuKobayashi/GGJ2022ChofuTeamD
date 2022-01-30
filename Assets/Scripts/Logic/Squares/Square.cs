using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 普通のマス目
public class Square : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public BoardPosition boardPosition { private set; get; }
    private Action<Square> OnClick = null;
    // 現在選択中などマスのStateが変わることがあるのでそれに合わせた管理をする
    public SquareState CurrentState { private set; get; }

    private Item droppingItem = null;
    private Color defaultImageColor;

    private void Awake()
    {
        this.defaultImageColor = this.GetComponent<UnityEngine.UI.Image>().color;
    }

    public void DropItem(Item item)
    {
        this.droppingItem = ComponentUtil.InstantiateTo<Item>(this.gameObject, item.gameObject);
    }

	public void CaptureItem(Piece piece)
	{
        if(droppingItem != null)
		{
			piece.EquipItem(droppingItem);
			this.droppingItem = null;
		}
	}

	public void Initialize(int positionX, int positionY, Action<Square> onClick)
    {
        this.CurrentState = SquareState.Normal;
        this.boardPosition = new BoardPosition(positionX, positionY);
        this.OnClick = onClick;
        this.ShowStateGraphic();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.CurrentState == SquareState.Normal)
        {
            this.ChangeStateWithGraphic(SquareState.Hovering);
        }
        else if(this.CurrentState == SquareState.Movable)
        {
            this.ChangeStateWithGraphic(SquareState.MovableHovering);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.CurrentState == SquareState.Hovering)
        {
            this.ChangeStateWithGraphic(SquareState.Normal);
        }
        else if (this.CurrentState == SquareState.MovableHovering)
        {
            this.ChangeStateWithGraphic(SquareState.Movable);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClick != null)
        {
            OnClick(this);
        }
    }

    public void ChangeStateWithGraphic(SquareState squareState)
    {
        this.CurrentState = squareState;
        this.ShowStateGraphic();
    }

    private void ShowStateGraphic()
    {
        if(this.CurrentState == SquareState.Normal)
        {
            this.GetComponent<UnityEngine.UI.Image>().color = defaultImageColor;
        }
        else if (this.CurrentState == SquareState.Hovering)
        {
            this.GetComponent<UnityEngine.UI.Image>().color = Color.gray;
        }
        else if (this.CurrentState == SquareState.Selecting)
        {
            this.GetComponent<UnityEngine.UI.Image>().color = Color.green;
        }
        else if (this.CurrentState == SquareState.Movable)
        {
            this.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }
        else if (this.CurrentState == SquareState.MovableHovering)
        {
            this.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
        }
    }
}
