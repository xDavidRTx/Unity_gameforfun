using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour {

    void OnTriggerEnter2D (Collider2D collision)
    {
        Debug.Log("Fim de Nivel");
        SceneManager.LoadScene("FCT Hill_2");
    }
}
