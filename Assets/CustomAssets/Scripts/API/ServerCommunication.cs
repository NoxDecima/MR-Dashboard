using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

/// <summary>
/// This class is responsible for handling REST API requests to remote server.
/// To extend this class you just need to add new API methods.
/// </summary>
public class ServerCommunication : PersistentLazySingleton<ServerCommunication>
{
    #region [Server Communication]

    /// <summary>
    /// This method is used to begin sending request process.
    /// </summary>
    /// <param name="url">API url.</param>
    /// <param name="callbackOnSuccess">Callback on success.</param>
    /// <param name="callbackOnFail">Callback on fail.</param>
    /// <typeparam name="T">Data Model Type.</typeparam>
    private void SendRequest(string url, UnityAction<string> callbackOnSuccess, UnityAction<string> callbackOnFail)
    {
        StartCoroutine(RequestCoroutine(url, callbackOnSuccess, callbackOnFail));
    }

    /// <summary>
    /// Coroutine that handles communication with REST server.
    /// </summary>
    /// <returns>The coroutine.</returns>
    /// <param name="url">API url.</param>
    /// <param name="callbackOnSuccess">Callback on success.</param>
    /// <param name="callbackOnFail">Callback on fail.</param>
    /// <typeparam name="T">Data Model Type.</typeparam>
    private IEnumerator RequestCoroutine(string url, UnityAction<string> callbackOnSuccess, UnityAction<string> callbackOnFail)
    {
        var www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || 
            www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
            callbackOnFail?.Invoke(www.error);
        }
        else
        {
            callbackOnSuccess?.Invoke(www.downloadHandler.text);
		}
    }

    /// <summary>
    /// This method finishes request process as we have received answer from server.
    /// </summary>
    /// <param name="data">Data received from server in JSON format.</param>
    /// <param name="callbackOnSuccess">Callback on success.</param>
    /// <param name="callbackOnFail">Callback on fail.</param>
    /// <typeparam name="T">Data Model Type.</typeparam>
    private void ParseResponse<T>(string data, UnityAction<T> callbackOnSuccess, UnityAction<string> callbackOnFail)
    {
        var parsedData = JsonUtility.FromJson<T>(data);
        callbackOnSuccess?.Invoke(parsedData);
    }

    #endregion

    
    #region [API]

    private void GetInstances(int userID, UnityAction<UserInstance[]> callbackOnSuccess, UnityAction<string> callbackOnFail)
    {
        SendRequest(string.Format(APIServerConfig.API_GET_INSTANCE, userID),
					jsonString => {
						var parsedData = JsonUtility.FromJson<UserInstanceArray>("{\"array\": " + jsonString + "}");
                        if(parsedData.array.Length > 0) {
        				    callbackOnSuccess?.Invoke(parsedData.array);
                        } else {
                            callbackOnFail?.Invoke($"No Instances received for user: {userID}.");
                        }

					}, 
					callbackOnFail);
    }
    
    private void GetLive(int userID, UnityAction<UserLive[]> callbackOnSuccess, UnityAction<string> callbackOnFail)
    {
                SendRequest(string.Format(APIServerConfig.API_GET_INSTANCE, userID),
					jsonString => {
						var parsedData = JsonUtility.FromJson<UserLiveArray>("{\"array\": " + jsonString + "}");
        				callbackOnSuccess?.Invoke(parsedData.array);
					}, 
					callbackOnFail);    
    }

    public void getTopic(int userID, UnityAction<string> callbackOnSuccess, UnityAction<string> callbackOnFail) 
    {
        GetInstances(userID, 
        instances => {
            var activeInstance = selectActiveInstance(instances);
            callbackOnSuccess?.Invoke(activeInstance.lp_description);
        }, callbackOnFail);
    }

    
    public void getEloHistory(int userID, UnityAction<int[]> callbackOnSuccess, UnityAction<string> callbackOnFail) 
    {
        GetInstances(userID, 
        instances => {
            var activeInstance = selectActiveInstance(instances);
            callbackOnSuccess?.Invoke(activeInstance.elo_path);
        }, callbackOnFail);
    }


    public void getProgress(int userID, UnityAction<float> callbackOnSuccess, UnityAction<string> callbackOnFail) 
    {
        GetInstances(userID, 
        instances => {
            var activeInstance = selectActiveInstance(instances);
            callbackOnSuccess?.Invoke(((float)activeInstance.elo) / 100.0f);
        }, callbackOnFail);
    }

    public void getEmotional(int userID, UnityAction<float> callbackOnSuccess, UnityAction<string> callbackOnFail) 
    {
        GetInstances(userID, 
        instances => {
            var activeInstance = selectActiveInstance(instances);

            var eloLength = activeInstance.elo_path.Length;

            float ratio = ((float)countCorrect(
                activeInstance.elo_path
                    .Skip(Math.Max(eloLength-20, 0))
                    .Take(eloLength)
                    .ToArray()
            )) / eloLength;

            callbackOnSuccess?.Invoke(ratio);

        }, callbackOnFail);
    }


    public void startSimulation(string classID, UnityAction<Student[]> callbackOnSuccess, UnityAction<string> callbackOnFail)
    {
        SendRequest(string.Format(APIServerConfig.API_START_SIMULATION, classID),
            jsonString => {
                var parsedData = JsonUtility.FromJson<Students>(jsonString);
                if(parsedData.students.Length > 0) {
                    callbackOnSuccess?.Invoke(parsedData.students);
                } else {
                    callbackOnFail?.Invoke($"No Students received for class: {classID}.");
                }
            }, 
            callbackOnFail);
    }


    private int countCorrect(int[] eloPath)
    {
        int nrCorrect = 0;

        if (eloPath.Length > 0)
        {
            if (eloPath[0] != 0) { nrCorrect++; }

            for (int i = 1; i < eloPath.Length; i++)
            {
                if (eloPath[i] > eloPath[i-1]) { nrCorrect++; }
            }
        }

        return nrCorrect;
    }


    private UserInstance selectActiveInstance(UserInstance[] instances)
    {
        // TODO Fix this function to be something proper.
        return instances.Where(i => i.was_last_active == 1).First();
    }

    #endregion
}