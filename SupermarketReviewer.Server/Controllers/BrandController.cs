using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
    }
}

