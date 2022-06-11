using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIServerConfig
{
    // API Endpoints.
    public const string SERVER_API_URL_FORMAT = "http://www.leerpaden.nl/";
    public const string API_GET_INSTANCE = SERVER_API_URL_FORMAT + "get_simulation/user={0}";
    public const string API_GET_LIVE = SERVER_API_URL_FORMAT + "live/user={0}";

    public const string API_START_SIMULATION = SERVER_API_URL_FORMAT + "start_simulation/class_id={0}";
    public const string API_NEXT_QUESTION_SIMULATION = SERVER_API_URL_FORMAT + "time_to_next_question/class_id={0}";
    public const string API_SKIP_TO_QUESTION = SERVER_API_URL_FORMAT + "skip_waiting/class_id={0}";
    

}
