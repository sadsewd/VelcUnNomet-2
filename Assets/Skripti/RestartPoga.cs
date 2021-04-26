using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Importē lai varētu strādāt ar ainām
using UnityEngine.SceneManagement;
public class RestartPoga : MonoBehaviour
{
    public void AtsaktSpeli()
    {
        //Tiek ielādēta spēles aina
        SceneManager.LoadScene("PilsetasAina");
    }
}
