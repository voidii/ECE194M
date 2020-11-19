using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Pause_Menu;

    void Start()
    {
        Cursor.visible = true;
        Screen.lockCursor = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGame(){
        SceneManager.LoadScene(1);
    }
    public void Quit(){
        Application.Quit(); 
    }

    public void Continue(){
        //Pause_Menu.SetActive(false);
        Debug.Log("continue");
        SceneManager.LoadScene(1);
        //Time.timeScale = 1;
        
    }
}
