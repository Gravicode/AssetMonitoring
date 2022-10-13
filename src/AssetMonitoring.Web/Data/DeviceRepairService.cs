using AssetMonitoring.Models;
using Microsoft.EntityFrameworkCore;
using AssetMonitoring.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitoring.Web.Data
{
    public class DeviceRepairService : ICrud<DeviceRepair>
    {
        AssetMonitoringDB db;

        public DeviceRepairService()
        {
            if (db == null) db = new AssetMonitoringDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.DeviceRepairs.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.DeviceRepairs.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<DeviceRepair> FindByKeyword(string Keyword)
        {
            var data = from x in db.DeviceRepairs
                       where x.SerialNo.Contains(Keyword) || x.MerchantName.Contains(Keyword) || x.PicName.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<DeviceRepair> GetAllData()
        {
            return db.DeviceRepairs.Include(c=>c.Merchant).Include(c => c.Workshop).ToList();
        }

        public DeviceRepair GetDataById(object Id)
        {
            return db.DeviceRepairs.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(DeviceRepair data)
        {
            try
            {
                db.DeviceRepairs.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(DeviceRepair data)
        {
            try
            {
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                /*
                if (sel != null)
                {
                    sel.Nama = data.Nama;
                    sel.Keterangan = data.Keterangan;
                    sel.Tanggal = data.Tanggal;
                    sel.DocumentUrl = data.DocumentUrl;
                    sel.StreamUrl = data.StreamUrl;
                    return true;

                }*/
                return true;
            }
            catch
            {

            }
            return false;
        }

        public long GetLastId()
        {
            return db.DeviceRepairs.Max(x => x.Id);
        }
    }

}