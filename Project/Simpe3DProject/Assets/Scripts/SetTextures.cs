using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * This class is used to set the texture of an object.
 * 
 * @author Kaj Wortel
 */
public class SetTextures : MonoBehaviour {
    /* --------------------------------------------------------------------------------
     * Constants.
     * --------------------------------------------------------------------------------
     */
     /** The render object of this game object. */
    private Renderer _Render;
    

    /* --------------------------------------------------------------------------------
     * Functions.
     * --------------------------------------------------------------------------------
     */
	/**
	 * Use this for initialization.
	 */
	void Start() {
		_Render = GetComponent<Renderer>();
    }
    
    /**
     * Sets the given texture to the object.
     */
    public void setTexture(Texture tex){
        _Render.material.mainTexture = tex;
    }

    /**
     * @return {@code true} if the object is visible.
     */
    public bool isVisible() {
        return _Render.isVisible;
    }
	
	
}
