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
	/** Array containing all main target objects to change the texture of. */
	private static GameObject[] _MainTargetObjects;
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
	/** The tag used to identify the main symbol objects to alter. */
	public string mainObjectTag = "MainSymbol";
	/** The tag used to identify the symbol objects to alter. */
	public string objectTag = "Symbol";
	/** The textures to use. */
	public string[] textureStrings = new string[] {
		"Letters/a", "Letters/k", "Letters/n", "Letters/m", "Letters/v",
		"Letters/w", "Letters/x", "Letters/y", "Letters/z"
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
		INIT, SHOW, SET, CLEAR
	};


    /* --------------------------------------------------------------------------------
     * Functions.
     * --------------------------------------------------------------------------------
     */
	/**
	 * Initializes the values of the script.
	 */
	void Start() {
		// Load textures.
		_Empty = Resources.Load<Texture2D>("Textures/empty");
		_Textures = new Texture2D[textureStrings.Length];
		for (int i = 0; i < _Textures.Length; i++) {
			_Textures[i] = Resources.Load<Texture2D>("Textures/" + textureStrings[i]);
			if (_Textures[i] == null) {
				Debug.LogError("Unable to load texture '" + textureStrings[i] + "'.");
			}
		}

		// Load game objects.
		_MainTargetObjects = GameObject.FindGameObjectsWithTag(mainObjectTag);
		GameObject[] targets = GameObject.FindGameObjectsWithTag(objectTag);
		_TargetObjects = new GameObject[_MainTargetObjects.Length + targets.Length];
		for (int i = 0; i < _TargetObjects.Length; i++) {
			if (i < targets.Length) {
				_TargetObjects[i] = targets[i];
			} else {
				_TargetObjects[i] = _MainTargetObjects[i - targets.Length];
			}
		}

        // Set empty texture by default.
        for (int i = 0; i < _TargetObjects.Length; i++) {
            setTex(_Empty, i, true);
        }
	}
	
	/**
	 * Updates the textures of the objects with the tag accordingly.
	 */
	void Update() {
        if (nextState() == State.SET) {
            if (Input.GetButtonDown("Next")) setRandomTextures();

        } else if (nextState() == State.INIT) {
            if (Input.GetButtonDown("Next")) {
                init();
                show();
            }

        } else if (nextState() == State.SHOW) {
            show();

        } else if (nextState() == State.CLEAR) {
            bool found = Input.GetButtonDown("Found");
            if (found || Input.GetButtonDown("Missing")) {
                float dt = Time.time - _Time;
                bool exists = targetExists();
                if (found == exists) {
                    if (!exists || isTargetVisible()) {
                        Debug.Log("The user was right! (time = " + dt + "s)");
                    } else {
                        Debug.Log("The user was right, but the target wasn't visible! (time = " + dt + "s)");
                    }

                } else {
                    Debug.Log("The user was wrong! (time = " + dt + "s)");
                }

                clear();
            }
        }
            /*
        } else if (Input.GetButtonDown("Found") || Input.GetButtonDown("Missing")) {
			if (nextState() == State.CLEAR) {
				bool found = Input.GetButtonDown("Found");
				float dt = Time.time - _Time;
				if (found == targetExists()) {
					if (!targetExists() || isTargetVisible()) {
						Debug.Log("The user was right! (time = " + dt + "s)");
					} else {
						Debug.Log("The user was right, but the target wasn't visible! (time = " + dt + "s)");
					}
				} else {
					Debug.Log("The user was wrong! (time = " + dt + "s)");
				}

				clear();
			}
		}*/
	}

	/**
	 * @return The next state in chronological order (i.e. CLEAR -> INIT -> SHOW -> SET -> CLEAR).
	 */
	private State nextState() {
		switch(_State) {
			case State.INIT:
				return State.SHOW;
			case State.SHOW:
				return State.SET;
			case State.SET:
				return State.CLEAR;
			case State.CLEAR:
				return State.INIT;
			default:
				return State.CLEAR;
		}
	}

	/**
	 * Sets the texture of the component with the given index.
	 * 
	 * @param tex The new texture of the object.
	 * @param i The index of the game object to set the texture of. Must be a valid index.
	 * @param all Whether to set the index relative to all objects, or only the main objects.
	 */
	private void setTex(Texture2D tex, int i, bool all) {
		SetTextures st = getSetTextures(all ? _TargetObjects[i] : _MainTargetObjects[i]);
		if (st != null) {
			st.setTexture(tex);
		}
	}

	/**
	 * @return The {@code SetTextures} script of the game object, or {@code null} if it doesn't have any.
	 */
	private SetTextures getSetTextures(GameObject obj) {
		SetTextures st = obj.GetComponent<SetTextures>();
		if (st == null) {
			Debug.LogError("Expected the game object with label '" + objectTag
					+ "' to have the script 'SetTextures', but '" + obj
					+ "' didn't have this.");
		}
		return st;
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
	 * Shows the symbol to look for at the main target objects.
	 */
	public void show() {
		for (int i = 0; i < _MainTargetObjects.Length; i++) {
			setTex(getTargetSymbol(), i, false);
		}
		_State = State.SHOW;
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

			setTex(tex, i, true);
		}
		_Time = Time.time;
		_State = State.SET;
	}

	/**
	 * Clears the textures and the internal state.
	 */
	public void clear() {
		_TargetObjectIndex = -1;
		_TargetSymbolIndex = -1;
		for (int i = 0; i < _TargetObjects.Length; i++) {
			setTex(_Empty, i, true);
		}
		_State = State.CLEAR;
	}

	/**
	 * @return The target object to search for.
	 */
	public GameObject getTargetObject() {
		if (_TargetObjectIndex == -1) return null;
		else if (_TargetObjectIndex < _TargetObjects.Length) return _TargetObjects[_TargetObjectIndex];
		else return _MainTargetObjects[_TargetObjectIndex - _TargetObjects.Length];
	}

	/**
	 * @return {@code true} if the target to search for exists. {@code false} otherwise.
	 */
	public bool targetExists() {
		return (_TargetObjectIndex != -1);
	}

	/**
	 * @return The texture of the symbol to search for.
	 */
	public Texture2D getTargetSymbol() {
		if (_TargetSymbolIndex == -1) return null;
		else return _Textures[_TargetSymbolIndex];
	}

	/**
	 * Checks if the target is visible.
	 */
	public bool isTargetVisible() {
		GameObject obj = getTargetObject();
		if (obj == null) return false;
		SetTextures st = getSetTextures(obj);
		if (st == null) return false;
		return st.isVisible();
	}


}
