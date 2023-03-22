using ASPNET_WEBAPI_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace ASPNET_WEBAPI_CRUD.Controllers
{
    public class CrudApiController : ApiController
    {
        CarrerEngineEntities db = new CarrerEngineEntities();

        [System.Web.Http.HttpGet]
        public IHttpActionResult Getuserlist()
        {
            List<User> li = db.Users.ToList();
            return Ok(li);
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetuserlistByID(int Id)
        {
            var person = db.Users.Where(model => model.UserID == Id).FirstOrDefault();
            return Ok(person);
        }

        //while inserting means posting or updating measn put we need entie data in obj form
        [System.Web.Http.HttpPost]
        public IHttpActionResult InsertUser(User users)
        {
            db.Users.Add(users);
            db.SaveChanges();
            return Ok();
        }


        //while inserting means posting or updating measn put we need entie data in obj form
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateUser(User users)
        {
            var person = db.Users.Where(model => model.UserID == users.UserID).FirstOrDefault();

            //SIMPLE OPTION1
            //db.Entry(person).State = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();

            //OPTION 2
            if (person != null)
            {
                person.UserID = users.UserID;
                person.FirstName = users.FirstName;
                person.LastName =  users.LastName;
                person.Email = users.Email;
                person.AboutMe = users.AboutMe;
                person.Role = users.Role;
                
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        /*
        Why this failed is because data is not coming in object
        its coming in id only
        If you see mvc code
        var response = client.DeleteAsync("CrudApi/" + id.ToString());
        data is coming in CrudApi/id, So we need to delete with Id

        [System.Web.Http.HttpDelete]
        public IHttpActionResult Deleteuser(User users)
        {
            db.Users.Remove(users);
            db.SaveChanges();
            return Ok();
        }
        */
        
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Deleteuser(int id)
        {
            var person = db.Users.Where(model => model.UserID == id).FirstOrDefault();
            if(person != null)
            {
                //Option 1
                //db.Entry(person).State = System.Data.Entity.EntityState.Deleted;
                
                //Option 2
                db.Users.Remove(person);
                db.SaveChanges();
            }
            
            return Ok();
        }
    }
}
