using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToMain : MonoBehaviour
{   

    [SerializeField] private int sceneIndex;

    private void OnTriggerEnter(Collider col){

        if(col.gameObject.GetComponent<ImPlayer>() != null){

            StartCoroutine(SwitchTimer());

        }

    }

    private IEnumerator SwitchTimer(){

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);

    }
}
