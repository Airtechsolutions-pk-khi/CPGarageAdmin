using BAL.Repositories;
using DAL.DBEntities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPGarageAdmin.Controllers
{
    public class modelController : Controller
    {
        modelRepository modelRepo;
        public modelController()
        {
            modelRepo = new modelRepository(new Garage_LiveEntities());

        }
        // GET: users
        public ActionResult list()
        {            
            return View(modelRepo.GetModels());                        
        }

        [HttpGet]
        public ActionResult add(int? id)
        {
            var makeList = modelRepo.DBContext.Makes.Where(x => x.StatusID == 1).ToList();
            ViewBag.makeList = new SelectList(makeList, "MakeID", "Name");
            //ViewBag.StateList = customerRepo.getRoleList().Select(x => new { x.ROLEID, x.ROLENAME }).ToList();
            //var user = customerRepo.getCustomerById(id);
            try
            {
                if (id != null)
                {
                    var data = modelRepo.GetModelbyid(int.Parse(id.ToString()));
                    return View(data);
                }
            }
            catch (Exception ex)
            {

            }

            return View();
        }
        [HttpPost]
        public ActionResult Save(ModelsViewModel modal, HttpPostedFileBase imgfile)
        {
            string path = "";
            bool isSuccess = false;
            ModelState.Remove("ModelID");
            if (ModelState.IsValid)
            {
                if (modal.ModelID > 0)
                {
                    if (modal.ImagePath != null || modal.ImagePath != "")
                    {
                        path = modal.ImagePath;
                    }
                    if (imgfile != null)
                    {
                        path = uploadimgfile(imgfile);
                    }
                   
                    var data = modelRepo.edit(modal, path);

                    if (data == 1)
                        return RedirectToAction("list");
                    else
                        return RedirectToAction("list");

                }
                else
                {
                    path = uploadimgfile(imgfile);
                    var data = modelRepo.add(modal, path);

                    if (data == 1)
                        return RedirectToAction("list");
                    else
                        return RedirectToAction("list");
                }

            }
            else
            {

                return RedirectToAction("list");
            }
            
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                var data = modelRepo.delete(id, 0);

                return RedirectToAction("list", data);
            }
            else
            {

                return View();
            }
        }
 
        public ActionResult listMake()
        {
            var makeList = modelRepo.DBContext.Makes.Where(x => x.StatusID == 1 || x.StatusID == 2).ToList();
            ViewBag.MakeList = makeList;
            return View(modelRepo.GetMake());
        }

        [HttpGet]
        public ActionResult addMake(int? id)        
        {            
            try
            {
                if (id != null)
                {
                    var data = modelRepo.GetMakebyid(int.Parse(id.ToString()));
                     
                    return View(data);
                }
            }
            catch (Exception ex)
            {

            }

            return View();
        }
        [HttpPost]
        public ActionResult SaveMake(MakeViewModel modal, HttpPostedFileBase imgfile)
        {
            string path = "";
             
            ModelState.Remove("MakeID");
            if (ModelState.IsValid)
            {
                if (modal.MakeID > 0)
                {
                    if (modal.ImagePath != null || modal.ImagePath != "")
                    {
                         path = modal.ImagePath;
                    }
                    if(imgfile != null)
                    { 
                    path = uploadimgfile(imgfile);
                    }
                    var data = modelRepo.editMake(modal, path);

                    if (data == 1)
                        return RedirectToAction("listMake");
                    else
                        return RedirectToAction("listMake");

                }
                else
                {
                    path = uploadimgfile(imgfile);
                    var data = modelRepo.addMake(modal, path);

                    if (data == 1)
                        return RedirectToAction("listMake");

                }

            }
            else
            {

                return RedirectToAction("listMake");
            }
            return RedirectToAction("listMake");
        }

        public string uploadimgfile(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png") || extension.ToLower().Equals(".svg"))
                {
                    try
                    {

                        path = Path.Combine(Server.MapPath("/assets/Uploads"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "/assets/Uploads/" + random + Path.GetFileName(file.FileName);

                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }

            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }



            return path;
        }


        [HttpGet]
        public ActionResult DeleteMake(int id)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                var data = modelRepo.deleteMake(id, 0);

                return RedirectToAction("listMake", data);
            }
            else
            {

                return View();
            }
        }
    }
}
