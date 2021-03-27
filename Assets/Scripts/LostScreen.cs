using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostScreen : MonoBehaviour
{
    public void StartAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
