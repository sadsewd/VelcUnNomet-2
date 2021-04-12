using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Jāimportē, lai varētu piesaistīt IDropHandler interfeisu un lietot OnDrop funkciju
using UnityEngine.EventSystems;

public class NomesanasVieta : MonoBehaviour, IDropHandler
{
    //Uzglabās velkamā objekta rotāciju ap Z asi un noliekamās vietas rotāciju
    //Starpība uzglabās, cik liela Z ass rotācijas leņķa starpība starp abiem objektiem
    private float vietaZrot, velkamaObjeZrot, rotacijasStarpiba;
    //Uzglabās velkamā objekta un nomešanas vietas izmerus
    private Vector2 vietasIzm, velkObjIzm;
    //Uzglabās objektu x un y ass izmēru starpību
    private float xIzmeruStarpiba, yIzmeruStarpiba;
    //Norādu uz skriptu Objekti
    public Objekti objektuSkripts;

    //Norāde, ja objektu cenšas nomest uz nometamā lauka
    public void OnDrop(PointerEventData notikums)
    {
        //Pārbauda vai kāds objekts tiek vilkts un nomests
        if(notikums.pointerDrag != null)
        {
            //Ja nomešanas laukā uzmestā attēla tags sakrīt ar lauka tagu
            if((notikums.pointerDrag.tag.Equals(tag)))
            {
                //Iegūst objektu rotacijas grādos
                vietaZrot = notikums.pointerDrag.GetComponent<RectTransform>().transform.eulerAngles.z;
                velkamaObjeZrot = GetComponent<RectTransform>().transform.eulerAngles.z;
                //Aprēķina rotācijas starpību
                rotacijasStarpiba = Mathf.Abs(vietaZrot - velkamaObjeZrot);
                //Iegūst objektu izmērus
                vietasIzm = notikums.pointerDrag.GetComponent<RectTransform>().localScale;
                velkObjIzm = GetComponent<RectTransform>().localScale;
                xIzmeruStarpiba = Mathf.Abs(vietasIzm.x - velkObjIzm.x);
                yIzmeruStarpiba = Mathf.Abs(vietasIzm.y - velkObjIzm.y);

                //Pārbauda vai objektu savstarpējā rotācija neatšķiras vairāk par 9 gradiem un vai x un y izmēri neatšķiras vairāk par 0.15
                if((rotacijasStarpiba <= 9 || (rotacijasStarpiba >= 351 && rotacijasStarpiba <= 360) && (xIzmeruStarpiba <= 0.15 && yIzmeruStarpiba <= 0.15)))
                {
                    objektuSkripts.vaiIstajaVieta = true;
                    //Nometamo objektu iecentrē nomešanas vietā
                    notikums.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                    //Pielāgo nomestā objekta rotāciju
                    notikums.pointerDrag.GetComponent<RectTransform>().localRotation = GetComponent<RectTransform>().localRotation;
                    //Pielāgo nomestā objekta izmēru
                    notikums.pointerDrag.GetComponent<RectTransform>().localScale = GetComponent<RectTransform>().localScale;

                    // Pārbauda pēc tagiem, kurš no objektiem ir pareizi nomests, tad atskaņo atbilstošo skaņu
                    switch (notikums.pointerDrag.tag)
                    {
                        case "Atkritumi": objektuSkripts.skanasAvots.PlayOneShot(objektuSkripts.skanaKoAtskanot[1]); break;
                        case "Atrie": objektuSkripts.skanasAvots.PlayOneShot(objektuSkripts.skanaKoAtskanot[2]); break;
                        case "Skola": objektuSkripts.skanasAvots.PlayOneShot(objektuSkripts.skanaKoAtskanot[3]); break;
                        default: Debug.Log("Nedefinēts tags!"); break;
                    }
                }
            }//Ja objektu tagi nesakrīt un nomet nepareizajā vietā
            else
            {
                objektuSkripts.vaiIstajaVieta = false;
                //Atskaņo skaņu
                objektuSkripts.skanasAvots.PlayOneShot(objektuSkripts.skanaKoAtskanot[0]);

                //Atkarībā no objektu taga, kurš tika vilkts, objektsuz tā sākotnējām koordinātām
                switch (notikums.pointerDrag.tag)
                {
                    case "Atkritumi": objektuSkripts.atkritumuMasina.GetComponent<RectTransform>().localPosition = objektuSkripts.atkrKoord; break;
                    case "Atrie": objektuSkripts.atroMasina.GetComponent<RectTransform>().localPosition = objektuSkripts.atroKoord; break;
                    case "Skola": objektuSkripts.autobuss.GetComponent<RectTransform>().localPosition = objektuSkripts.bussKoord; break;
                    default: Debug.Log("Objektam nav noteikta pārvietošanas vieta!"); break;
                }
            }
        }
    }
}
