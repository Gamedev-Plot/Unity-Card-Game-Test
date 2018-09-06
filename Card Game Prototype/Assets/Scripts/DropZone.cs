using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
     public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("On Pointer Enter");

        if (eventData.pointerDrag == null)
        {
            return;
        }

        Draggable card = eventData.pointerDrag.GetComponent<Draggable>();
        if (card != null)
        {
            card.placeHolderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("On Pointer Exit");

        if (eventData.pointerDrag == null)
        {
            return;
        }

        Draggable card = eventData.pointerDrag.GetComponent<Draggable>();
        if (card != null && card.placeHolderParent == this.transform)
        {
            card.placeHolderParent = card.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerDrag.name + " was dropped on to " + gameObject.name);

        Draggable card = eventData.pointerDrag.GetComponent<Draggable>();
        if (card != null)
        {
            card.parentToReturnTo = this.transform;
        }
    }
}
