using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Creates a key listener which changes the texture of the component at random.
 * 
 * @author Kaj Wortel
 */
public class TexKeyListener : MonoBehaviour {

    /* --------------------------------------------------------------------------------
     * Constants.
     * --------------------------------------------------------------------------------
     */
	/** Array containing all target objects to change the texture of. */
	private static GameObject[] _TargetObjects;
	/** The empty texture. */
	private static Texture2D _Empty;
	/** Array containing all textures to change to. */
	private static Texture2D[] _Textures;


    /* --------------------------------------------------------------------------------
     * Variables.
     * --------------------------------------------------------------------------------
     */
	/** The tag to modify the objects of. */
	public string objectTag = "Symbol";
	/** The textures to use. */
	public string[] textureStrings = new string[] {
		"potato", "croissant", "smile"
	};

	/** Denotes the index of the symbol to search for. */
	public int _TargetSymbolIndex = -1;
	/** Denotes the index of the current target symbol, or {@code -1} if it doesn't exist. */
	public int _TargetObjectIndex = -1;
	/** The state of the script. */
	public State _State = State.CLEAR;
	/** The time needed for the search so far. */
	public float _Time = 0f;


    /* --------------------------------------------------------------------------------
     * Inner classes.
     * --------------------------------------------------------------------------------
     */
	/**
	 * Enum denoting the state of the script.
	 */
	public enum State {
		INIT, SET, CLEAR
	};


    /* --------------------------------------------------------------------------------
     * Functions.
     * --------------------------------------------------------------------------------
     */
	/**
	 * Initializes the values of the script.
	 */
	void Start() {
		_TargetObjects = GameObject.FindGameObjectsWithTag(objectTag);
		_Empty = Resources.Load<Texture2D>("Textures/empty");
		_Textures = new Texture2D[textureStrings.Length];
		for (int i = 0; i < _Textures.Length; i++) {
			_Textures[i] = Resources.Load<Texture2D>("Textures/" + textureStrings[i]);
			if (_Textures[i] == null) {
				Debug.LogError("Unable to load texture '" + textureStrings[i] + "'.");
			}
		}
	}
	
	/**
	 * Updates the textures of the objects with the tag accordingly.
	 */
	void Update() {
		if (Input.GetKeyDown("space")) {
			if (_State == State.INIT) setRandomTextures();
			else if (_State == State.CLEAR) init();

		} else if (Input.GetKeyDown("y") || Input.GetKeyDown("n")) {
			if (_State == State.SET) {
				bool found = Input.GetKeyDown("y");
				float dt = Time.time - _Time;
				if (found == tagetExists()) {
					Debug.Log("The user was right! (time = " + dt + "s)");
				} else {
					Debug.Log("The user was wrong! (time = " + dt + "s)");
				}

				clear();
			}
		}
	}

	/**
	 * Sets the texture of the component with the given index.
	 * 
	 * @param tex The new texture of the object.
	 * @param i The index of the game object to set the texture of. Must be a valid index.
	 */
	private void setTex(Texture2D tex, int i) {
		SetTextures st = _TargetObjects[i].GetComponent<SetTextures>();
		if (st == null) {
			Debug.LogError("Expected the game object with label '" + objectTag
					+ "' to have the script 'SetTextures', but '" + _TargetObjects[i]
					+ "' didn't have this.");
			
		} else {
			st.setTexture(tex);
		}
	}

	/**
	 * Initializes the internal state.
	 */
	public void init() {
		_TargetSymbolIndex = Random.Range(0, _Textures.Length);
		_TargetObjectIndex = (Random.value > 0.5f
				? Random.Range(0, _TargetObjects.Length)
				: -1);
		_State = State.INIT;
	}

	/**
	 * Randomly selects one of the remaining textures for each game object,
	 * except for the object to search for, if any. Additionally starts
	 * the timer.
	 */
	public void setRandomTextures() {
		for (int i = 0; i < _TargetObjects.Length; i++) {
			Texture2D tex;
			if (i == _TargetObjectIndex) {
				tex = _Textures[_TargetSymbolIndex];

			} else {
				int symbolIndex = Random.Range(0, _Textures.Length - 1);
				if (symbolIndex >= _TargetSymbolIndex) {
					symbolIndex++;
				}
				tex = _Textures[symbolIndex];
			}

			setTex(tex, i);
		}
		_State = State.SET;
		_Time = Time.time;
	}

	/**
	 * Clears the textures and the internal state.
	 */
	public void clear() {
		_TargetObjectIndex = -1;
		_TargetSymbolIndex = -1;
		for (int i = 0; i < _TargetObjects.Length; i++) {
			setTex(_Empty, i);
		}
		_State = State.CLEAR;
	}

	/**
	 * @return The target object to search for.
	 */
	public GameObject getTargetObject() {
		if (_TargetObjectIndex == -1) return null;
		else return _TargetObjects[_TargetObjectIndex];
	}

	/**
	 * @return {@code true} if the target to search for exists. {@code false} otherwise.
	 */
	public bool tagetExists() {
		return (_TargetObjectIndex != -1);
	}

	/**
	 * @return The texture of the symbol to search for.
	 */
	public Texture2D getTargetSymbol() {
		if (_TargetSymbolIndex == -1) return null;
		else return _Textures[_TargetSymbolIndex];
	}


}
