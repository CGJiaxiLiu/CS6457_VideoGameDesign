
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private string Level2Load;
    public void FadeOut(string LevelName)
    {
        Level2Load = LevelName;
        animator.SetTrigger("fade");        
    }
    public void OnFadeComplete()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(Level2Load);       
    }
    
}
