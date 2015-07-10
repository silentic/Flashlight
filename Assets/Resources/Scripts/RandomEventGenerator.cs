using UnityEngine;
using System.Collections;

public class RandomEventGenerator : MonoBehaviour {

    int eventStatusCount;

    [Header("[Ghost On The Screen]")]
    public GameObject ghostOnTheScreen;
    public float ghostScreenProb;
    public float ghostScreenDuration;

	// Use this for initialization
	void Start () {
        InvokeRepeating("ActiveGhostOnTheScreen", 5, 1);
	}

    void ActiveGhostOnTheScreen()
    {
        float num = Random.Range(0.0f, 100);
        //Debug.Log ("TestDebug " + num);

        if (num < ghostScreenProb && EventActiveCondition())
        {		// random light off with XX% probability 
            eventStatusCount++;
            StartCoroutine(GhostAppearOnTheScreen());
        }
    }

    IEnumerator GhostAppearOnTheScreen() {
        ghostOnTheScreen.SetActive(true);
        yield return new WaitForSeconds(ghostScreenDuration);
        ghostOnTheScreen.SetActive(false);
        eventStatusCount--;
    }

    bool EventActiveCondition()
    {
        return eventStatusCount == 0;
    }
}
