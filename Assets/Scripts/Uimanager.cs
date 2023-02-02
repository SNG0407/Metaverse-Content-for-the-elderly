using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Uimanager : MonoBehaviour
{

    public GameObject aliveFriend1;  
    public GameObject aliveFriend2;  

    public GameObject GhostFriend1;  
    public GameObject GhostFriend2;  

    public GameObject PetUI;  

    public ParticleSystem ps; 
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
         SceneManager.LoadScene("MainPark");
    }
    public void FriendsBtn()
    {
         Debug.Log("LoadScene");
         SceneManager.LoadScene("FriendPlace");
    }
    //FamilyPlace
      public void FamilyBtn()
    {
         Debug.Log("LoadScene");
         SceneManager.LoadScene("FamilyPlace");
    }

    public void aliveBtn()
    {
        aliveFriend1.SetActive(true);
        aliveFriend2.SetActive(true);

        GhostFriend1.SetActive(false);
        GhostFriend2.SetActive(false);

        ps.gameObject.SetActive(true);
        ps.Play();
    }
    public void GoBackBtn()
    {
        SceneManager.LoadScene("MainPark");
    }

    public void petBtn()
    {
        PetUI.SetActive(true);
    }
}
