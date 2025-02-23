﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Jāimportē, lai varētu strādāt ar EventSystems
using UnityEngine.EventSystems;

public class DragDropScript : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //Uzglabās norādi uz Objektu skripts
    public Objekti objektuSkripts;
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
        //Spēles sākuma tiek paslēpts uzvaras ekrāns un atsāksanas poga
        objektuSkripts.AtsaksanasPoga.SetActive(false);
        objektuSkripts.UzvarasLogs.SetActive(false);
    }

    //Nostrādā nospiežot peles klikšķi uz objekta
    public void OnPointerDown(PointerEventData notikums)
    {
        Debug.Log("Uzklikšķināts uz velkamā objekta!");
    }

    //Nostrādā uzsākot vilkšanu
    public void OnBeginDrag(PointerEventData notikums)
    {
        Debug.Log("Uzsākta vilkšana");
        //Attīra pēdējo vilkto objektu
        objektuSkripts.pedejaisVilktais = null;
        //Uzsākot vilkt objektu tas paliek nedaudz caurspīdīgs
        kanvasGrupa.alpha = 0.6f;
        //Lai objektam varētu iet cauri RayCast stars
        kanvasGrupa.blocksRaycasts = false;
    }

    //Nostrādā reāli notiekot vilkšanai
    public void OnDrag(PointerEventData notikums)
    {
        Debug.Log("Notiek vilkšana!");
        velkObjektTransf.anchoredPosition += notikums.delta / objektuSkripts.kanva.scaleFactor;
    }

    //Nostrādā, kad tiek beigta vilkšana
    public void OnEndDrag(PointerEventData notikums)
    {
        //Atlaižot objektu iestata to kā pēdējo vilkto
        objektuSkripts.pedejaisVilktais = notikums.pointerDrag;
        Debug.Log("Pēdējais vilktais objekts: "+ objektuSkripts.pedejaisVilktais);
        Debug.Log("Beigta vilkšana!");
        //Atlaižot objektu tas atkal paliek necaurspīdīgs
        kanvasGrupa.alpha = 1f;

        //Pārbauda vai objekts ir vienkārši nomests vai arī nomests pareizajā vietā
        if(objektuSkripts.vaiIstajaVieta == false)
        {
            //Ja nav nomests pareizajā vietā, tad atkal padara velkamu
            kanvasGrupa.blocksRaycasts = true;
        }
        else
        {
            //tiek uzskaitīts cik daudz pareizi noliktu objektu ir
            objektuSkripts.PareiziNolikti = objektuSkripts.PareiziNolikti + 1;
            Debug.Log(objektuSkripts.PareiziNolikti);
            //Ja objekts nolikts pareizajā vietā, izmērā, rotācijā, tad pēdējo vilkto attīra
            objektuSkripts.pedejaisVilktais = null;
            //tiek pārbaudīts vai ir visi objekti nolikti pareizajā vietā
            if (objektuSkripts.PareiziNolikti == 11)
            {
                //Tākā visi objekti ir nolikti pareizajā vietā tad tiek parādīts uzvaras logs kopā ar atsāksanas pogu
                objektuSkripts.UzvarasLogs.SetActive(true);
                objektuSkripts.AtsaksanasPoga.SetActive(true);
            }
        }
        //Ja viens objekts nomests pareizajā vietā, tad lai varētu turpināt pārvietot pārējos objektus
        objektuSkripts.vaiIstajaVieta = false;
    }
}
