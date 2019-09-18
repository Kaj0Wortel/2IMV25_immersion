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
		_Render = GetComponent<Renderer>();
        Texture2D texture = Resources.Load<Texture2D>("Textures/potato");
        setTexture(texture);
    }
    
    public void setTexture(Texture tex)
	{
        _Render.material.mainTexture = tex;//_Render.material.SetTexture("MainTex", tex);
    }
	
	/**
	 *  Update is called once per frame.
	 */
	void Update()
	{
		
	}
	
	
}
