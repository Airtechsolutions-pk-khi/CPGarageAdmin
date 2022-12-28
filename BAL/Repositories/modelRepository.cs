
using DAL.DBEntities;

using BAL.Repositories;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Twilio.Rest.Api.V2010.Account;
using Twilio;
using System.Web;
using System.IO;
using System.Configuration;
 

namespace BAL.Repositories
{
    public class modelRepository : BaseRepository
    {
        

        public modelRepository()
             : base()
        {
            DBContext = new Garage_LiveEntities();
        }

        public modelRepository(Garage_LiveEntities contextDB)
            : base(contextDB)
        {
            DBContext = contextDB;
        }
        public int edit(ModelsViewModel modal, string path)
        {
            using (var dbContextTransaction = DBContext.Database.BeginTransaction())
            {
                try
                {
                    if (modal.ModelID > 0)
                    {
                        Model _model = DBContext.Models.Where(x => x.ModelID == modal.ModelID).FirstOrDefault();                        
                        _model.Name = modal.Name;
                        _model.Year = modal.Year;
                        _model.EngineNo = modal.EngineNo;
                        _model.RecommendedLitres = modal.RecommendedLitres;
                        _model.ImagePath = path;
                        _model.LastUpdatedBy = modal.LastUpdatedBy;
                        _model.LastUpdatedDate = DateTime.Now;
                        _model.StatusID = modal.StatusID;
                        _model.DisplayOrder = modal.DisplayOrder;
                        DBContext.Entry(_model).State = EntityState.Modified;
                        DBContext.UpdateOnly<Model>(_model, x =>
                       x.Name,
                        x => x.Year,
                        x => x.EngineNo,
                        x => x.RecommendedLitres,
                        x => x.ImagePath,
                        x => x.LastUpdatedBy,
                        x => x.LastUpdatedDate,
                        x => x.StatusID,
                        x => x.DisplayOrder

                        );
                        DBContext.SaveChanges();

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
        public int add(ModelsViewModel modal, string path)
        {
            using (var dbContextTransaction = DBContext.Database.BeginTransaction())
            {

                try
                {
                    Model _model = new Model();
                    //Location _location = new Location();
                    //SubUser _subuser = new SubUser();
                    //Receipt _receipt = new Receipt();

                    _model.Name = modal.Name;
                    _model.MakeID = modal.MakeID;
                    _model.Year = modal.Year;
                    _model.EngineNo = modal.EngineNo;
                    _model.RecommendedLitres = modal.RecommendedLitres;
                    _model.ImagePath = path;
                    _model.LastUpdatedBy = modal.LastUpdatedBy;
                    _model.LastUpdatedDate = DateTime.Now;
                    _model.StatusID = modal.StatusID;
                    _model.DisplayOrder = modal.DisplayOrder;
                    Model data = DBContext.Models.Add(_model);
                    DBContext.SaveChanges();
                     
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
       
        public List<Model> GetModels()
        {
            try
            {
                var data = DBContext.Models.Include("Make").Where(x => x.StatusID == 1 || x.StatusID == 2).ToList();
                return data;
            }
            catch (Exception ex)
            {
                //_baseRepo.ErrorLog(ex, "loginRepository/GetLoginInfo", "Exception");
                return new List<Model>();
            }
        }
        public ModelsViewModel GetModelbyid(int id)
        {
            try
            {
                var data = DBContext.Models.Where(x => x.ModelID == id)
                    .AsEnumerable().Select(r => new ModelsViewModel
                    {
                        ModelID = r.ModelID,
                        RowID = r.RowID,
                        MakeID = r.MakeID,
                        Name = r.Name,
                        Year = r.Year,                        
                        EngineNo = r.EngineNo,
                        RecommendedLitres = r.RecommendedLitres,
                        ImagePath = r.ImagePath,
                        LastUpdatedBy = r.LastUpdatedBy,
                        LastUpdatedDate = r.LastUpdatedDate,
                        CreatedOn = r.CreatedOn,
                        CreatedBy = r.CreatedBy,
                        DisplayOrder = r.DisplayOrder,                                               
                        StatusID = r.StatusID,
                        
                    })
                  .FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                //_baseRepo.ErrorLog(ex, "loginRepository/GetLoginInfo", "Exception");
                return new ModelsViewModel();
            }
        }
       
        public List<Model> delete(int id, int deletedBy)
        {
            var modl = DBContext.Models.Where(x => x.ModelID == id).FirstOrDefault();
            if (modl != null)
            {
                modl.LastUpdatedDate = DateTimeUTC.Now;
                modl.StatusID = 3;
                DBContext.UpdateOnly<Model>(modl, x => x.LastUpdatedDate, x => x.StatusID);
                DBContext.SaveChanges();
            }
            return GetModels();
        }















        public int editMake(MakeViewModel modal,string path)
        {
            using (var dbContextTransaction = DBContext.Database.BeginTransaction())
            {
                try
                {
                    if (modal.MakeID > 0)
                    {
                        
                        Make _model = DBContext.Makes.Where(x => x.MakeID == modal.MakeID).FirstOrDefault();


                        _model.Name = modal.Name;
                        _model.ArabicName = modal.ArabicName;
                        _model.RowID = modal.RowID;                       
                        _model.ImagePath = path;
                        _model.LastUpdatedBy = modal.LastUpdatedBy;
                        _model.LastUpdatedDate = DateTime.Now;
                        _model.StatusID = modal.StatusID;
                        _model.DisplayOrder = modal.DisplayOrder;
                        DBContext.Entry(_model).State = EntityState.Modified;
                        DBContext.UpdateOnly<Model>(_model, x =>
                       x.Name,
                        x => x.Year,
                        x => x.EngineNo,
                        x => x.RecommendedLitres,
                        x => x.ImagePath,
                        x => x.LastUpdatedBy,
                        x => x.LastUpdatedDate,
                        x => x.StatusID,
                        x => x.DisplayOrder

                        );
                        DBContext.SaveChanges();

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
        
        public int addMake(MakeViewModel modal, string path)
        {
            using (var dbContextTransaction = DBContext.Database.BeginTransaction())
            {
                try
                {
                    Make _model = new Make();
                    
                    _model.Name = modal.Name;                    
                    _model.ArabicName = modal.ArabicName;
                    _model.ImagePath = path;
                    _model.LastUpdatedBy = modal.LastUpdatedBy;
                    _model.LastUpdatedDate = DateTime.Now;
                    _model.StatusID = modal.StatusID;
                    _model.DisplayOrder = modal.DisplayOrder;
                    Make data = DBContext.Makes.Add(_model);
                    DBContext.SaveChanges();

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

        public List<Make> GetMake()
        {
            try
            {
                var data = DBContext.Makes.Where(x => x.StatusID == 1 || x.StatusID == 2).ToList();
                return data;
            }
            catch (Exception ex)
            {
                //_baseRepo.ErrorLog(ex, "loginRepository/GetLoginInfo", "Exception");
                return new List<Make>();
            }
        }
        public MakeViewModel GetMakebyid(int id)
        {
            try
            {
                var data = DBContext.Makes.Where(x => x.MakeID == id)
                    .AsEnumerable().Select(r => new MakeViewModel
                    {
                        MakeID = r.MakeID,
                        RowID = r.RowID,                        
                        Name = r.Name,
                        ArabicName = r.ArabicName,
                        ImagePath = r.ImagePath,
                        LastUpdatedBy = r.LastUpdatedBy,
                        LastUpdatedDate = r.LastUpdatedDate,
                        CreatedOn = r.CreatedOn,
                        CreatedBy = r.CreatedBy,
                        DisplayOrder = r.DisplayOrder,
                        StatusID = r.StatusID,

                    })
                  .FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                //_baseRepo.ErrorLog(ex, "loginRepository/GetLoginInfo", "Exception");
                return new MakeViewModel();
            }
        }

        public List<Make> deleteMake(int id, int deletedBy)
        {
            var make = DBContext.Makes.Where(x => x.MakeID == id).FirstOrDefault();
            if (make != null)
            {
                make.LastUpdatedDate = DateTimeUTC.Now;
                make.StatusID = 3;
                DBContext.UpdateOnly<Make>(make, x => x.LastUpdatedDate, x => x.StatusID);
                DBContext.SaveChanges();
            }
            return GetMake();
        }
    }
}
