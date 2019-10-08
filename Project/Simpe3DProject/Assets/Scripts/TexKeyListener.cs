using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

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
	/** The static script object. */
	private static GameObject _StaticScriptObject;


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
    public Controller controller = Controller.DETECT;

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

    /**
     * Enum denoting the different type of controllers.
     */
    public enum Controller {
        KEYBOARD_ONLY, DETECT, OCULUS_RIFT, VIVE
    }


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
		
		_StaticScriptObject = GameObject.Find("StaticScriptObject");
	}

    /**
	 * Updates the textures of the objects with the tag accordingly.
	 */
    void Update() {
        checkController();
        if (nextState() == State.SET) {
            if (nextPressed()) setRandomTextures();

        } else if (nextState() == State.INIT) {
            if (nextPressed()) {
                init();
                show();
            }

        } else if (nextState() == State.SHOW) {
            show();

        } else if (nextState() == State.CLEAR) {
            bool found = foundPressed();
            if (found || missingPressed()) {
                float dt = Time.time - _Time;
                bool exists = targetExists();
                StringBuilder sb = new StringBuilder();
                sb.Append("expected=");
                sb.Append(exists);
                sb.Append(";answer=");
                sb.Append(found);
                sb.Append(";correct=");
                sb.Append(exists == found);
                sb.Append(";time=");
                sb.Append(dt);
                sb.Append(";visible=");
                if (targetExists()) {
                    sb.Append(isTargetVisible());
                } else {
                    sb.Append("ignore");
                }

				string result = sb.ToString();
				DataWriter dw = _StaticScriptObject.GetComponent<DataWriter>();
				if (dw != null) {
					dw.WriteLine(result);
				}
				Debug.Log(sb.ToString());

                clear();
            }
        }
	}

    /**
     * Checks if the current controller is still selected.
     * Note that this function does NOT detect if a controller is disconnected.
     * It WILL detect a controller change if, and only if, the value of
     * {@link #controller} has been reset to {@link Controller#DETECT}.
     */
    private void checkController() {
        if (controller == Controller.DETECT) {
            string[] data = Input.GetJoystickNames();
            for (int i = 0; i < data.Length; i++) {
                if (data[i].IndexOf("oculus", System.StringComparison.CurrentCultureIgnoreCase) != -1) {
                    controller = Controller.OCULUS_RIFT;
                    break;

                } else if (data[i].IndexOf("vive", System.StringComparison.CurrentCultureIgnoreCase) != -1) {
                    controller = Controller.VIVE;
                    break;
                }
            }
            if (controller == Controller.DETECT) {
                controller = Controller.KEYBOARD_ONLY;
            }
            Debug.Log("Controller set to '" + controller + "'.");
        }
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
    
    /**
     * @return {@code true} if the button for the {@code next} action has been pressed.
     *     {@code false otherwise.
     */
    private bool nextPressed() {
        return Input.GetButtonDown("Next") ||
                (controller == Controller.OCULUS_RIFT && Input.GetButtonDown("Next Oculus")) ||
                (controller == Controller.VIVE && Input.GetButtonDown("Next VIVE"));
    }

    /**
     * @return {@code true} if the button for the {@code found} action has been pressed.
     *     {@code false otherwise.
     */
    private bool foundPressed() {
        return Input.GetButtonDown("Found") ||
                (controller == Controller.OCULUS_RIFT && Input.GetButtonDown("Found Oculus")) ||
                (controller == Controller.VIVE && Input.GetButtonDown("Found VIVE"));
    }

    /**
     * @return {@code true} if the button for the {@code missing} action has been pressed.
     *     {@code false otherwise.
     */
    private bool missingPressed() {
        return Input.GetButtonDown("Missing") ||
                (controller == Controller.OCULUS_RIFT && Input.GetButtonDown("Missing Oculus")) ||
                (controller == Controller.VIVE && Input.GetButtonDown("Missing VIVE"));
    }


}
