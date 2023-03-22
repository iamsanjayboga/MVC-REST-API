using ASPNET_WEBAPI_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ASPNET_WEBAPI_CRUD.Controllers
{
    public class CrudMVCController : Controller
    {
        // GET: CrudMVC
        HttpClient client = new HttpClient();

        [HttpGet]
        public ActionResult Index()
        {
            List<User> users = new List<User>();
            client.BaseAddress = new Uri("http://localhost:49942/api/CrudApi");
            //What ever is used after client.-->Delete, Post, Put,Get this specify which 
            //Action verb to hit in web API

            //This will call IHttpActionResult Getuserlist() because we dont have any parameter
            //it is just a controller name 
            var response = client.GetAsync("CrudApi");

            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<User>>();
                display.Wait();
                users = display.Result;
            }
            return View(users);
        }

        //Here Initially GetDetails was not working as In Index page Details button action was
        //given as Details so I went and changed to GetDetails then it worked
        [HttpGet]
        public ActionResult GetDetails(int id)
        {
            User users = null;
            
            client.BaseAddress = new Uri("http://localhost:49942/api/CrudApi");
            //What ever is used after client.-->Delete, Post, Put,Get this specify which 
            //Action verb to hit in web API

            //This will call IHttpActionResult Getuserlist(int id) because it has one parameter
            //it is just a controller name with int iD
            var response = client.GetAsync("CrudApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                //In above once we het data from WebAPI
                //we need to specify what type of data is example <User>
                var display = test.Content.ReadAsAsync<User>();
                display.Wait();
                users = display.Result;
            }
            return View(users);
        }

        //This is without parameter which will be called to navigate to page
        //therefore [httpPost] is not mentioned above
        public ActionResult Create()
        {
            //This can be empty or we can return the vew we want to go to
            return View("Create");
        }

        [HttpPost]//This must match the same Http Header which is used in Web API
        public ActionResult Create(User users)
        {
            client.BaseAddress = new Uri("http://localhost:49942/api/CrudApi");
            //What ever is used after client.-->Delete, Post, Put,Get this specify which 
            //Action verb to hit in web API

            //In WEb APi Code -> IHttpActionResult InsertUser(User users) is expecting user obj
            //so while sending data to web api we need to specify type of data
            //there we say PostAsJsonAsync<User>
            var response = client.PostAsJsonAsync<User>("CrudApi",users);
            response.Wait();

            var test = response.Result;
            if(test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Create");
        }


        //Here why are we creating this method with id ?
        //because we we click on edit button in index page it goes to "CrudApi/Edit/1" -> by Default
        //So to get all the details based on id and populate in fields we write this method
        
        public ActionResult Edit(int id)
        {
            //This is similar to -GetDetails-

            User users = null;
            client.BaseAddress = new Uri("http://localhost:49942/api/CrudApi");
            //What ever is used after client.-->Delete, Post, Put,Get this specify which 
            //Action verb to hit in web API

            //This will call IHttpActionResult Getuserlist(int id) because it has one parameter
            //it is just a controller name with int iD
            var response = client.GetAsync("CrudApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                //In above once we het data from WebAPI
                //we need to specify what type of data is example <User>
                var display = test.Content.ReadAsAsync<User>();
                display.Wait();
                users = display.Result;
            }
            return View(users);
        }

        [HttpPost] //Here Why Post? Why not Put because not sure
        public ActionResult Edit(User users)
        {
            //This is Similar to -Create- Small Change "PutAsJsonAsync"

            client.BaseAddress = new Uri("http://localhost:49942/api/CrudApi");
            //What ever is used after client.-->Delete, Post, Put,Get this specify which 
            //Action verb to hit in web API


            var response = client.PutAsJsonAsync<User>("CrudApi", users); //but here its is put
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Edit");
        }


        public ActionResult Delete(int id)
        {
            User users = null;
            client.BaseAddress = new Uri("http://localhost:49942/api/CrudApi");
            //What ever is used after client.-->Delete, Post, Put,Get this specify which 
            //Action verb to hit in web API

            //This will call IHttpActionResult Getuserlist(int id) because it has one parameter
            //it is just a controller name with int iD
            var response = client.GetAsync("CrudApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if(test.IsSuccessStatusCode)
            {
                //In above once we het data from WebAPI
                //we need to specify what type of data is example <User>
                var display = test.Content.ReadAsAsync<User>();
                display.Wait();
                users = display.Result;
            }
            return View(users);
        }

        //Why action name because with same name and paratmenr already method exists
        //therefore we want to tell controller this is delete action only
        [HttpPost, ActionName("Delete")] 
        public ActionResult DeleteConfimed(int id)
        {
            
            client.BaseAddress = new Uri("http://localhost:49942/api/CrudApi");
            //What ever is used after client.-->Delete, Post, Put,Get this specify which 
            //Action verb to hit in web API

            //Here why not "CrudApi?id=" and Why "CrudApi/" because already we came to 
            //this url ?id= is used to navigate to url called query string
            //this is just a confirmation as we are already in CrudApi/id
            //therefore we are using this

            //in WEb APi code --> IHttpActionResult Deleteuser(int id)
            //it accepts int id therefore we need to send int id not object
            var response = client.DeleteAsync("CrudApi/" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Delete");
        }
    }
}