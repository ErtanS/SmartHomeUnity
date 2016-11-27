using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RequestSet {

    private string url;
    private Dictionary<string, string> data;
    private WWWForm form;

    public RequestSet(string url, Dictionary<string,string> data)
    {
        this.url=url;
        this.data = data;
    }

    public RequestSet(string url, WWWForm form)
    {
        this.url = url;
        this.form = form;
    }

    public string getUrl()
    {
        return url;
    }

    public Dictionary<string,string> getData()
    {
        return data;
    }

    public WWWForm getForm()
    {
        return form;
    }
}
