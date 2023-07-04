using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InputHandle : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler
{
    Vector2 pos1;
    Vector2 pos2;
    bool onclick= false;
    bool cul = false;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log( "position : " + eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !onclick)
        {
            pos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("Click Pos1 : " + pos1);
            onclick = true;
        }

        if (Input.GetMouseButtonUp(0) && onclick)
        {
            pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("Click Pos2 : " + pos2);
            onclick = false;
            cul = true;
        }

        if (cul)
        {
            float x = pos2.x - pos1.x;
            float y = pos2.y - pos1.y;
            /*Debug.Log(Mathf.Abs(pos2.x) > Mathf.Abs(pos1.x) ? "tren" : "duoi");
            Debug.Log(Mathf.Abs(pos1.y) > Mathf.Abs(pos2.y) ? "phai" : "trai");*/
            //Debug.Log(pos2.y>pos1.y ? "tren" : "duoi");
            //Debug.Log(pos2.x>pos1.x ? "phai" : "trai");

            // UP-Right
            if (pos2.y >= pos1.y && pos2.x >= pos1.x )
            {
                if((pos2.x - pos1.x) > (pos2.y - pos1.y)) Debug.Log("SANG PHAI");
                else Debug.Log("LEN TREN");
            }
            
            //DOWN-Left
            if (pos2.y <= pos1.y && pos2.x <= pos1.x)
            {

                if ((pos2.x - pos1.x) < (pos2.y - pos1.y)) Debug.Log("SANG TRAI");
                else Debug.Log("XUONG DUOI");
            }

            //UP-Left
            if (pos2.y >= pos1.y && pos2.x <= pos1.x)
            {

                if (Mathf.Abs((pos2.x - pos1.x)) > Mathf.Abs((pos2.y - pos1.y))) Debug.Log("SANG TRAI");
                else Debug.Log("LEN TREN");
            }

            //DOWN-Right
            if (pos2.y <= pos1.y && pos2.x >= pos1.x)
            {

                if (Mathf.Abs((pos2.x - pos1.x)) > Mathf.Abs((pos2.y - pos1.y))) Debug.Log("SANG PHAI");
                else Debug.Log("XUONG DUOI");
            }

            //Debug.Log("X :" + x);
            //Debug.Log("Y :" + y);
            cul = false;
        }
    }
}
