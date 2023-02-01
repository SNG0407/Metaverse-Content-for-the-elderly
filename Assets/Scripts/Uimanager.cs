using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Uimanager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
      public void Startbtn()
    {
         Debug.Log("LoadScene");
         SceneManager.LoadScene("Mainground");
    }
    public void FriendsBtn()
    {
         Debug.Log("LoadScene");
         SceneManager.LoadScene("FriendPlace");
    }
}
