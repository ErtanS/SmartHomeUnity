using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSet : ArrayList
{
    private object locked = new object();

    /// <summary>
    /// Initializes a new instance of the <see cref="DataSet"/> class.
    /// </summary>
    /// <param name="resultDoor">Rückgabe aller Türen aus der Php-Datei</param>
    /// <param name="resultLight">Rückgabe aller Lampen aus der Php-Datei.</param>
    /// <param name="resultWindow">Rückgabe aller Fenster aus der Php-Datei.</param>
    /// <param name="resultHeater">Rückgabe aller Heizungen aus der Php-Datei.</param>
    /// <param name="resultSpeaker">Rückgabe aller Lautsprecher aus der Php-Datei.</param>
    /// <param name="resultShutters">Rückgabe aller Rollladen aus der Php-Datei.</param>
    public DataSet(String resultDoor, String resultLight, String resultWindow, String resultHeater, String resultSpeaker,
        String resultShutters, String resultCamera, String resultDryer, String resultOven, String resultPc,
        String resultStove, String resultTv, String resultWall, String resultWasher, String resultWater)
    {
        fillDevices(resultDoor, resultLight, resultWindow, resultHeater, resultSpeaker, resultShutters, resultCamera,
            resultDryer, resultOven, resultPc, resultStove, resultTv, resultWall, resultWasher, resultWater);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataSet"/> class.
    /// </summary>
    /// <param name="resultDoor">Rückgabe aller Türen aus der Php-Datei</param>
    /// <param name="resultLight">Rückgabe aller Lampen aus der Php-Datei.</param>
    /// <param name="resultWindow">Rückgabe aller Fenster aus der Php-Datei.</param>
    /// <param name="resultHeater">Rückgabe aller Heizungen aus der Php-Datei.</param>
    /// <param name="resultSpeaker">Rückgabe aller Lautsprecher aus der Php-Datei.</param>
    /// <param name="resultShutters">Rückgabe aller Rollladen aus der Php-Datei.</param>
    public DataSet(String resultDoor, String resultLight, String resultWindow, String resultHeater, String resultSpeaker,
        String resultShutters, String resultScenario, String resultCamera, String resultDryer, String resultOven,
        String resultPc, String resultStove, String resultTv, String resultWall, String resultWasher, String resultWater)
    {
        fillTimestamps(resultDoor, resultLight, resultWindow, resultHeater, resultSpeaker, resultShutters,
            resultScenario, resultCamera, resultDryer, resultOven, resultPc, resultStove, resultTv, resultWall,
            resultWasher, resultWater);
    }

    /// <summary>
    /// Füllen aller Datensätze aus der Datenbank
    /// </summary>
    /// <param name="resultDoor"></param>
    /// <param name="resultLight"></param>
    /// <param name="resultWindow"></param>
    /// <param name="resultHeater"></param>
    /// <param name="resultSpeaker"></param>
    /// <param name="resultShutters"></param>
    /// <param name="resultCamera"></param>
    /// <param name="resultDryer"></param>
    /// <param name="resultOven"></param>
    /// <param name="resultPc"></param>
    /// <param name="resultStove"></param>
    /// <param name="resultTv"></param>
    /// <param name="resultWall"></param>
    /// <param name="resultWasher"></param>
    /// <param name="resultWater"></param>
    public void fillDevices(String resultDoor, String resultLight, String resultWindow, String resultHeater,
        String resultSpeaker, String resultShutters, String resultCamera, String resultDryer, String resultOven,
        String resultPc, String resultStove, String resultTv, String resultWall, String resultWasher, String resultWater)
    {
        JsonData jArray;
        jArray = JsonMapper.ToObject(resultDoor);
        addDoor(Config.TAG_JSON_ARRAY, jArray);
        jArray = JsonMapper.ToObject(resultLight);
        addLight(Config.TAG_JSON_ARRAY, jArray);
        jArray = JsonMapper.ToObject(resultShutters);
        addShutters(Config.TAG_JSON_ARRAY, jArray);
        jArray = JsonMapper.ToObject(resultSpeaker);
        addSpeaker(Config.TAG_JSON_ARRAY, jArray);
        jArray = JsonMapper.ToObject(resultWindow);
        addWindow(Config.TAG_JSON_ARRAY, jArray);
        jArray = JsonMapper.ToObject(resultHeater);
        addHeater(Config.TAG_JSON_ARRAY, jArray);
        jArray = JsonMapper.ToObject(resultCamera);
        addCamera(Config.TAG_JSON_ARRAY, jArray);
        jArray = JsonMapper.ToObject(resultDryer);
        addDryer(Config.TAG_JSON_ARRAY, jArray);
        jArray = JsonMapper.ToObject(resultOven);
        addOven(Config.TAG_JSON_ARRAY, jArray);
        jArray = JsonMapper.ToObject(resultPc);
        addPc(Config.TAG_JSON_ARRAY, jArray);
        jArray = JsonMapper.ToObject(resultStove);
        addStove(Config.TAG_JSON_ARRAY, jArray);
        jArray = JsonMapper.ToObject(resultTv);
        addTv(Config.TAG_JSON_ARRAY, jArray);
        jArray = JsonMapper.ToObject(resultWall);
        addWall(Config.TAG_JSON_ARRAY, jArray);
        jArray = JsonMapper.ToObject(resultWasher);
        addWasher(Config.TAG_JSON_ARRAY, jArray);
        jArray = JsonMapper.ToObject(resultWater);
        addWater(Config.TAG_JSON_ARRAY, jArray);
    }

    /// <summary>
    /// Füllen aller Zeitstempel aus der Datenbank
    /// </summary>
    /// <param name="resultDoor"></param>
    /// <param name="resultLight"></param>
    /// <param name="resultWindow"></param>
    /// <param name="resultHeater"></param>
    /// <param name="resultSpeaker"></param>
    /// <param name="resultShutters"></param>
    /// <param name="resultScenario"></param>
    /// <param name="resultCamera"></param>
    /// <param name="resultDryer"></param>
    /// <param name="resultOven"></param>
    /// <param name="resultPc"></param>
    /// <param name="resultStove"></param>
    /// <param name="resultTv"></param>
    /// <param name="resultWall"></param>
    /// <param name="resultWasher"></param>
    /// <param name="resultWater"></param>
    public void fillTimestamps(String resultDoor, String resultLight, String resultWindow, String resultHeater,
        String resultSpeaker, String resultShutters, String resultScenario, String resultCamera, String resultDryer,
        String resultOven, String resultPc, String resultStove, String resultTv, String resultWall, String resultWasher,
        String resultWater)
    {
        JsonData jArray;
        jArray = JsonMapper.ToObject(resultDoor);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_TYPE_EN_DOOR);
        jArray = JsonMapper.ToObject(resultLight);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_TYPE_EN_LIGHT);
        jArray = JsonMapper.ToObject(resultShutters);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_TYPE_EN_SHUTTERS);
        jArray = JsonMapper.ToObject(resultSpeaker);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_TYPE_EN_SPEAKER);
        jArray = JsonMapper.ToObject(resultWindow);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_TYPE_EN_WINDOW);
        jArray = JsonMapper.ToObject(resultHeater);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_TYPE_EN_HEATER);
        jArray = JsonMapper.ToObject(resultScenario);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_CATEGORY_SCENARIO);

        jArray = JsonMapper.ToObject(resultCamera);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_TYPE_EN_CAMERA);
        jArray = JsonMapper.ToObject(resultDryer);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_TYPE_EN_DRYER);
        jArray = JsonMapper.ToObject(resultOven);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_TYPE_EN_OVEN);
        jArray = JsonMapper.ToObject(resultPc);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_TYPE_EN_PC);
        jArray = JsonMapper.ToObject(resultStove);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_TYPE_EN_STOVE);
        jArray = JsonMapper.ToObject(resultTv);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_TYPE_EN_TV);
        jArray = JsonMapper.ToObject(resultWall);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_TYPE_EN_WALL);
        jArray = JsonMapper.ToObject(resultWasher);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_TYPE_EN_WASHER);
        jArray = JsonMapper.ToObject(resultWater);
        addTimestamp(Config.TAG_JSON_ARRAY, jArray, Config.STRING_TYPE_EN_WATER);
    }

    /// <summary>
    /// Löscht das Element mit spezifizierter id und type.
    /// </summary>
    /// <param name="id"> Id des zu löschenden Elements</param>
    /// <param name="type">Typ des zu löschenden Elements</param>
    private void remove(int id, String type)
    {
        foreach (DeviceDataSet item in this)
        {
            if (item.getId() == id && item.getType().Equals(type))
            {
                Remove(item);
            }
        }
    }

    /// <summary>
    /// Fügt das spezifiezierte Elemnt hinzu.
    /// </summary>
    /// <param name="data">Gerät, welches zum DataSet hinzugefügt werden soll </param>
    private void add(DeviceDataSet data)
    {
        bool isInList = false;
        int id = data.getId();
        string type = data.getType();
        string category = data.getCategory();
        if (category.Equals("delete"))
        {
            remove(id, type);
        }
        else
        {
            foreach (DeviceDataSet item in this)
            {
                if (item.getId() == id && item.getType().Equals(type))
                {
                    isInList = true;
                    Remove(item);
                    Add(data);
                    break;
                }
            }
            if (!isInList)
            {
                Add(data);
            }
        }
    }

    /// <summary>
    /// Verarbeitet Daten, die bei jedem Gerät vorhanden sind
    /// </summary>
    /// <returns> Datensatz für Gerät </returns>
    /// <param name="jArray">Daten der PHP-Rückgabe</param>
    /// <param name="jsonTag">Tag zur Zuordnung der PHP-Rückgabe</param>
    /// <param name="position">Position</param>
    /// <param name="type">Typ des Gerätes</param>
    private void addTimestamp(String jsonTag, JsonData jArray, string type)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            int id = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_ID].ToString().Trim());
            string category = jArray[jsonTag][i][Config.TAG_CATEGORY].ToString().Trim();
            int hour = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_HOUR].ToString().Trim());
            int minute = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_MINUTE].ToString().Trim());
            DeviceDataSet data = new DeviceDataSet(id, hour, minute, category, type);
            lock (locked)
            {
                add(data);
            }
        }
    }


    /// <summary>
    /// Fügt Türen zum DataSet hinzu.
    /// </summary>
    /// <param name="jsonTag"> Tag zur Zuordnung der PHP-Rückgabe</param>
    /// <param name="jArray"> Daten der PHP-Rückgabe</param>
    private void addDoor(String jsonTag, JsonData jArray)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            DeviceDataSet jsonData = jsonExtension(jArray, jsonTag, i, Config.STRING_TYPE_EN_DOOR);
            string password = jArray[jsonTag][i][Config.TAG_PASSWORD].ToString().Trim();
            DoorDataSet data = new DoorDataSet(jsonData, password);
            lock (locked)
            {
                add(data);
            }
        }
    }

    /// <summary>
    /// Fügt Lautsprecher zum DataSet hinzu.
    /// </summary>
    /// <param name="jsonTag"> Tag zur Zuordnung der PHP-Rückgabe</param>
    /// <param name="jArray"> Daten der PHP-Rückgabe</param>
    private void addSpeaker(String jsonTag, JsonData jArray)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            DeviceDataSet jsonData = jsonExtension(jArray, jsonTag, i, Config.STRING_TYPE_EN_SPEAKER);
            int volume = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_VOLUME].ToString().Trim());
            int songid = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_SONGID].ToString().Trim());
            int stop = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_STOP].ToString().Trim());
            SpeakerDataSet data = new SpeakerDataSet(jsonData, volume, songid, stop);
            lock (locked)
            {
                add(data);
            }
        }
    }

    /// <summary>
    /// Fügt Rolladen zum DataSet hinzu.
    /// </summary>
    /// <param name="jsonTag"> Tag zur Zuordnung der PHP-Rückgabe</param>
    /// <param name="jArray"> Daten der PHP-Rückgabe</param>
    private void addShutters(String jsonTag, JsonData jArray)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            DeviceDataSet jsonData = jsonExtension(jArray, jsonTag, i, Config.STRING_TYPE_EN_SHUTTERS);
            ShuttersDataSet data = new ShuttersDataSet(jsonData);
            lock (locked)
            {
                add(data);
            }
        }
    }

    /// <summary>
    /// Fügt Fenster zum DataSet hinzu.
    /// </summary>
    /// <param name="jsonTag"> Tag zur Zuordnung der PHP-Rückgabe</param>
    /// <param name="jArray"> Daten der PHP-Rückgabe</param>
    private void addWindow(String jsonTag, JsonData jArray)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            DeviceDataSet jsonData = jsonExtension(jArray, jsonTag, i, Config.STRING_TYPE_EN_WINDOW);
            WindowDataSet data = new WindowDataSet(jsonData);
            lock (locked)
            {
                add(data);
            }
        }
    }

    /// <summary>
    /// Fügt Lichter zum DataSet hinzu.
    /// </summary>
    /// <param name="jsonTag"> Tag zur Zuordnung der PHP-Rückgabe</param>
    /// <param name="jArray"> Daten der PHP-Rückgabe</param>
    private void addLight(String jsonTag, JsonData jArray)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            DeviceDataSet jsonData = jsonExtension(jArray, jsonTag, i, Config.STRING_TYPE_EN_LIGHT);
            string color = jArray[jsonTag][i][Config.TAG_COLOR].ToString().Trim();
            int intensity = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_INTENSITY].ToString().Trim());
            LightDataSet data = new LightDataSet(jsonData, color, intensity);
            lock (locked)
            {
                add(data);
            }
        }
    }

    /// <summary>
    /// Fügt Heizungen zum DataSet hinzu.
    /// </summary>
    /// <param name="jsonTag"> Tag zur Zuordnung der PHP-Rückgabe</param>
    /// <param name="jArray"> Daten der PHP-Rückgabe</param>
    private void addHeater(String jsonTag, JsonData jArray)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            DeviceDataSet jsonData = jsonExtension(jArray, jsonTag, i, Config.STRING_TYPE_EN_HEATER);
            int temperature = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_TEMPERATURE].ToString().Trim());
            HeaterDataSet data = new HeaterDataSet(jsonData, temperature);
            lock (locked)
            {
                add(data);
            }
        }
    }

    /// <summary>
    /// Fügt Kameras zum DataSet hinzu.
    /// </summary>
    /// <param name="jsonTag"></param>
    /// <param name="jArray"></param>
    private void addCamera(String jsonTag, JsonData jArray)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            DeviceDataSet jsonData = jsonExtension(jArray, jsonTag, i, Config.STRING_TYPE_EN_CAMERA);
            int emergency = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_EMERGENCY].ToString().Trim());
            int autoEmergency = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_AUTOEMERGENCY].ToString().Trim());
            int frequency = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_FREQUENCY].ToString().Trim());
            CameraDataSet data = new CameraDataSet(jsonData, emergency, autoEmergency, frequency);
            lock (locked)
            {
                add(data);
            }
        }
    }

    /// <summary>
    /// Fügt Trockner zum DataSet hinzu.
    /// </summary>
    /// <param name="jsonTag"></param>
    /// <param name="jArray"></param>
    private void addDryer(String jsonTag, JsonData jArray)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            DeviceDataSet jsonData = jsonExtension(jArray, jsonTag, i, Config.STRING_TYPE_EN_DRYER);
            int temperature = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_TEMPERATURE].ToString().Trim());
            int duration = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_DURATION].ToString().Trim());
            int rpm = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_RPM].ToString().Trim());
            int amount = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_AMOUNT].ToString().Trim());
            int clothes = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_CLOTHES].ToString().Trim());
            DryerDataSet data = new DryerDataSet(jsonData, temperature, duration, rpm, amount, clothes);
            lock (locked)
            {
                add(data);
            }
        }
    }

    /// <summary>
    /// Fügt Backöfen zum DataSet hinzu.
    /// </summary>
    /// <param name="jsonTag"></param>
    /// <param name="jArray"></param>
    private void addOven(String jsonTag, JsonData jArray)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            DeviceDataSet jsonData = jsonExtension(jArray, jsonTag, i, Config.STRING_TYPE_EN_OVEN);
            int temperature = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_TEMPERATURE].ToString().Trim());
            int duration = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_DURATION].ToString().Trim());
            OvenDataSet data = new OvenDataSet(jsonData, temperature, duration);
            lock (locked)
            {
                add(data);
            }
        }
    }

    /// <summary>
    /// Fügt Pcs zum DataSet hinzu.
    /// </summary>
    /// <param name="jsonTag"></param>
    /// <param name="jArray"></param>
    private void addPc(String jsonTag, JsonData jArray)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            DeviceDataSet jsonData = jsonExtension(jArray, jsonTag, i, Config.STRING_TYPE_EN_PC);
            int videoid = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_VIDEOID].ToString().Trim());
            string pictureid = jArray[jsonTag][i][Config.TAG_PICTUREID].ToString().Trim();
            int volume = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_VOLUME].ToString().Trim());
            PcDataSet data = new PcDataSet(jsonData, videoid, pictureid, volume);
            lock (locked)
            {
                add(data);
            }
        }
    }

    /// <summary>
    /// Fügt Herde zum DataSet hinzu.
    /// </summary>
    /// <param name="jsonTag"></param>
    /// <param name="jArray"></param>
    private void addStove(String jsonTag, JsonData jArray)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            DeviceDataSet jsonData = jsonExtension(jArray, jsonTag, i, Config.STRING_TYPE_EN_STOVE);
            int duration = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_DURATION].ToString().Trim());
            int temperature = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_TEMPERATURE].ToString().Trim());
            StoveDataSet data = new StoveDataSet(jsonData, temperature, duration);
            lock (locked)
            {
                add(data);
            }
        }
    }

    /// <summary>
    /// Fügt Fernseher zum DataSet hinzu.
    /// </summary>
    /// <param name="jsonTag"></param>
    /// <param name="jArray"></param>
    private void addTv(String jsonTag, JsonData jArray)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            DeviceDataSet jsonData = jsonExtension(jArray, jsonTag, i, Config.STRING_TYPE_EN_TV);
            int channel = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_CHANNEL].ToString().Trim());
            string pictureid = jArray[jsonTag][i][Config.TAG_PICTUREID].ToString().Trim();
            int volume = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_VOLUME].ToString().Trim());
            TvDataSet data = new TvDataSet(jsonData, channel, pictureid, volume);
            lock (locked)
            {
                add(data);
            }
        }
    }

    /// <summary>
    /// Fügt Leinwände zum DataSet hinzu
    /// </summary>
    /// <param name="jsonTag"></param>
    /// <param name="jArray"></param>
    private void addWall(String jsonTag, JsonData jArray)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            DeviceDataSet jsonData = jsonExtension(jArray, jsonTag, i, Config.STRING_TYPE_EN_WALL);
            string color = jArray[jsonTag][i][Config.TAG_COLOR].ToString().Trim();
            string pictureid = jArray[jsonTag][i][Config.TAG_PICTUREID].ToString().Trim();
            WallDataSet data = new WallDataSet(jsonData, color, pictureid);
            lock (locked)
            {
                add(data);
            }
        }
    }

    /// <summary>
    /// Fügt Waschmaschinen zum DataSet hinzu.
    /// </summary>
    /// <param name="jsonTag"></param>
    /// <param name="jArray"></param>
    private void addWasher(String jsonTag, JsonData jArray)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            DeviceDataSet jsonData = jsonExtension(jArray, jsonTag, i, Config.STRING_TYPE_EN_WASHER);
            int temperature = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_TEMPERATURE].ToString().Trim());
            int duration = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_DURATION].ToString().Trim());
            int rpm = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_RPM].ToString().Trim());
            int amount = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_AMOUNT].ToString().Trim());
            int clothes = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_CLOTHES].ToString().Trim());
            WasherDataSet data = new WasherDataSet(jsonData, temperature, duration, rpm, amount, clothes);
            lock (locked)
            {
                add(data);
            }
        }
    }

    /// <summary>
    /// Fügt Waschbecken zum DataSet hinzu.
    /// </summary>
    /// <param name="jsonTag"></param>
    /// <param name="jArray"></param>
    private void addWater(String jsonTag, JsonData jArray)
    {
        for (int i = 0; i < jArray[jsonTag].Count; i++)
        {
            DeviceDataSet jsonData = jsonExtension(jArray, jsonTag, i, Config.STRING_TYPE_EN_WATER);
            int intensity = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_INTENSITY].ToString().Trim());
            int temperature = Convert.ToInt32(jArray[jsonTag][i][Config.TAG_TEMPERATURE].ToString().Trim());
            WaterDataSet data = new WaterDataSet(jsonData, intensity, temperature);
            lock (locked)
            {
                add(data);
            }
        }
    }


    /// <summary>
    /// Verarbeitet Daten, die bei jedem Gerät vorhanden sind
    /// </summary>
    /// <returns> Datensatz für Gerät </returns>
    /// <param name="jArray">Daten der PHP-Rückgabe</param>
    /// <param name="jsonTag">Tag zur Zuordnung der PHP-Rückgabe</param>
    /// <param name="position">Position</param>
    /// <param name="type">Typ des Gerätes</param>
    private DeviceDataSet jsonExtension(JsonData jArray, String jsonTag, int position, string type)
    {
        int id = Convert.ToInt32(jArray[jsonTag][position][Config.TAG_ID].ToString().Trim());
        string name = jArray[jsonTag][position][Config.TAG_NAME].ToString().Trim();
        int state = 0;
        if (!type.Equals(Config.STRING_TYPE_EN_WALL))
        {
            state = Convert.ToInt32(jArray[jsonTag][position][Config.TAG_STATE].ToString().Trim());
        }
        string scenarioRoom = jArray[jsonTag][position][Config.TAG_SCENARIOROOM].ToString().Trim();
        string category = jArray[jsonTag][position][Config.TAG_CATEGORY].ToString().Trim();
        int hour = Convert.ToInt32(jArray[jsonTag][position][Config.TAG_HOUR].ToString().Trim());
        int minute = Convert.ToInt32(jArray[jsonTag][position][Config.TAG_MINUTE].ToString().Trim());
        return new DeviceDataSet(id, name, state, scenarioRoom, hour, minute, category, type);
    }

    /// <summary>
    /// Erneuert Datensatz des Gerätes mit spezifizierten Namen.
    /// </summary>
    /// <returns> Datensatz </returns>
    /// <param name="name">Name des Gerätes</param>
    public DeviceDataSet getDevice(String name)
    {
        lock (locked)
        {
            foreach (DeviceDataSet item in this)
            {
                if (item.getName().Equals(name))
                {
                    return item;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Überprüft, ob zu gegebener Uhrzeit ein Timestamp vorhanden ist.
    /// </summary>
    /// <returns><c>true</c>, wenn, Timestamp vorhanden <c>false</c> wenn nicht </returns>
    /// <param name="hour"> Stunde </param>
    /// <param name="minute">Minute</param>
    public Boolean isTimestamp(int hour, int minute)
    {
        lock (locked)
        {
            foreach (DeviceDataSet item in this)
            {
                if (item.getHour() == hour && item.getMinute() == minute)
                {
                    return true;
                }
            }
        }
        return false;
    }
}