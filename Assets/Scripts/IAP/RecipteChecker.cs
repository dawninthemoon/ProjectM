using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;
using UnityEngine.Networking;
using UnityEngine.Purchasing;
using UnityEngine.Events;

public class RecipteChecker : MonoBehaviour
{
    private readonly static string TARGET_URL = "http://54.180.95.26:4600/api/v1/purchases";
    private System.Action<bool> resultCallback;
    public void CheckRecipte( PurchaseEventArgs args, string productId, System.Action<bool> resultCallback )
    {
        this.resultCallback = resultCallback;
        JSONObject jsonObject = new JSONObject();

        #if UNITY_ANDROID
                jsonObject.Add("os","android");
        #elif UNITY_IOS
                jsonObject.Add("os","ios");
        #else
                jsonObject.Add("os","android");
        #endif

        jsonObject.Add("payload", GetPayload( args ));
        //jsonObject.Add("playerId", User.Instance.userData.userId);

        StartCoroutine( CoPostWebRequest( jsonObject, TARGET_URL, CheckCallback ) );
    }

    public void CheckCallback( bool isSuccess, string requestMessage )
    {
        // if( isSuccess )
        // {
        resultCallback?.Invoke( isSuccess );

        Debug.Log( "Shop Request Message: " + requestMessage );
        // }
    }

    private IEnumerator CoPostWebRequest(JSONObject _form, string _URL, System.Action<bool, string> _OnCallback)
    {
        UnityWebRequest request = new UnityWebRequest(_URL, "POST");
        string json = _form.ToString();
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        //Send the request then wait here until it returns
        yield return request.SendWebRequest();
        bool success2 = request.isNetworkError == false && request.isHttpError == false;
        if (success2)
        {
            Debug.Log("<post>" + request.downloadHandler.text + "</post>");
            Debug.Log("성공!");
        }
        else
        {
            Debug.LogError(request.error);
        }
        _OnCallback?.Invoke(success2, request.downloadHandler.text);
        request.Dispose();
    }

    #if UNITY_ANDROID
    //안드로이드
    protected JSONObject GetPayload( PurchaseEventArgs args )
    {
        string token = "";
        try
        {
            JSONObject purchaseResult = JSONObject.Parse(args.purchasedProduct.receipt);
            JSONObject purchaseResult2 = JSONObject.Parse( purchaseResult.GetString("Payload"));
            JSONObject purchaseResult3 = JSONObject.Parse( purchaseResult2.GetString("json"));
            token = purchaseResult3.GetString("purchaseToken");

        }
        catch ( System.Exception e)
        {
            System.Console.WriteLine(e);
        }

        JSONObject payload = new JSONObject();
        payload.Add("token",token);
        payload.Add("packageName", Application.identifier);
        payload.Add("productId",args.purchasedProduct.definition.id);
        Debug.Log(payload.ToString());

        return payload;
    }
#elif UNITY_IOS  
    //IOS
    protected string GetPayload( PurchaseEventArgs args )
    {
        JSONObject purchaseResult = JSONObject.Parse(args.purchasedProduct.receipt);
        if (!purchaseResult["Payload"].Str.Contains("fake"))
            return purchaseResult["Payload"].Str;
        return "";
    }
#else
    protected string GetPayload( PurchaseEventArgs args )
    {
        return "";
    }
#endif
}
