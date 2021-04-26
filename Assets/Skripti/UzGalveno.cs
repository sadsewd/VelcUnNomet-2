using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UzGalveno : MonoBehaviour
{
    public void Izvelne()
    {
        //Tiek ieladeta izvelnes aina
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
