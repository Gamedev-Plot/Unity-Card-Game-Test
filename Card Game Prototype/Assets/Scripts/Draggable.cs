using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo = null;
    public Transform placeHolderParent = null;

    [Range(0.0f, 1.0f)]
    public float placeholderAlpha = 0.5f;

    private GameObject placeHolder = null;

    private Vector2 offset;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("On Begin Drag");
        offset = new Vector2(this.transform.position.x - eventData.position.x, this.transform.position.y - eventData.position.y);

        CreatePlaceHolder();

        parentToReturnTo = this.transform.parent;
        placeHolderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("On Drag");
        this.transform.position = eventData.position + offset;

        MovePlaceHolder();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("On End Drag");
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy(placeHolder);
    }

    private void CreatePlaceHolder()
    {
        placeHolder = Instantiate(gameObject);
        placeHolder.transform.SetParent(this.transform.parent, false);
        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        placeHolder.GetComponent<CanvasGroup>().alpha = placeholderAlpha;
    }

    private void MovePlaceHolder()
    {
        if (placeHolder.transform.parent != placeHolderParent)
        {
            placeHolder.transform.SetParent(placeHolderParent);
        }

        int newSiblingIndex = placeHolderParent.childCount;

        for (int i = 0; i < placeHolderParent.childCount; i++)
        {
            if(this.transform.position.x < placeHolderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;

                if (placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }
                break;
            }
        }

        placeHolder.transform.SetSiblingIndex(newSiblingIndex);
    }
}
