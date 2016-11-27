using UnityEngine;
using System.Collections;
using System;


public class DoorDataSet : DeviceDataSet
{
    private String password;

    /// <summary>
    /// Initializes a new instance of the <see cref="DoorDataSet"/> class.
    /// </summary>
    /// <param name="values">Grunddaten</param>
    /// <param name="password">Password</param>
    public DoorDataSet(DeviceDataSet values, String password) : base(values)
    {
        this.password = password;
    }

    /// <summary>
    /// Gets the password.
    /// </summary>
    /// <returns>The password.</returns>
    public String getPassword()
    {
        return password;
    }
}