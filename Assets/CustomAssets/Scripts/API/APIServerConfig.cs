using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIServerConfig
{
    // URL with place to put API method in it.
    public const string SERVER_API_URL_FORMAT = "http://www.leerpaden.nl/";
    // Mocky generates random strings for your endpoints, you should name them properly!
    public const string API_GET_INSTANCE = SERVER_API_URL_FORMAT + "get_instances/user={0}";
    public const string API_GET_LIVE = SERVER_API_URL_FORMAT + "live/user={0}";
}
