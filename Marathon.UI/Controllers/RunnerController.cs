using Marathon.UI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Marathon.UI.Models;

namespace Marathon.UI.Controllers
{
	public class RunnerController : Controller
	{
		private readonly IHttpClientFactory httpClientFactory;

		public RunnerController(IHttpClientFactory httpClientFactory)
        {
			this.httpClientFactory=httpClientFactory;
		}
        public async Task<IActionResult> Index()
		{
			List<RunnerDto> response = new List<RunnerDto>();

			try
			{
				// Get All Regions from Web API
				var client = httpClientFactory.CreateClient();

				var httpResponseMessage = await client.GetAsync("https://localhost:7298/api/runner");

				httpResponseMessage.EnsureSuccessStatusCode();

				response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RunnerDto>>());
			}
			catch (Exception ex)
			{
				// Log the exception
			}

			return View(response);
		}
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRunnerViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7298/api/runner"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<RunnerDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Runner");
            }

            return View();
        }
    }
}
