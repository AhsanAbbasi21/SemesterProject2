using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scroll_Speed = 0.1f; // Speed of scrolling
    private MeshRenderer mesh_Renderer; // Renderer to manipulate the material's texture
    private float y_Scroll; // Tracks scrolling on the Y-axis

    void Awake()
    {
        mesh_Renderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Scroll(); // Call the Scroll function every frame
    }

    void Scroll()
    {
        // Scroll on the Y-axis instead of the X-axis
        y_Scroll = Time.time * scroll_Speed;
        Vector2 offset = new Vector2(0f, y_Scroll); // Set the Y-axis offset
        mesh_Renderer.sharedMaterial.SetTextureOffset("_MainTex", offset); // Apply the offset to the texture
    }
}
