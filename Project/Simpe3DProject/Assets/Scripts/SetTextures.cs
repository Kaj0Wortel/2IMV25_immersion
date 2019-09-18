using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * This class is used to set the texture of an object.
 * 
 * @author Kaj Wortel
 */
public class SetTextures : MonoBehaviour {
    /**----------------------------------------
     * Constants.
     * ----------------------------------------
     */
    private Renderer _Render;

    public GameObject target;
    

    /**----------------------------------------
     * Functions.
     * ----------------------------------------
     */
	/**
	 * Use this for initialization.
	 */
	void Start()
	{
        //_Render.material = new Material(Shader.Find("Shaders/Alpha-Diffuse.shader"));
        //_Render.material.shader = Shader.Find("Transparent/Diffuse");
		_Render = GetComponent<Renderer>();
		/*
        //Fetch the Renderer from the GameObject
        m_Renderer = GetComponent<Renderer> ();

        //Make sure to enable the Keywords
        m_Renderer.material.EnableKeyword ("_NORMALMAP");
        m_Renderer.material.EnableKeyword ("_METALLICGLOSSMAP");

        //Set the Texture you assign in the Inspector as the main texture (Or Albedo)
        m_Renderer.material.SetTexture("_MainTex", m_MainTexture);
        //Set the Normal map using the Texture you assign in the Inspector
        m_Renderer.material.SetTexture("_BumpMap", m_Normal);
        //Set the Metallic Texture as a Texture you assign in the Inspector
        m_Renderer.material.SetTexture ("_MetallicGlossMap", m_Metal);*/
        Texture2D texture = Resources.Load<Texture2D>("Textures/potato");
        setTexture(texture);
    }
    
    public void setTexture(Texture tex)
	{
        //_Render.material.enableKeyword("");
        _Render.material.mainTexture = tex;//_Render.material.SetTexture("MainTex", tex);
    }
	
	/**
	 *  Update is called once per frame.
	 */
	void Update()
	{
		
	}
	
	
}
