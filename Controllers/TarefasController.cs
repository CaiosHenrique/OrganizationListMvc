using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using OrganizationListMvc.Models;

namespace OrganizationListMvc.Controllers
{

        public class OrganizationsController : Controller
        {
            public string uriBase = "http://Listafofinha.somee.com/Tarefas/";
            public HttpClient httpClient = new HttpClient();

            [HttpGet]
        public ActionResult IndexTarefas()
        {
            return View("IndexTarefas");
        }
        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                string uriComplementar = "GetAll";
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<TarefaViewModel> listaTarefa = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<TarefaViewModel>>(serialized));

                    return View(listaTarefa);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {

                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("IndexTarefas");
            }
        }
[HttpPost]
        public async Task<ActionResult> CadastrarAsync(TarefaViewModel p)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(JsonConvert.SerializeObject(p));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Tarefas {0}, Id {1} salvo com sucesso!", p.IdTarefa, serialized);
                    return RedirectToAction("IndexTarefas");
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Create");
            }
        }
[HttpGet]
        public async Task<ActionResult> DetalhesAsync(int? IdTarefa)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.GetAsync(uriBase + IdTarefa.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TarefaViewModel p = await Task.Run(() =>
                    JsonConvert.DeserializeObject<TarefaViewModel>(serialized));
                    return View(p);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("IndexTarefas");
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditarAsync(int? IdTarefa)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.GetAsync(uriBase + IdTarefa.ToString());

                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TarefaViewModel p = await Task.Run(() =>
                    JsonConvert.DeserializeObject<TarefaViewModel>(serialized));
                    return View(p);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("IndexTarefas");
            }
        }
[HttpPost]
        public async Task<ActionResult> EditarAsync(TarefaViewModel p)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var content = new StringContent(JsonConvert.SerializeObject(p));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await httpClient.PutAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] =
                        string.Format("Dieta {0}, atualizado com sucesso!", p.DescTarefa);

                    return RedirectToAction("IndexTarefas");
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {

                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("IndexTarefas");
            }
        }
[HttpGet]
        public async Task<ActionResult> DeleteAsync(int IdTarefa)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Barer", token);

                HttpResponseMessage response = await httpClient.DeleteAsync(uriBase + IdTarefa.ToString());
                String serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Tarefa Id {0} removida com sucesso!", IdTarefa);
                    return RedirectToAction("IndexTarefas");
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {

                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("IndexTarefas");

            }

        }
        
        }

}