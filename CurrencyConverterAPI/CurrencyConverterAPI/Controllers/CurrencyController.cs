using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using CurrencyConverterAPI.Models;

[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CurrencyController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("listquotes")]
    public async Task<IActionResult> GetCurrencyQuotes()
    {
        var client = _httpClientFactory.CreateClient("CurrencyConverter");
        var response = await client.GetAsync("https://currency-exchange.p.rapidapi.com/listquotes");
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();

        // Parse JSON into a list of quotes
        var quotes = JsonConvert.DeserializeObject<List<string>>(body);

        var result = new CurrencyQuoteList
        {
            Quotes = quotes
        };

        return Ok(result);
    }

    [HttpGet("exchange")]
    public async Task<IActionResult> GetExchangeRate([FromQuery] string from, [FromQuery] string to, [FromQuery] decimal quantity)
    {
        var client = _httpClientFactory.CreateClient("CurrencyConverter");
        var url = $"https://currency-exchange.p.rapidapi.com/exchange?from={from}&to={to}&q={quantity}";
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();

        // Parse the exchange rate from the response
        decimal exchangeRate = decimal.Parse(body);

        var result = new CurrencyExchange
        {
            From = from,
            To = to,
            Quantity = quantity,
            ExchangeRate = exchangeRate
        };

        return Ok(result);
    }
}
