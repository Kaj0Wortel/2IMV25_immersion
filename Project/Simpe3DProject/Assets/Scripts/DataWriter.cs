using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataWriter : MonoBehaviour {

    /** The system dependant file separator. */
    private static readonly char _FS = Path.DirectorySeparatorChar;
	/** The folder used to write the data in. */
    private static readonly string _PATH = "Assets" + _FS+ "Results" + _FS;

    /** The writer used to write the data. */
    private StreamWriter sw;
    
    /** The file to write the data to. */
    //public string fileName = "results.txt";

    public Type type = Type.IN_PLACE;

    public enum Type {
        IN_PLACE, MOVING, TEST
    }

    /**
     * Sets up the writer.
     */
    void Start() {
        if (type == Type.TEST) return;
        sw = new StreamWriter(_PATH + (type == Type.IN_PLACE
            ? "results_in_place.txt"
            : "results_moving.txt"), true);
        sw.WriteLine("Start test @" + DateTime.Now + ";" + type.ToString());
    }

    /**
     * Prints a line to the file.
     */
    public void WriteLine(string line) {
        if (type == Type.TEST) return;
        sw.WriteLine(line);
    }

    /**
     * Writes the end message and closes the stream.
     */
    public void OnApplicationQuit() {
        if (type == Type.TEST) return;
        sw.WriteLine("End test @" + DateTime.Now + ";" + type.ToString());
        sw.Close();
    }


}
