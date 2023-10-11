using System.Net.Http.Headers;
using System.Text.Json;
using WebUI.Session;

namespace WebUI.HttpClientUtilities;

public class ApiHttpClient
{
    private readonly JwtApplication _jwtApplication;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public ApiHttpClient(JwtApplication jwtApplication, HttpClient httpClient)
    {
        _jwtApplication = jwtApplication;
        _httpClient = httpClient;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
    }

    public async Task<T> SendRequest<T>(HttpContent? httpContent, HttpMethod method, string path, bool withBearerToken = true, string? token = null)
    {
        using var requestMessage = new HttpRequestMessage(method, path);

        if (withBearerToken)
        {
            if (string.IsNullOrEmpty(_jwtApplication.JwtToken) && !string.IsNullOrEmpty(token))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _jwtApplication.JwtToken);
            }
        }

        if (httpContent is not null)
        {
            requestMessage.Content = httpContent;
        }

        using var response = await _httpClient.SendAsync(requestMessage);

        if (!response.IsSuccessStatusCode)
        {
            var constructor = typeof(T).GetConstructor(Type.EmptyTypes);
            return constructor != null ? (T)constructor.Invoke(null) : default!;
        }

        var responseStream = await response.Content.ReadAsStreamAsync();
        if (responseStream.Length == 0)
        {
            return default!;
        }

        return await JsonSerializer.DeserializeAsync<T>(responseStream, _jsonSerializerOptions) ?? default!;
    }

    public async Task<T> SendRequest<T>(HttpMethod method, string path, object? dto = null, bool withBearerToken = true, string? token = null)
    {
        HttpContent? httpContent = null;

        if (dto is not null)
        {
            var json = JsonSerializer.Serialize(dto);

            var stringContent = new StringContent(json);
            stringContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            httpContent = stringContent;
        }

        return await SendRequest<T>(httpContent, method, path, withBearerToken, token);
    }
}
