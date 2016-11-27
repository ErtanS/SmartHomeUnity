using UnityEngine;
using System.Collections;

public class DeviceName
{
    private string firstName;
    private string newName;
    private string deviceType;

	/// <summary>
	/// Initializes a new instance of the <see cref="DeviceName"/> class.
	/// </summary>
	/// <param name="firstName">First name.</param>
	/// <param name="deviceType">Device type.</param>
    public DeviceName(string firstName, string deviceType)
    {
        this.firstName = firstName;
        this.deviceType = deviceType;
    }

	/// <summary>
	/// Reset this instance.
	/// </summary>
    public void reset()
    {
        firstName = newName;
        newName = null;
    }

	/// <summary>
	/// Rename the specified newName.
	/// </summary>
	/// <param name="newName">New name.</param>
    public void rename(string newName)
    {
        this.newName = newName;
    }

	/// <summary>
	/// Gets the new name.
	/// </summary>
	/// <returns>The new name.</returns>
    public string getNewName()
    {
        return newName;
    }

	/// <summary>
	/// Gets the first name.
	/// </summary>
	/// <returns>The first name.</returns>
    public string getFirstName()
    {
        return firstName;
    }

	/// <summary>
	/// Gets the type of the device.
	/// </summary>
	/// <returns>The device type.</returns>
    public string getDeviceType()
    {
        return deviceType;
    }
}