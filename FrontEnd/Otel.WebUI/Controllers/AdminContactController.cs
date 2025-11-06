using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using Otel.EntityLayer.Concrete;
using Otel.WebUI.DTOs.ContactDTO;

namespace Otel.WebUI.Controllers
{
    public class AdminContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Inbox([FromQuery] int? categoryId)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(categoryId != null
                                   ? $"https://localhost:44355/api/Contact/category/{categoryId}"
                                   : "https://localhost:44355/api/Contact");


            if (!responseMessage.IsSuccessStatusCode)
                return View();

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<InboxContactDTO>>(jsonData);
            ViewBag.InboxCount = values!.Count;
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> ReadEmail([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid email ID.");

            var client = _httpClientFactory.CreateClient();


            var responseMessage = await client.GetAsync($"https://localhost:44355/api/Contact/{id}");

            if (!responseMessage.IsSuccessStatusCode)
                return NotFound("Email not found.");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<DetailContactDTO>(jsonData);

            return View(value);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEmail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:44355/api/Contact/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Inbox");

            ModelState.AddModelError(string.Empty, "An error occurred while deleting the email.");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> OutgoingMessages()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44355/api/Contact/replied-count");

            if (!responseMessage.IsSuccessStatusCode)
                return View();

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<OutgoingMessagesDTO>>(jsonData);

            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> SendEmail([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid email ID.");

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync($"https://localhost:44355/api/Contact/{id}");

            if (!responseMessage.IsSuccessStatusCode)
                return NotFound("Email not found.");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<SendMessageDTO>(jsonData);

            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(SendMessageDTO sendMessageDTO)
        {
            if (sendMessageDTO == null || sendMessageDTO.ContactID <= 0)
            {
                ModelState.AddModelError(string.Empty, "Invalid message data.");
                return View(sendMessageDTO);
            }

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync($"https://localhost:44355/api/Contact/{sendMessageDTO.ContactID}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                return NotFound("Email not found.");
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var contactEntity = JsonConvert.DeserializeObject<Contact>(jsonData);

            if (contactEntity == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to retrieve contact information.");
                return View(sendMessageDTO);
            }

            contactEntity.ResponseMessage = sendMessageDTO.ResponseMessage;
            contactEntity.IsReplied = true;
            contactEntity.ResponseDate = DateTime.Now;

            var updatedJsonData = JsonConvert.SerializeObject(contactEntity);
            var stringContent = new StringContent(updatedJsonData, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:44355/api/Contact", stringContent);

            if (response.IsSuccessStatusCode)
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("Hasan Uslu - CEO OF OTELIER", "dumenmail123@gmail.com"));
                mimeMessage.To.Add(new MailboxAddress("Guest", contactEntity.Email));

                var bodyBuilder = new BodyBuilder
                {
                    TextBody = sendMessageDTO.ResponseMessage
                };
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = "About the email you sent";

                using (var smtpClient = new SmtpClient())
                {
                    await smtpClient.ConnectAsync("smtp.gmail.com", 587, false);
                    await smtpClient.AuthenticateAsync("dumenmail123@gmail.com", "klryoieeoreqdqro");
                    await smtpClient.SendAsync(mimeMessage);
                    await smtpClient.DisconnectAsync(true);
                }

                return RedirectToAction("Inbox");
            }

            ModelState.AddModelError(string.Empty, "An error occurred while sending the email.");
            return View(sendMessageDTO);
        }

    }
}
