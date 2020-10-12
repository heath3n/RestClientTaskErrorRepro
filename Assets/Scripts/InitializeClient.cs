using System;
using System.Threading.Tasks;
using Proyecto26;
using UnityEngine;

public class InitializeClient : MonoBehaviour {
    void Start() {
        CreateUser().ContinueWith(userTask => {
            User user = userTask.Result;
            user.GetAuthToken().ContinueWith(tokenTask => {
                RestClient.DefaultRequestHeaders["Authorization"] = tokenTask.Result;
                try {
                    RestClient.Post("https://jsonplaceholder.typicode.com/posts", null);
                } catch (Exception e) {
                    Debug.Log(e.Message);
                }
            });
        });
    }
    
    private async Task<User> CreateUser() {
        return new User();
    }
}

public class User {
    // This task gets some information necessary to configure RestClient.
    public async Task<string> GetAuthToken() {
        return "authToken123";
    }
}