using System.Collections;
using UnityEngine;

public class SpiderWebAction : ObjectAction
{
    public float immobilizeTime = 3.0f; 

    public override IEnumerator CoAction()
    {

        Debug.Log("Player is caught in the spider web!");


        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.enabled = false; 
        }

        yield return new WaitForSeconds(immobilizeTime);

        
        if (player != null)
        {
            player.enabled = true;
        }

        Debug.Log("Player is free from the spider web!");
    }
}