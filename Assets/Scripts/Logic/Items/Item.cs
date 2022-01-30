using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemTypes itemTypes;

	public ItemTypes CurrentItemTypes { get { return itemTypes; } }
}
