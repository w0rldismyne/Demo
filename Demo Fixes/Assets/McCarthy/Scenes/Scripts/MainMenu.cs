using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lucerna.Utils;

public class MainMenu : MonoBehaviour
{
   private void Start() {
      Time.timeScale = 1;
   }
   
   public void PlayGame()
   { 
      PlayerPrefs.SetInt("LoadSlot", -1);
      try { SceneLoader.instance.LoadSceneAsync("Demo (with Fungus)"); }
      catch { 
         Debug.LogWarning("MainMenu::PlayGame() --- Scene Loader not found! Using instant transition instead."); 
         SceneManager.LoadScene("Demo (with Fungus)"); 
      }
   }

 public void QuitGame ()
    {
        Debug.Log("Quit!");
        Application.Quit(); }

   public void MenuOGame ()
   {
      try { SceneLoader.instance.LoadSceneAsync("MenuO"); }
      catch { 
         Debug.LogWarning("MainMenu::MenuOGame() --- Scene Loader not found! Using instant transition instead."); 
         SceneManager.LoadScene("MenuO"); 
      }
      
   }
   public void StartMenu () { SceneManager.LoadScene("StartMenu"); }

   public void GoToScene(string sceneName) {
      try { SceneLoader.instance.LoadSceneAsync(sceneName); }
      catch { 
         Debug.LogWarning("MainMenu::GoToScene() --- Scene Loader not found! Using instant transition instead."); 
         SceneManager.LoadScene(sceneName); 
      }
   }
}



