namespace IssueTrackerApi.Adapters;

public class BusinessApiAdapter
{
    private readonly HttpClient _httpClient;

    public BusinessApiAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ClockResponseModel> GetClockResponseAsync()
    {
        var response = await _httpClient.GetAsync("/clock");
        response.EnsureSuccessStatusCode(); // 200 - 299

        var model = await response.Content.ReadFromJsonAsync<ClockResponseModel>();

        return model;
    }
}

public record ClockResponseModel
{
    public bool IsOpen { get; set; }
    public DateTime? NextOpenTime { get; set; }
}