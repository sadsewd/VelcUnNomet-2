using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropSkripts : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler/*, IEndDragHandler */
{
    //Uzglabās norādi uz Objektu skripts
    //public Objekti objektuSkripts;
    //Velkamam objektam piestiprinātā komponente
    private CanvasGroup kanvasGrupa;
    //Vilktā objekta atrašanās vietas koordinātu maiņai
    private RectTransform velkObjektTransf;

    void Awake()
    {
        //Piekļūst objektam piesaistītajai CanvasGroup komponentei
        kanvasGrupa = GetComponent<CanvasGroup>();
        //Piekļūst objekta RectTransform komponentei
        velkObjektTransf = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData notikums)
    {
        Debug.Log("Uzklikšķināts uz velkamā objekta!");
    }

    public void OnBeginDrag(PointerEventData notikums)
    {
        Debug.Log("Uzsākta vilkšana");
        //Attīra pēdejo vilkto objektu
        //objektuSkripts.pedejaisVilktais = null;
        kanvasGrupa.alpha = 0.6f;
        //Lai objektam varētu iet cauri RayCast stars
        kanvasGrupa.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData notikums)
    {
        Debug.Log("Notiek vilkšana!");
        //velkObjektTransf.anchoredPosition += notikums.delta / objektuSkripts.kanva.scaleFactor;
    }
}
