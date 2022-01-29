using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 普通のマス目
public class Square : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public BoardPosition boardPosition { private set; get; }
    private Action<Square> OnClick = null;

    public void Initialize(int positionX, int positionY, Action<Square> onClick)
    {
        this.boardPosition = new BoardPosition(positionX, positionY);
        this.OnClick = onClick;
        this.GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<UnityEngine.UI.Image>().color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClick != null)
        {
            OnClick(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
