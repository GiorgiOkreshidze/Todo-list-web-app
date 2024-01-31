using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApp.Services;

#pragma warning disable S1075
#pragma warning disable S112

public class UsersWebApiService
{
    private readonly Uri baseAddress = new Uri("https://localhost:7226/api");
    private readonly HttpClient httpClient;

    public UsersWebApiService()
    {
        this.httpClient = new HttpClient
        {
            BaseAddress = this.baseAddress
        };
    }


    public void SetBearerToken(string token)
    {
        this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public string LoginUser(UserDto user)
    {
        string jsonData = JsonConvert.SerializeObject(user);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = this.httpClient.PostAsync(this.httpClient.BaseAddress + "/Auth/Login", content).Result;
        if (response.IsSuccessStatusCode)
        {

            return response.Content.ReadAsStringAsync().Result;
        }
        else
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }

    public void Register(UserDto user)
    {
        string jsonData = JsonConvert.SerializeObject(user);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = this.httpClient.PostAsync(this.httpClient.BaseAddress + "/Auth/Register", content).Result;

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
        }
    }

    internal bool ValidateConnection()
    {
        HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/Auth/ValidateConnection").Result;

        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
