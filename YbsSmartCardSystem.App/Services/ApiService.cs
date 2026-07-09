using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using YbsSmartCardSystem.Domain;
using YbsSmartCardSystem.Domain.Features.BusPayment.Models;
using YbsSmartCardSystem.Domain.Features.Card.Models;
using YbsSmartCardSystem.Domain.Features.Package.Models;
using YbsSmartCardSystem.Domain.Features.Topup.Models;
using YbsSmartCardSystem.Domain.Features.Transaction.Models;

namespace YbsSmartCardSystem.App.Services;

public class ApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _baseUrl;

    public ApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _baseUrl = configuration.GetValue<string>("BackendApiUrl")!;
    }

    private async Task<TResponse> Execute<TRequest, TResponse>(string endpoint, HttpMethod httpMethod, TRequest? request)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(_baseUrl);

        using var message = new HttpRequestMessage(httpMethod, endpoint);
        if (request is not null)
        {
            message.Content = JsonContent.Create(request);
        }

        using var response = await httpClient.SendAsync(message);
        return (await response.Content.ReadFromJsonAsync<TResponse>())!;
    }

    public Task<Result<CardListResponseModel>> GetCards(CardListRequestModel request)
    {
        string url = QueryHelpers.AddQueryString(ApiEndpoints.CardList, new Dictionary<string, string?>
        {
            ["cardNo"] = request.CardNo,
            ["mobileNo"] = request.MobileNo,
            ["name"] = request.Name,
            ["pageNo"] = request.PageNo.ToString(),
            ["pageSize"] = request.PageSize.ToString()
        });

        return Execute<CardListRequestModel, Result<CardListResponseModel>>(url, HttpMethod.Get, null);
    }

    public Task<Result<CardDetailResponseModel>> GetCard(int cardId)
    {
        return Execute<object, Result<CardDetailResponseModel>>($"api/Card/{cardId}", HttpMethod.Get, null);
    }

    public Task<Result<CardCreateResponseModel>> CardCreate(CardCreateRequestModel request)
    {
        return Execute<CardCreateRequestModel, Result<CardCreateResponseModel>>(ApiEndpoints.CreateCard, HttpMethod.Post, request);
    }

    public Task<Result<PackageListResponseModel>> GetPackages(PackageListRequestModel request)
    {
        string url = QueryHelpers.AddQueryString(ApiEndpoints.PackageList, new Dictionary<string, string?>
        {
            ["pageNo"] = request.PageNo.ToString(),
            ["pageSize"] = request.PageSize.ToString()
        });

        return Execute<PackageListRequestModel, Result<PackageListResponseModel>>(url, HttpMethod.Get, null);
    }

    public Task<Result<PackageDetailResponseModel>> GetPackage(int packageId)
    {
        return Execute<object, Result<PackageDetailResponseModel>>($"api/Package/{packageId}", HttpMethod.Get, null);
    }

    public Task<Result<PackageCreateResponseModel>> CreatePackage(PackageCreateRequestModel request)
    {
        return Execute<PackageCreateRequestModel, Result<PackageCreateResponseModel>>(ApiEndpoints.PackageList, HttpMethod.Post, request);
    }

    public Task<Result<PackageUpdateResponseModel>> UpdatePackage(int packageId, PackageUpdateRequestModel request)
    {
        request.PackageId = packageId;
        return Execute<PackageUpdateRequestModel, Result<PackageUpdateResponseModel>>($"api/Package/{packageId}", HttpMethod.Put, request);
    }

    public Task<Result<PackageDeleteResponseModel>> DeletePackage(int packageId)
    {
        return Execute<object, Result<PackageDeleteResponseModel>>($"api/Package/{packageId}", HttpMethod.Delete, null);
    }

    public Task<Result<TopupResponseModel>> Topup(TopupRequestModel request)
    {
        return Execute<TopupRequestModel, Result<TopupResponseModel>>(ApiEndpoints.Topup, HttpMethod.Post, request);
    }

    public Task<Result<BusTapResponseModel>> BusTap(BusTapRequestModel request)
    {
        return Execute<BusTapRequestModel, Result<BusTapResponseModel>>(ApiEndpoints.BusTap, HttpMethod.Post, request);
    }

    public Task<Result<TransactionListResponseModel>> GetTransactions(TransactionListRequestModel request)
    {
        string url = QueryHelpers.AddQueryString(ApiEndpoints.TransactionList, new Dictionary<string, string?>
        {
            ["cardNo"] = request.CardNo,
            ["pageNo"] = request.PageNo.ToString(),
            ["pageSize"] = request.PageSize.ToString()
        });

        return Execute<TransactionListRequestModel, Result<TransactionListResponseModel>>(url, HttpMethod.Get, null);
    }

    public Task<Result<TransactionDetailResponseModel>> GetTransaction(int transactionId)
    {
        return Execute<object, Result<TransactionDetailResponseModel>>($"api/Transaction/{transactionId}", HttpMethod.Get, null);
    }
}

public class ApiEndpoints
{
    public const string CardList = "api/Card";
    public const string CreateCard = "api/Card";
    public const string PackageList = "api/Package";
    public const string Topup = "api/Topup";
    public const string BusTap = "api/BusPayment/tap";
    public const string TransactionList = "api/Transaction";
}
