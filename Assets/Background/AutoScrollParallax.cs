using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScrollParallax : MonoBehaviour
{
    public float additionalScrollSpeed;

    //ab array of all the backgrounds game objects
    [SerializeField] private GameObject[] backgrounds;

    //an array that corresponds to the backgrounds array, where it gives the scroll speed for each individual background
    [SerializeField] private float[] scrollSpeed;

    private void Update()
    {
        //loops through array of objects, making scrolling occur for each
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //gets the renderer for this item in the array
            Renderer rend = backgrounds[i].GetComponent<Renderer>();

            //calculates the scroll offset
            float offset = Time.time * (scrollSpeed[i] + additionalScrollSpeed);

            //offsets the texture of this item based on the offset calculated previously
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));

        }
    }
}
