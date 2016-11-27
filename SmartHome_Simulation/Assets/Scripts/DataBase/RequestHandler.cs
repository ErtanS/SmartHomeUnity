using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine;

public class RequestHandler
{
    /// <summary>
    /// Abfrage eines Get-Request an die Datenbank mit Hilfe einer PHP-Datei
    /// </summary>
    /// <returns>Rückgabe der Php-Datei</returns>
    /// <param name="url">Pfad zur Php-Datei</param>
    public string sendGetRequest(string url)
    {
        HttpWebRequest myRequest = (HttpWebRequest) WebRequest.Create(url);
        myRequest.Method = "GET";
        HttpWebResponse myResponse = (HttpWebResponse) myRequest.GetResponse();
        StreamReader sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
        string result = sr.ReadToEnd();
        return result;
    }

    /// <summary>
    /// Fügt ein Gerät in die Datenbank ein
    /// </summary>
    /// <param name="url">Pfad zur Php-Datei</param>
    /// <param name="value">Name des Gerätes</param>
    /// <param name="room">Raum </param>
    /// <param name="table">Tabelle</param>
    public RequestSet insert(string url, string value, string room, string table)
    {
        Dictionary<string, string> postData = new Dictionary<string, string>()
        {
            {"name", value},
            {"room", room},
            {"table", table}
        };
        return new RequestSet(Config.URL_INSERT, postData);
    }

    /// <summary>
    /// Selektiert alle Geräte(category = 'device') bzw. Zeitstempel(category = 'timestamp') aus der Datenbank
    /// </summary>
    /// <returns>Rückgabe der Php-Datei</returns>
    /// <param name="url">Pfad zur Php-Datei</param>
    /// <param name="category">Kategorie</param>
    public string selectAll(string url, string category)
    {
        string pageSource;
        using (WebClient client = new WebClient())
        {
            NameValueCollection postData = new NameValueCollection()
            {
                {"category", category}
            };
            pageSource = Encoding.UTF8.GetString(client.UploadValues(url, postData));
        }
        return pageSource;
    }

    /// <summary>
    /// Fügt einen Musiktitel in die Datenbank ein
    /// </summary>
    /// <returns>Rückgabe der Php-Datei</returns>
    /// <param name="url">Pfad zur Php-Datei</param>
    /// <param name="musicid">Id des Titels</param>
    /// <param name="title">Title</param>
    public RequestSet insertPlaylists(string id, string name, string table)
    {
        Dictionary<string, string> postData = new Dictionary<string, string>()
        {
            {"id", id},
            {"name", name},
            {"table", table}
        };
        return new RequestSet(Config.URL_INSERT_PLAYLIST, postData);
    }

    /// <summary>
    /// Aktualisiert einen bestimmten Wert in der Datenbank
    /// </summary>
    /// <returns>Rückgabe der Php-Datei</returns>
    /// <param name="url">Pfad zur Php-Datei</param>
    /// <param name="table">Tabelle</param>
    /// <param name="set_column">Spaltenname</param>
    /// <param name="set_value">Wert</param>
    /// <param name="id">Id</param>
    public RequestSet updateDatabase(string table, string set_column, string set_value, int id)
    {
        Dictionary<string, string> postData = new Dictionary<string, string>()
        {
            {"table", table},
            {"set_column", set_column},
            {"set_value", set_value},
            {"where_row", "id"},
            {"where_value", id.ToString()}
        };
        return new RequestSet(Config.URL_UPDATE, postData);
    }


    //private void makeRequest(string url, Dictionary<string, string> data)
    //{
    //    UnityWebRequest delReq = UnityWebRequest.Post(url, data);
    //    delReq.Send();
    //}

    /// <summary>
    /// Aktualisiert einen bestimmten Wert in der Datenbank
    /// </summary>
    /// <returns>Rückgabe der Php-Datei</returns>
    /// <param name="url">Pfad zur Php-Datei</param>
    /// <param name="table">Tabelle</param>
    /// <param name="set_column">Spaltenname</param>
    /// <param name="set_value">Wert</param>
    /// <param name="id">Id</param>
    public RequestSet updateDatabase(string table, string set_column, string set_value, string where_row, string where_value)
    {
        Dictionary<string, string> postData = new Dictionary<string, string>()
        {
            {"table", table},
            {"set_column", set_column},
            {"set_value", set_value},
            {"where_row", where_row},
            {"where_value", where_value}
        };
        return new RequestSet(Config.URL_UPDATE, postData);
    }

