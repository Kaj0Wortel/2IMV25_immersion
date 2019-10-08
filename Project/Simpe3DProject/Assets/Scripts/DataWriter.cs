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
    public string fileName = "results.txt";


    /**
     * Sets up the writer.
     */
    void Start() {
        sw = new StreamWriter(_PATH + fileName, true);
        sw.WriteLine("Start test @" + DateTime.Now);
    }

    /**
     * Prints a line to the file.
     */
    public void WriteLine(string line) {
        sw.WriteLine(line);
    }

    /**
     * Writes the end message and closes the stream.
     */
    public void OnApplicationQuit() {
        sw.WriteLine("End test @" + DateTime.Now);
        sw.Close();
    }


}
