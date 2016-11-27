using UnityEngine;
using System.Collections;
using System.Windows.Forms;
using System.IO;

public class FileExplorer
{

    private DataManager dm;

    public FileExplorer(DataManager dm)
    {
        this.dm = dm;
    }

	/// <summary>
	/// Saves the file.
	/// </summary>
	/// <returns><c>true</c>, if file was saved, <c>false</c> otherwise.</returns>
	/// <param name="saveData">Save data.</param>
    public bool saveFile(string[] saveData)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "Build|*.build",
            Title = "Speichern..."
        };

        saveFileDialog.ShowDialog();
        string fileName = saveFileDialog.FileName;
        if (!string.IsNullOrEmpty(fileName))
        {
            File.WriteAllLines(fileName, saveData);
            LevelManager.changeFile(fileName);
            dm.initializeDatabase(true);
            return true;
        }
        return false;
    }

	/// <summary>
	/// Loads the file.
	/// </summary>
	/// <returns><c>true</c>, if file was loaded, <c>false</c> otherwise.</returns>
    public bool loadFile()
    {
        OpenFileDialog loadDialog = new OpenFileDialog
        {
            Filter = "Build|*.build",
            Title = "Lade..."
        };

        loadDialog.ShowDialog();
        string fileName = loadDialog.FileName;

        if (!string.IsNullOrEmpty(fileName))
        {
            GameobjectLoader.startLoading();
            LevelManager.changeFile(fileName);
            return true;
        }
        return false;
    }
}