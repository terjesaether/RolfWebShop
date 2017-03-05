using RolfWebShop.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RolfWebShop.ViewModels
{
    public class CreateProductViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public CreateProductViewModel()
        {
            Product = new Product();
            SelectListOfCategories = GetSelectListCategories();
            SelectListOfProducers = GetSelectListProducers();
            SelectListOfUnits = GetSelectListUnits();
        }

        public Product Product { get; set; }


        public IEnumerable<SelectListItem> SelectListOfProducers { get; set; }
        public IEnumerable<SelectListItem> SelectListOfCategories { get; set; }
        public IEnumerable<SelectListItem> SelectListOfUnits { get; set; }

        public IEnumerable<SelectListItem> GetSelectListProducers()
        {

            foreach (var item in db.Producers)
            {
                yield return new SelectListItem
                {
                    Value = item.ProducerId.ToString(),
                    Text = item.Name
                };

            }
        }

        public IEnumerable<SelectListItem> GetSelectListCategories()
        {

            foreach (var item in db.Categories)
            {
                yield return new SelectListItem
                {
                    Value = item.CategoryId.ToString(),
                    Text = item.Name
                };

            }
        }

        public IEnumerable<SelectListItem> GetSelectListUnits()
        {

            foreach (var item in db.Units)
            {
                yield return new SelectListItem
                {
                    Value = item.UnitId.ToString(),
                    Text = item.Name
                };

            }
        }
    }
}