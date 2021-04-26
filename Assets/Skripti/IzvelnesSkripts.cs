using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IzvelnesSkripts : MonoBehaviour
{
    public void UzsaktSpeli()
    {
        //Tiek ielādēta spēles aina
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    

}
