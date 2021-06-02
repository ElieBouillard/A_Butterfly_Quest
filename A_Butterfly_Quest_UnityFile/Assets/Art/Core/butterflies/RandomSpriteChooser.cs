using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteChooser : MonoBehaviour
{
    public string textureName = "_MainTexture";
    public List<Texture> butterfliesTextures = new List<Texture>();

    void Start()
    {
        PickRandomButterflyTexture();
    }

    void PickRandomButterflyTexture()
    {
        GetComponent<Renderer>().material.SetTexture(textureName, butterfliesTextures[Random.Range(0, butterfliesTextures.Count)]);
    }

    void Update()
    {
        
    }
}
