
using DAL.DBEntities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Twilio.Rest.Api.V2010.Account;
using Twilio;
using System.Web;
using System.IO;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Data;
using Twilio.TwiML.Messaging;
using WebAPICode.Helpers;
using System.Web.Razor.Tokenizer;
using System.Web.Configuration;
using System.Web.Mvc;

namespace BAL.Repositories
{
    public class customersRepository : BaseRepository
    {
        private DataTable _dt;
        public customersRepository()
             : base()
        {
            DBContext = new Garage_LiveEntities();
        }
        public customersRepository(Garage_LiveEntities contextDB)
            : base(contextDB)
        {
            DBContext = contextDB;
        }

        public List<User> GetAll()
        {
            try
            {
                var lst = new List<User>();

                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetCustomer_CP", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<User>>();
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SelectListItem> GetCountries()
        {
            return DBContext.Countries
                .Select(c => new SelectListItem
                {
                    Text = c.Name,  // Display Name
                    Value = c.Code  // Value to send
                })
                .ToList();
        }
        public CustomerViewModel GetCustomerbyid(int id)
        {
            try
            {
                var data = DBContext.Users.Where(x => x.UserID == id)
                    .AsEnumerable().Select(r => new CustomerViewModel
                    {
                        FirstName = r.FirstName,
                        UserID = r.UserID,
                        UserName = r.UserName,
                        LastName = r.LastName,
                        ImagePath = r.ImagePath,
                        Password = new clsCryption().EncryptDecrypt(r.Password, "decrypt"),
                        Company = r.Company,
                        BusinessType = r.BusinessType,
                        Email = r.Email,
                        ContactNo = r.ContactNo,
                        CityID = r.CityID,
                        CountryID = r.CountryID,
                        Website = r.Website,
                        Subscribe = r.Subscribe,
                        RoleID = r.RoleID,
                        TimeZoneID = r.TimeZoneID,
                        LastUpdatedBy = r.LastUpdatedBy,
                        LastUpdatedDate = r.LastUpdatedDate,
                        StatusID = r.StatusID == 1 ? true : false,
                        CompanyCode = r.CompanyCode,
                        CreatedDate = r.CreatedDate,
                        States = r.States,
                        Zipcode = r.Zipcode,
                        VATNO = r.VATNO,
                        Address = r.Address,
                        IsSMSActivate = r.IsSMSCheckoutAddOn == null ? false : Convert.ToBoolean(r.IsSMSCheckoutAddOn.ToString()),
                        IsGarageGo = r.IsGarageGo == null ? false : Convert.ToBoolean(r.IsGarageGo.ToString()),
                        IsCashier = r.IsCashier == null ? false : Convert.ToBoolean(r.IsCashier.ToString()),
                        //AllowNegativeInventory = r.AllowNegativeInventory == null ? false : Convert.ToBoolean(r.AllowNegativeInventory.ToString()),
                        //IsOdoo = r.IsOdoo == null ? false : Convert.ToBoolean(r.IsOdoo.ToString()),
                        IsAccountingAddons = r.IsAccountingAddons == null ? false : Convert.ToBoolean(r.IsAccountingAddons.ToString()),
                        IsYakeen = r.IsYakeen == null ? false : Convert.ToBoolean(r.IsYakeen.ToString()),
                        IsMojaz = r.IsMojaz == null ? false : Convert.ToBoolean(r.IsMojaz.ToString()),
                        IsDefaultCar = r.IsDefaultCar == null ? false : Convert.ToBoolean(r.IsDefaultCar.ToString()),
                        LocationID = r.Locations.FirstOrDefault().LocationID,
                        LocationName = r.Locations.FirstOrDefault().Name,
                        LocationAddress = r.Locations.FirstOrDefault().Address,
                        LocationContactNo = r.Locations.FirstOrDefault().ContactNo,
                        LocationEmail = r.Locations.FirstOrDefault().Email,
                        Currency = r.Locations.FirstOrDefault().Currency,
                        Tax = r.Tax,
                        PackageInfoID = r.UserPackageDetails.Count == 0 ? 0 : r.UserPackageDetails.FirstOrDefault().PackageInfoID,
                        //CountryList = GetCountries(),
                        //ExpiryDate = r.UserPackageDetails.FirstOrDefault().ExpiryDate,
                    })
                  .FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                //_baseRepo.ErrorLog(ex, "loginRepository/GetLoginInfo", "Exception");
                return new CustomerViewModel();
            }
        }

        public int edit(CustomerViewModel modal)
        {
            using (var dbContextTransaction = DBContext.Database.BeginTransaction())
            {

                try
                {
                    if (modal.UserID > 0)
                    {
                        User _user = DBContext.Users.Where(x => x.UserID == modal.UserID).FirstOrDefault();
                        _user.FirstName = modal.FirstName;
                        _user.LastName = modal.LastName;
                        _user.Email = modal.Email;
                        _user.ContactNo = modal.ContactNo;
                        _user.UserName = modal.Email;
                        _user.Company = modal.Company;
                        _user.IsCashier = modal.IsCashier;
                        _user.IsGarageGo = modal.IsGarageGo;
                        _user.IsYakeen = modal.IsYakeen;
                        _user.IsMojaz = modal.IsMojaz;
                        _user.IsDefaultCar = modal.IsDefaultCar;
                        _user.BusinessType = modal.BusinessType;
                        _user.Password = new clsCryption().EncryptDecrypt(modal.Password, "encrypt");
                        _user.IsSMSCheckoutAddOn = modal.IsSMSActivate;
                        _user.StatusID = modal.StatusID == true ? 1 : 2;
                        _user.PackageInfoID = modal.PackageInfoID;
                        _user.IsAccountingAddons = modal.IsAccountingAddons;
                        _user.CreatedDate = modal.CreatedDate;
                        DBContext.Entry(_user).State = EntityState.Modified;
                        DBContext.UpdateOnly<User>(_user, x =>
                       x.FirstName,
                        x => x.LastName,
                        x => x.Email,
                        x => x.Company,
                        x => x.Password,
                        x => x.UserName,
                        x => x.ContactNo,
                        x => x.IsSMSCheckoutAddOn,
                        x => x.IsGarageGo,
                        x => x.IsCashier,
                        x => x.IsYakeen,
                        x => x.IsMojaz,
                        x => x.IsDefaultCar,
                        x => x.BusinessType,
                        x => x.StatusID,
                        x => x.UserPackageDetails,
                        x => x.CreatedDate
                        );
                        Location _location = DBContext.Locations.Where(x => x.UserID == modal.UserID).FirstOrDefault();
                        _location.Name = modal.LocationName;
                        _location.Address = modal.LocationAddress;
                        _location.ContactNo = modal.LocationContactNo;
                        _location.Email = modal.LocationEmail;
                        DBContext.Entry(_location).State = EntityState.Modified;
                        DBContext.UpdateOnly<Location>(_location, 
                            y => y.Name,
                            y => y.Address,
                            y => y.ContactNo,
                            y => y.Email);
                        DBContext.SaveChanges();

                        if (modal.PackageInfoID == 0 || modal.PackageInfoID == null)
                        {
                            UserPackageDetail package = new UserPackageDetail();

                            package.UserID = modal.UserID;
                            package.PackageInfoID = modal.PackageInfoID;
                            package.StatusID = 1;
                            package.CreatedDate = DateTime.UtcNow.AddMinutes(180);
                            package.LastUpdatedDate = DateTime.UtcNow.AddMinutes(180);
                            package.ExpiryDate = modal.ExpiryDate;
                            DBContext.UserPackageDetails.Add(package);
                            DBContext.SaveChanges();
                        }
                        else if (modal.PackageInfoID != null)
                        {
                            //UserPackageDetail _package = DBContext.UserPackageDetails.Where(x => x.UserID = modal.UserID).FirstOrDefault();
                            UserPackageDetail _package = DBContext.UserPackageDetails
                                    .Where(x => x.UserID == modal.UserID)
                                    .FirstOrDefault();
                            _package.PackageInfoID = modal.PackageInfoID;
                            _package.UserID = modal.UserID;
                            _package.StatusID = modal.StatusID == true ? 1 : 2;
                            _package.LastUpdatedDate = DateTime.UtcNow.AddMinutes(180);
                            _package.ExpiryDate = modal.ExpiryDate;
                            DBContext.Entry(_package).State = EntityState.Modified;
                            DBContext.UpdateOnly<UserPackageDetail>(_package, x =>
                               x.PackageInfoID,
                            x => x.StatusID,
                            x => x.LastUpdatedDate,
                            x => x.ExpiryDate
                            );
                            DBContext.SaveChanges();
                        }
                    }

                    dbContextTransaction.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                }
            }
            return 0;
        }
        public int add(CustomerViewModel modal)
        {
            using (var dbContextTransaction = DBContext.Database.BeginTransaction())
            {
                try
                {
                    User _user = new User();
                    Location _location = new Location();
                    SubUser _subuser = new SubUser();
                    Receipt _receipt = new Receipt();

                    UserPackageDetail _package = new UserPackageDetail();

                    _user.FirstName = modal.FirstName;
                    _user.LastName = modal.LastName;
                    _user.Company = modal.Company;
                    _user.UserName = modal.Email;
                    _user.Email = modal.Email;
                    _user.ContactNo = modal.ContactNo;
                    _user.Password = new clsCryption().EncryptDecrypt(modal.Password, "encrypt");
                    _user.BusinessType = modal.BusinessType;
                    _user.Password = new clsCryption().EncryptDecrypt(modal.Password, "encrypt");
                    _user.Address = modal.LocationAddress;
                    _user.CityID = modal.ID;
                    _user.PackageInfoID = modal.PackageInfoID;
                    _user.CountryID = modal.CountryID;
                    _user.Subscribe = false;
                    _user.TimeZoneID = 54;
                    _user.Tax = 0;
                    _user.IsGarageGo = modal.IsGarageGo;
                    _user.IsCashier = modal.IsCashier;
                    _user.IsYakeen = modal.IsYakeen;
                    _user.IsMojaz = modal.IsMojaz;
                    _user.IsDefaultCar = modal.IsDefaultCar;
                    _user.StatusID = modal.StatusID == true ? 1 : 2;
                    _user.CompanyCode = "POS-" + randomstring(6);
                    _user.Address = modal.LocationAddress;
                    _user.LastUpdatedDate = DateTime.UtcNow.AddMinutes(180);
                    User data = DBContext.Users.Add(_user);
                    DBContext.SaveChanges();
                    if (data.UserID > 0)
                    {
                        _location.Name = modal.LocationName;
                        _location.Address = modal.LocationAddress;
                        _location.ContactNo = modal.LocationContactNo;
                        _location.Email = modal.LocationEmail;
                        _location.Name = modal.LocationName;
                        _location.TimeZoneID = 54;
                        _location.UserID = data.UserID;
                        _location.Open_Time = TimeSpan.Parse("09:00:00");
                        _location.Close_Time = TimeSpan.Parse("21:00:00");
                        _location.StatusID = 1;
                        _location.CompanyCode = _user.CompanyCode;
                        _location.Currency = modal.Currency;
                        Location dataLocation = DBContext.Locations.Add(_location);
                        DBContext.SaveChanges();

                        try
                        {
                            //add main store for location
                            var store = new Store();
                            store.StatusID = 1;
                            store.UserID = _location.UserID;
                            store.Contact = _location.ContactNo;
                            store.Address = _location.Address;
                            store.LastUpdatedDate = _location.LastUpdatedDate;
                            store.LastUpdatedBy = _location.LastUpdatedBy;
                            store.StoreLocationID = null;
                            store.Type = "Main Store";
                            store.Name = dataLocation.Name + "-mainstore";
                            DBContext.Stores.Add(store);
                            DBContext.SaveChanges();
                            //add store for location
                            store = new Store();
                            store.StatusID = 1;
                            store.UserID = _location.UserID;
                            store.Contact = _location.ContactNo;
                            store.Address = _location.Address;
                            store.LastUpdatedDate = _location.LastUpdatedDate;
                            store.LastUpdatedBy = _location.LastUpdatedBy;
                            store.StoreLocationID = dataLocation.LocationID;
                            store.Type = "Location Store";
                            store.Name = _location.Name + "-store";
                            DBContext.Stores.Add(store);
                            DBContext.SaveChanges();
                        }
                        catch (Exception e)
                        { }
                        if (dataLocation.LocationID > 0)
                        {
                            _subuser.FirstName = modal.FirstName;
                            _subuser.LastName = modal.LastName;
                            _subuser.UserName = modal.Email;
                            _subuser.Password = modal.Password;
                            _subuser.LocationID = dataLocation.LocationID;
                            _subuser.CityID = modal.ID;
                            _subuser.CountryID = modal.CountryID;
                            _subuser.Passcode = GenerateRandomNo();
                            _subuser.TimeZoneID = 54;
                            _subuser.StatusID = 1;
                            _subuser.LastUpdatedDate = DateTime.UtcNow.AddMinutes(180);
                            _subuser.CompanyCode = _user.CompanyCode;
                            _subuser.UserID = _user.UserID;
                            SubUser dataSubuser = DBContext.SubUsers.Add(_subuser);
                            DBContext.SaveChanges();
                        }
                        if (dataLocation.LocationID > 0)
                        {
                            _receipt.CompanyTitle = modal.Company;
                            _receipt.CompanyAddress = modal.Address;
                            _receipt.CompanyPhones = modal.ContactNo;
                            _receipt.LocationID = dataLocation.LocationID;
                            _receipt.StatusID = 1;
                            _receipt.IsActive = true;
                            _receipt.LastUpdatedDate = DateTime.UtcNow.AddMinutes(180);
                            Receipt dataSubuser = DBContext.Receipts.Add(_receipt);
                            DBContext.SaveChanges();
                        }
                        if (data.UserID != 0)
                        {
                            _package.UserID = data.UserID;
                            _package.PackageInfoID = modal.PackageInfoID;
                            _package.StatusID = 1;
                            _package.CreatedDate = DateTime.UtcNow.AddMinutes(180);
                            _package.LastUpdatedDate = DateTime.UtcNow.AddMinutes(180);
                            _package.ExpiryDate = modal.ExpiryDate;
                            UserPackageDetail datauser = DBContext.UserPackageDetails.Add(_package);
                            DBContext.SaveChanges();
                        }

                        dbContextTransaction.Commit();

                        try
                        {
                            var obj = new RoleGroup();
                            //data.LastUpdatedBy = 
                            //var forms = DBContext.Role_Forms.Where(x => x.StatusID == 1);
                            string[] arr = { "Manager", "Cashier", "Technician" };
                            foreach (var item in arr)
                            {
                                SqlParameter[] e = new SqlParameter[5];
                                e[0] = new SqlParameter("@GroupName", item);
                                e[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                                e[2] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                                e[3] = new SqlParameter("@StatusID", data.StatusID);
                                e[4] = new SqlParameter("@UserID", data.UserID);
                                obj.GroupID = int.Parse((new DBHelperGarageUAT().GetDatasetFromSP)("sp_InsertRoleGroup", e).Tables[0].Rows[0][0].ToString());
                                //obj.GroupID = int.Parse((new DBHelperGarageUAT().GetTableFromSP)("sp_InsertRoleGroup", e).ToString());

                                var New = item == "Manager" ? 1 : item == "Technician" ? 0 : item == "Cashier" ? 0 : 0;
                                var Edit = item == "Manager" ? 1 : item == "Technician" ? 0 : item == "Cashier" ? 0 : 0;
                                var Remove = item == "Manager" ? 1 : item == "Technician" ? 0 : item == "Cashier" ? 0 : 0;
                                var Access = item == "Manager" ? 1 : item == "Technician" ? 0 : item == "Cashier" ? 0 : 0;
                                var IsCashier = item == "Manager" ? 0 : item == "Technician" ? 0 : item == "Cashier" ? 1 : 0;

                                SqlParameter[] e3 = new SqlParameter[7];
                                e3[0] = new SqlParameter("@GroupID", obj.GroupID);
                                e3[1] = new SqlParameter("@New", New);
                                e3[2] = new SqlParameter("@Edit", Edit);
                                e3[3] = new SqlParameter("@Remove", Remove);
                                e3[4] = new SqlParameter("@Access", Access);
                                e3[5] = new SqlParameter("@StatusID", data.StatusID);
                                e3[6] = new SqlParameter("@IsCashier", IsCashier);
                                (new DBHelperGarageUAT().ExecuteNonQueryReturn)("sp_InsertRoleGroup_ADMIN", e3);
                            }
                        }
                        catch (Exception ex)
                        {
                            return 0;
                        }
                        return 1;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                }
            }
            return 0;
        }
        public string randomstring(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            bool flag = false;
            while (!flag)
            {

                var objdata = DBContext.Users.Where(c => c.CompanyCode == "POS-" + result).Any();
                if (DBContext.Users.Where(c => c.CompanyCode == "POS-" + result).Any() == false)
                {
                    flag = true;
                }
                else
                {
                    result = new string(
                        Enumerable.Repeat(chars, length)
                                  .Select(s => s[random.Next(s.Length)])
                                  .ToArray());
                }
            }


            return result;
        }
        public string randomNumbers(int length)
        {
            var chars = "0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());



            return result;
        }
        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public List<SmsBilling> GetCustomerSMSBills(int userid, string startDate, string endDate)
        {
            List<SmsBilling> lst = new List<SmsBilling>();
            try
            {
                var users = DBContext.Users.Where(x => x.StatusID == 1).ToList();
                foreach (var i in users)
                {
                    var _integration = i.Integrations.Where(x => x.Name == "Twilio").FirstOrDefault();
                    if (_integration != null)
                    {

                        var report = smsReport(_integration.IntegrationKey, _integration.Token, startDate, endDate);
                        if (report != null)
                        {
                            lst.Add(new SmsBilling
                            {
                                CompanyName = i.UserName,
                                FromDate = Convert.ToDateTime(startDate).ToShortDateString(),
                                ToDate = Convert.ToDateTime(endDate).ToShortDateString(),
                                SMSCount = report.SMSCount,
                                Total = (report.Total * 3.75) * (-1),
                                UserID = i.UserID
                            });
                        }

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return new List<SmsBilling>();
            }

        }
        public SmsBilling smsReport(string _accountSid, string _authToken, string fDate, string tDate)
        {
            SmsBilling rspReport = new SmsBilling();
            object rsp = null;
            try
            {
                string accountSid = _accountSid;
                string authToken = _authToken;

                try
                {
                    rspReport.SMSCount = 0;
                    rspReport.Total = 0;
                    TwilioClient.Init(accountSid, authToken);


                    var messages = MessageResource.Read(
                         dateSentAfter: DateTime.Parse(fDate),
                         dateSentBefore: DateTime.Parse(tDate)
                    );


                    foreach (var record in messages)
                    {
                        rspReport.SMSCount += int.Parse(record.NumSegments);
                        rspReport.Total += double.Parse(record.Price);

                    }

                }
                catch (Exception ex)
                {
                    rspReport = null;
                }

                return rspReport;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<User> getAllUsers()
        {
            List<SmsBilling> lst = new List<SmsBilling>();
            try
            {
                return DBContext.Users.Where(x => x.StatusID == 1).ToList();
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }
        public List<User> delete(int id, int deletedBy)
        {
            var customer = DBContext.Users.Where(x => x.UserID == id).FirstOrDefault();
            if (customer != null)
            {
                customer.LastUpdatedDate = DateTimeUTC.Now;
                customer.StatusID = 3;
                DBContext.UpdateOnly<User>(customer, x => x.LastUpdatedDate, x => x.StatusID);
                DBContext.SaveChanges();
            }
            return GetAll();
        }
        public List<User> status(int id)
        {
            if (id == 1)
            {
                var customer = DBContext.Users.Where(x => x.StatusID == 1).FirstOrDefault();
                if (customer != null)
                {
                    customer.LastUpdatedDate = DateTimeUTC.Now;
                    customer.StatusID = 2;
                    DBContext.UpdateOnly<User>(customer, x => x.LastUpdatedDate, x => x.StatusID);
                    DBContext.SaveChanges();
                }
            }
            else
            {
                var customer = DBContext.Users.Where(x => x.StatusID != 1).FirstOrDefault();
                customer.LastUpdatedDate = DateTimeUTC.Now;
                customer.StatusID = 1;
                DBContext.UpdateOnly<User>(customer, x => x.LastUpdatedDate, x => x.StatusID);
                DBContext.SaveChanges();
            }
            return GetAll();
        }
        public string GetInvoicePrint(string companyname, string smscount, string fromdate, string todate, string total, int userid)
        {
            try
            {
                List<SmsBilling> Items;
                string html = "";
                string content = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Template") + "\\" + "invoice-sms.txt");



                html += "<tr>                                                                                                                                                                             ";
                html += "      <td height='10' colspan='5' ></td>                                                                                                   ";
                html += "</tr>                                                                                                                                                                           ";
                html += " <tr>                                                                                                                                                                            ";
                html += "      <td style='font-size: 16px; font-family: 'Open Sans', sans-serif; color: #646a6e;  line-height: 18px;  vertical-align: top; padding:10px;'>1</td>                        ";
                html += "      <td style='font-size: 16px; font-family: 'Open Sans', sans-serif; color: #ffb509;  line-height: 18px;  vertical-align: top; padding:10px;' class='article'>              ";
                html += "          " + "Twilio SMS " + "                                                                                                                                       ";
                html += "      </td>                                                                                                                                                                      ";
                html += "      <td style='font-size: 16px; font-family: 'Open Sans', sans-serif; color: #646a6e;  line-height: 18px;  vertical-align: top; padding:10px;' align='center'>  " + smscount + "  </td>         ";
                html += "      <td style='font-size: 16px; font-family: 'Open Sans', sans-serif; color: #646a6e;  line-height: 18px;  vertical-align: top; padding:10px;'><small>  " + "0.2325" + "  </small></td>      ";
                html += "                                                                                                                                                                                 ";
                html += "      <td style='font-size: 16px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px;' align='right'>  " + total + "  </td>    ";
                html += "</tr>                                                                                                                                                                            ";
                html += "<tr>                                                                                                                                                                             ";
                html += "      <td height='10' colspan='5' ></td>                                                                                                   ";
                html += "</tr>                                                                                                                                                                           ";
                html += "<tr>                                                                                                                                                                             ";
                html += "      <td height='1' colspan='5' style='border-bottom:1px solid #e4e4e4'></td>                                                                                                   ";
                html += "</tr>                                                                                                                                                                           ";
                html += "<tr>                                                                                                                                                                             ";
                html += "      <td height='10' colspan='5' ></td>                                                                                                   ";
                html += "</tr>                                                                                                                                                                           ";




                var companyinfo = DBContext.Users.Where(x => x.UserID == userid).FirstOrDefault();
                content = content.Replace("#billno#", "INV-" + randomNumbers(5));
                content = content.Replace("#items#", html);
                content = content.Replace("#companyname#", companyinfo.Company);
                content = content.Replace("#date#", fromdate + " - " + todate);
                content = content.Replace("#logo#", ConfigurationSettings.AppSettings["URL"].ToString() + "\\assets\\images\\logogarage.png");
                content = content.Replace("#note#", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.");
                content = content.Replace("#total#", "SR " + total);


                var path = ConfigurationSettings.AppSettings["URL"].ToString() + GeneratePDF(content, userid, "Invoice", "INV-" + DateTime.UtcNow.Ticks.ToString());

                return path.Replace("~", "");
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public string GeneratePDF(string html, int id, string FolderName, string FileName)
        {

            var htmlContent = html.ToString();
            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);
            var filename = FileName;
            string folderPath = "~/Upload/" + FolderName + "/";   // set folder

            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(folderPath)))
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(folderPath));
            string FilePath = System.Web.HttpContext.Current.Server.MapPath(folderPath + filename + ".pdf");
            var bw = new BinaryWriter(System.IO.File.Open(FilePath, FileMode.OpenOrCreate));
            bw.Write(pdfBytes);
            bw.Close();



            return folderPath + filename + ".pdf";
        }
        public List<UserPackageBLL> GetUserPackage()
        {
            try
            {
                var lst = new List<UserPackageBLL>();

                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetUserPackage_UAT", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<UserPackageBLL>>();
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
