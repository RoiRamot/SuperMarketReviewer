using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Windows.Forms;
using SupermarketReviewer.Core.Models;
using SupermarketReviewer.Core.Models.DbModels;

namespace SupermarketReviewer.Server.Controllers
{
    public class BrandsController : ApiController
    {
        [HttpPost]
        public void Postbrands([FromBody] List<Brand> brands)
        {
            using (var db = new BrandContext())
            {

                db.Database.Delete();
                try
                {
                    foreach (var brand in brands)
                    {
                        var brandForDb = new BrandForDb(brand);
                        db.Brands.Add(brandForDb);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception)
                        {
                            var error = new HttpResponseMessage(HttpStatusCode.NotFound)
                            {
                                Content = new StringContent(string.Format("Unable to save to data base")),
                                ReasonPhrase = "Unable to save to data base"
                            };
                            throw new HttpResponseException(error);
                        }

                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

                MessageBox.Show("finished uploading to DB");

            }

        }

        //var saveBrandForDb = new List<BrandForDb>();
            //var saveStoreForDb = new List<StoreForDb>();
            //foreach (var brand in brands)
            //{

            //    saveBrandForDb.Add(new BrandForDb(brand, saveStoreForDb));
            //    saveStoreForDb.Clear();
            //}
            //using (var db = new BrandContext())

            //{
            //    try
            //    {
            //        foreach (var brand in saveBrandForDb)
            //        {
            //            db.Brands.Add(brand);
            //        }
            //        try
            //        {
            //            db.SaveChanges();
            //        }
            //        catch (Exception)
            //        {

            //            throw;
            //        }

            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }
            //}

        
    }
}