    /// <summary>
    /// Aktualisert die Eigenschaften aller Geräte die zur übergebenen Uhrzeit eine Zeitschaltung besitzen
    /// </summary>
    /// <returns>Rückgabe der Php-Datei</returns>
    /// <param name="hour">aktuelle Stunde</param>
    /// <param name="minute">aktuelle Minute</param>
    public RequestSet updateTimestamp(int hour, int minute)
    {
        Dictionary<string, string> postData = new Dictionary<string, string>()
        {
            {"hour", hour.ToString()},
            {"minute", minute.ToString()}
        };
        return new RequestSet(Config.URL_UPDATE_TIMESTAMP, postData);
    }

    /// <summary>
    /// Selektiert alle Geräte(category = 'device') bzw. Zeitstempel(category = 'timestamp') aus der Datenbank
    /// </summary>
    /// <returns>Rückgabe der Php-Datei</returns>
    /// <param name="url">Pfad zur Php-Datei</param>
    /// <param name="category">Kategorie</param>
    public RequestSet pushFirebase(string message)
    {
        Dictionary<string, string> postData = new Dictionary<string, string>()
        {
            {"message", message}
        };
        return new RequestSet(Config.URL_PUSH_NOTIFICATION, postData);
    }

    /// <summary>
    /// Schalten eines Emergency Szenarios
    /// </summary>
    /// <param name="name"></param>
    public RequestSet startEmergencyScenario(string name)
    {
        Dictionary<string, string> postData = new Dictionary<string, string>()
        {
            {"name", name}
        };
        return new RequestSet(Config.URL_EMERGENCY, postData);
    }

    /// <summary>
    /// Zurücksetzen der Musik Tabelle
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public RequestSet clearTable(string table)
    {
        Dictionary<string, string> postData = new Dictionary<string, string>()
        {
            {"table", table}
        };
        return new RequestSet(Config.URL_CLEAR_TABLE, postData);
    }

    /// <summary>
    /// Erstellung einer Sicherung der Datenbank
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public RequestSet backupDatabase(string filename)
    {
        Dictionary<string, string> postData = new Dictionary<string, string>()
        {
            {"filename", filename}
        };
        return new RequestSet(Config.URL_BACKUP_DATABASE, postData);
    }

    /// <summary>
    /// Wiederherstellung einer zuvor gesicherten Datenbank
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public RequestSet restoreDatabase(string filename)
    {
        Dictionary<string, string> postData = new Dictionary<string, string>()
        {
                {"filename", filename}
        };
        return new RequestSet(Config.URL_RESTORE_DATABASE, postData);
    }

    /// <summary>
    /// Einfache Methode zum Löschen von Datensätzen
    /// </summary>
    /// <param name="name"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public RequestSet delete(string name, string table)
    {
        Dictionary<string, string> postData = new Dictionary<string, string>()
        {
            {"name", name},
            {"table", table}
        };
        return new RequestSet(Config.URL_DELETE, postData);
    }

    /// <summary>
    /// Löschen eines Raums inkl. aller Komponenten die sich in diesem befindet haben
    /// </summary>
    /// <param name="roomName"></param>
    /// <returns></returns>
    public RequestSet deleteRoom(string roomName)
    {
        Dictionary<string, string> postData = new Dictionary<string, string>()
        {
            {"room", roomName}
        };
        return new RequestSet(Config.URL_DELETE_ROOM, postData);
    }

    /// <summary>
    /// Umbenennen eines Raums 
    /// </summary>
    /// <param name="oldRoom"></param>
    /// <param name="newRoom"></param>
    public RequestSet renameRoom(string oldRoom, string newRoom)
    {
        Dictionary<string, string> postData = new Dictionary<string, string>()
        {
            {Config.STRING_INTENT_ROOM, oldRoom},
            {Config.TAG_NAME, newRoom}
        };
        return new RequestSet(Config.URL_UPDATE_ROOMNAME, postData);
    }

    public IEnumerator makeRequest(RequestSet data)
    {
        UnityWebRequest www = UnityWebRequest.Post(data.getUrl(), data.getData());
        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else {
            //Show results as text
        //Debug.Log(www.downloadHandler.text);
        }
    }

    public IEnumerator uploadRequest(RequestSet data)
    {
        UnityWebRequest www = UnityWebRequest.Post(data.getUrl(), data.getForm());
        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
           // Debug.Log(www.downloadHandler.text);
        }
    }

   
}