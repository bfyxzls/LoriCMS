
using LoriCMS.Application.Commands;
using LoriCMS.Application.DTO;
using LoriCMS.Domain.AggregatesModel;
using LoriCMS.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LindAgile.Web.Controllers
{
    public class WorkController : Controller
    {
        IRepository<Work_Item> workItemRepository;
        public WorkController()
        {
            workItemRepository = new EFRepository<Work_Item>();
            workItemRepository.SetDataContext(new LindContext());
        }

        // GET: Work
        public ActionResult Index()
        {
            return View(workItemRepository.GetModel().ToList().MapTo<Work_ItemDTO>());
        }

        // GET: Work/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Work/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Work/Create
        [HttpPost]
        public ActionResult Create(Work_ItemDTO workItem)
        {
            if (ModelState.IsValid)
            {
                LoriCMS.ServiceBus.BusManager.Instance.Publish(new CreateWorkCommand
                {
                    Work_ItemDTO = workItem
                });
                return RedirectToAction("Index");
            }

            return View(workItem);
        }

        // GET: Work/Edit/5
        public ActionResult Edit(int id)
        {
            return View(workItemRepository.Find(id));
        }

        // POST: Work/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Work_Item workItem)
        {
            try
            {
                var old = workItemRepository.Find(id);
                old.Content.Title = workItem.Content.Title;
                old.Content.Detail = workItem.Content.Detail;
                old.StartTime = workItem.StartTime;
                old.EndTime = workItem.EndTime;
                old.DataUpdateDateTime = DateTime.Now;
                old.Schedule = workItem.Schedule;
                workItemRepository.Update(old);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: Work/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Work/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
