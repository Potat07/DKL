using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Fight : MonoBehaviour
{

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.name == "Player") 
        {
            SceneManager.LoadScene(0);
        }

    }

} 
