using DAL.DBEntities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace BAL.Repositories
{
    public class uploadRepository : BaseRepository
    {
        public uploadRepository()
            : base()
        {
            DBContext = new Garage_LiveEntities();
        }

        public uploadRepository(Garage_LiveEntities contextDB)
            : base(contextDB)
        {
            DBContext = contextDB;
        }
        public bool IsBase64Encoded(String str)
        {
            try
            {
                // If no exception is caught, then it is possibly a base64 encoded string
                byte[] data = Convert.FromBase64String(str);
                // The part that checks if the string was properly padded to the
                // correct length was borrowed from d@anish's solution
                return (str.Replace(" ", "").Length % 4 == 0);
            }
            catch
            {
                // If exception is caught, then it is not a base64 encoded string
                return false;
            }
        }
        public string uploadFiles(string _bytes,string foldername)
        {
            try
            {
                if (_bytes != null && _bytes.ToString() != "")
                {

                    byte[] bytes = Convert.FromBase64String(_bytes.Replace("data:image/png;base64,", "")
                        .Replace("data:image/jpg;base64,", "")
                        .Replace("data:image/jpeg;base64,", "")
                        .Replace("data:image/svg+xml;base64,", ""));
                    string filePath = "/Upload/"+ foldername + "/" + Path.GetFileName(Guid.NewGuid() + ".jpg");

                    System.IO.File.WriteAllBytes(System.Web.HttpContext.Current.Server.MapPath(filePath), bytes);

                    _bytes = filePath;

                }
                else
                {
                    _bytes = "";
                }
            }
            catch (Exception ex)
            {
                _bytes = "";
            }
            return _bytes;
        }
    }
}
