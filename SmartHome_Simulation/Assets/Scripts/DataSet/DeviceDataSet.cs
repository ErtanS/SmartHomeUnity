using UnityEngine;
using System.Collections;
using System;

public class DeviceDataSet
{
    private int id;
    private String name;
    private int state;
    private String scenarioRoom;
    private int hour;
    private int minute;
    private String category;
    private String type;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeviceDataSet"/> class.
    /// </summary>
    /// <param name="id">Id des Gerätes.</param>
    /// <param name="name">Name</param>
    /// <param name="state">status</param>
    /// <param name="scenarioRoom">Name des Scenarios oder Name des Raumes</param>
    /// <param name="hour">Stunde</param>
    /// <param name="minute">Minute.</param>
    /// <param name="category">Kategorie (scenario,device,timestamp)</param>
    /// <param name="type">Typ.</param>
    public DeviceDataSet(int id, String name, int state, String scenarioRoom, int hour, int minute, String category,
        String type)
    {
        this.id = id;
        this.name = name;
        this.state = state;
        this.scenarioRoom = scenarioRoom;
        this.hour = hour;
        this.minute = minute;
        this.category = category;
        this.type = type;
    }

    public DeviceDataSet(string name, string type)
    {
        this.name = name;
        this.type = type;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeviceDataSet"/> class.
    /// </summary>
    /// <param name="id">Id des Gerätes.</param>
    /// <param name="hour">Stunde</param>
    /// <param name="minute">Minute.</param>
    /// <param name="category">Kategorie (scenario,device,timestamp)</param>
    /// <param name="type">Typ.</param>
    public DeviceDataSet(int id, int hour, int minute, String category, String type)
    {
        this.id = id;
        this.hour = hour;
        this.minute = minute;
        this.category = category;
        this.type = type;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeviceDataSet"/> class.
    /// </summary>
    /// <param name="values">Konstruktor für Unterklassen</param>
    public DeviceDataSet(DeviceDataSet values)
    {
        this.id = values.getId();
        this.name = values.getName();
        this.state = values.getState();
        this.scenarioRoom = values.getScenarioRoom();
        this.hour = values.getHour();
        this.minute = values.getMinute();
        this.category = values.getCategory();
        this.type = values.getType();
    }

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <returns>The identifier.</returns>
    public int getId()
    {
        return id;
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <returns>The name.</returns>
    public String getName()
    {
        return name;
    }

    /// <summary>
    /// Gets the state.
    /// </summary>
    /// <returns>The state.</returns>
    public int getState()
    {
        return state;
    }

    /// <summary>
    /// Gets the scenario room.
    /// </summary>
    /// <returns>The scenario room.</returns>
    public String getScenarioRoom()
    {
        return scenarioRoom;
    }

    /// <summary>
    /// Gets the hour.
    /// </summary>
    /// <returns>The hour.</returns>
    public int getHour()
    {
        return hour;
    }

    /// <summary>
    /// Gets the minute.
    /// </summary>
    /// <returns>The minute.</returns>
    public int getMinute()
    {
        return minute;
    }

    /// <summary>
    /// Gets the category.
    /// </summary>
    /// <returns>The category.</returns>
    public String getCategory()
    {
        return category;
    }

    /// <summary>
    /// Gets the type.
    /// </summary>
    /// <returns>The type.</returns>
    public String getType()
    {
        return type;
    }
}